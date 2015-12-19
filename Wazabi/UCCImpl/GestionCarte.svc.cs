using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Wazabi.Logique;
using Wazabi.Logique.StrategyCarteImpl;
using Wazabi.Model;

namespace Wazabi.UCC
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom de classe "GestionCarte" à la fois dans le code, le fichier svc et le fichier de configuration.
    // REMARQUE : pour lancer le client test WCF afin de tester ce service, sélectionnez GestionCarte.svc ou GestionCarte.svc.cs dans l'Explorateur de solutions et démarrez le débogage.
    public class GestionCarte : IGestionCarte
    {
        private readonly WazabiEntities context = new WazabiEntities();

        private Dictionary<string, StrategyCarte> dico = new Dictionary<string, StrategyCarte>();
        private int NbCartesTotal;

        public GestionCarte(IEnumerable<XElement> xml, int NbCartesTotal)
        {
            dico["1"] = new SupprimerNbeDe();
            dico["2"] = new ChangerSensDe();
            dico["3"] = new SupprimerNbeDe();
            dico["4"] = new DonnerUnDe();
            dico["5"] = new PrendreUneCarte();
            dico["6"] = new MettreAUneCarte();
            dico["7"] = new PiocherTroisCartes();
            dico["8"] = new MettreTousADeuxCartes();
            dico["9"] = new FairePasserTour();
            dico["10"] = new RejouerChangerSens();

            foreach (var carte in xml)
            {
                for (int i = 0; i < int.Parse(carte.Attribute("nb").Value); i++)
                {
                    Carte tmp = new Carte(dico[carte.Attribute("codeEffet").Value]);
                    tmp.Cout = int.Parse(carte.Attribute("cout").Value);
                    tmp.CodeEffet = carte.Attribute("codeEffet").Value;
                    tmp.Description = carte.Value;
                    tmp.Effet = carte.Attribute("effet").Value;
                    tmp.ImageRef = carte.Attribute("src").Value;
                    context.Cartes.Add(tmp);
                }
            }

            this.NbCartesTotal = NbCartesTotal;

            context.SaveChanges();
        }

        public List<Carte> GenererPioche()
        {
            return context.Cartes.Take(NbCartesTotal).OrderBy(c => Guid.NewGuid()).ToList();
        }
    }
}