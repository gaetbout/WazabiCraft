using System;
using System.Collections.Generic;
using System.Linq;
using Wazabi.Model;

namespace Wazabi.UCC
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom de classe "GestionCarte" à la fois dans le code, le fichier svc et le fichier de configuration.
    // REMARQUE : pour lancer le client test WCF afin de tester ce service, sélectionnez GestionCarte.svc ou GestionCarte.svc.cs dans l'Explorateur de solutions et démarrez le débogage.
    public class GestionCarte : IGestionCarte
    {
        private readonly WazabiEntities context = new WazabiEntities();

        public List<Carte> GenererPioche()
        {
            List<Carte> pioche = new List<Carte>(context.Cartes);
            return pioche.OrderBy(c => new Random().Next()).ToList();
        }
    }
}