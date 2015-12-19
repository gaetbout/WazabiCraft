using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Web.Hosting;
using System.Xml.Linq;
using Wazabi.Client;
using Wazabi.Model;
using Wazabi.UCC;

namespace Wazabi.UCCImpl
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)] // 1 seule instance est renvoyé
    public class GestionPartie : IGestionPartie
    {
        private string But;
        private int NbCartesJoueur;
        private int NbCartesTotal;
        public int MinJoueurs;
        private int MaxJoueurs;

        public readonly WazabiEntities context = new WazabiEntities();
        public GestionDe gestionDe;
        public GestionCarte gestionCarte;
        public Partie Partie;
        public EtatImpl Etat;

        public void Init()
        {
            if (But != null)
            {
                return;
            }

            XDocument xdoc;
            try
            {
                xdoc = XDocument.Load(HostingEnvironment.ApplicationPhysicalPath + "wazabi.xml");
            }
            catch (FileNotFoundException e)
            {
                throw new Exception("Fichier incorrect");
            }

            //Charger les données du jeu
            IEnumerable<XElement> donnees = from wazabi in xdoc.Descendants("wazabi") select wazabi;
            But = donnees.First().Attribute("but").Value;
            NbCartesJoueur = int.Parse(donnees.First().Attribute("nbCartesParJoueur").Value);
            NbCartesTotal = int.Parse(donnees.First().Attribute("nbCartesTotal").Value);
            MinJoueurs = int.Parse(donnees.First().Attribute("minJoueurs").Value);
            MaxJoueurs = int.Parse(donnees.First().Attribute("maxJoueurs").Value);

            //Charges les données des dés 
            IEnumerable<XElement> desXML = from de in xdoc.Descendants("de") select de;
            gestionDe = new GestionDe(desXML);

            //Charger les cartes 
            IEnumerable<XElement> cartesXML = from carte in xdoc.Descendants("carte") select carte;
            gestionCarte = new GestionCarte(cartesXML, NbCartesTotal);
        }

        public bool CreerPartie(JoueurClient joueur, string nom)
        {
            Joueur temp = context.Joueurs.FirstOrDefault(j => j.Id == joueur.Id);
            if (temp == null)
            {
                return false;
            }

            Partie partieEnCours =
                context.Parties.FirstOrDefault(
                    p => p.Etat == (int) Partie.State.CREATION || p.Etat == (int) Partie.State.EN_COURS);
            if (partieEnCours != null)
            {
                return false;
            }

            Partie = new Partie();
            Partie.Createur = temp;
            Partie.EtatType = Partie.State.CREATION;
            Partie.DateHeureCreation = DateTime.Now;
            Partie.Nom = nom;
            Partie.Sens = true;

            context.Parties.Add(Partie);
            context.SaveChanges();

            Etat = new EtatCreation(this);

            return Etat.RejoindrePartie(temp);
        }


        public bool LancerPartie()
        {
            return Etat.LancerPartie();
        }

        public bool RejoindrePartie(JoueurClient joueur)
        {
            Joueur temp = context.Joueurs.FirstOrDefault(j => j.Id == joueur.Id);
            if (temp == null)
            {
                throw new Exception("Le joueur qui essaie de rejoindre la partie n'existe pas en base de donnée");
            }
            if (Partie == null || Partie.Joueurs.Any(j => j.Joueur_Id == temp.Id))
            {
                return false;
            }

            return Etat.RejoindrePartie(temp);
        }

        public PartieClient PartieCourante()
        {
            if (Etat == null)
            {
                return null;
            }

            return Etat.PartieCourante();
        }

        public void InitPlateau()
        {
            Etat.InitPlateau(NbCartesJoueur);
            TourSuivant();
        }

        public void TourSuivant()
        {
            Etat.TourSuivant(gestionDe);
        }

        private JoueurPartie Suivant()
        {
            return Etat.Suivant();
        }

        public void QuitterPartie(JoueurClient joueur)
        {
            if (Partie == null) throw new Exception("Aucune partie en cours, impossible de quitter!");
            if (Partie.Joueurs.FirstOrDefault(j => j.Joueur_Id == joueur.Id) == null)
                throw new Exception("Joueur pas présent dans la partie, impossible de quitter!");
            Etat.QuitterPartie(joueur);
        }

        public void CloturerPartie(Joueur vainqueur)
        {
            if (Partie == null)
                throw new Exception("On ne peut pas cloturer la partie car il n'y a pas de partie en cours!");
            if (Partie.Joueurs.FirstOrDefault(j => j.Joueur_Id == vainqueur.Id) == null)
                throw new Exception("Le vainqueur est pas présent dans la partie, impossible de cloturer!");
            Etat.CloturerPartie(vainqueur);
        }

        public ICollection<PartieClient> GetParties()
        {
            ICollection<PartieClient> collection = new List<PartieClient>();

            foreach (Partie partie in context.Parties)
            {
                if (partie.EtatType == Partie.State.FINIE)
                {
                    PartieClient temp = new PartieClient(partie);
                    collection.Add(temp);
                }
            }
            return collection.OrderByDescending(p => p.Id).ToList();
        }


        public ICollection<JoueurPartieClient> GetJoueurPartie(int idPartie)
        {
            ICollection<JoueurPartieClient> collection = new List<JoueurPartieClient>();

            Partie partie = context.Parties.FirstOrDefault(p => p.Id == idPartie);

            foreach (JoueurPartie jp in partie.Joueurs)
            {
                JoueurPartieClient jpc = new JoueurPartieClient(jp);
                collection.Add(jpc);
            }

            return collection;
        }


        public void ClearBD()
        {
            context.Database.ExecuteSqlCommand("DELETE FROM [Wazabi].[dbo].[DeJoueurPartie]");
            context.Database.ExecuteSqlCommand("DELETE FROM [Wazabi].[dbo].[JoueurPartieCarte]");
            context.Database.ExecuteSqlCommand("DELETE FROM [Wazabi].[dbo].[JoueurParties]");
            context.Database.ExecuteSqlCommand("DELETE FROM [Wazabi].[dbo].[PartieCarte]");
            context.Database.ExecuteSqlCommand("DELETE FROM [Wazabi].[dbo].[Parties]");
            context.Database.ExecuteSqlCommand("DELETE FROM [Wazabi].[dbo].[Joueurs]");
            context.Database.ExecuteSqlCommand("DELETE FROM [Wazabi].[dbo].[Des]");
            context.Database.ExecuteSqlCommand("DELETE FROM [Wazabi].[dbo].[Cartes]");
        }


        public bool ActionDes(Dictionary<string, string> dictionary)
        {
            return Etat.ActionDes(dictionary);
        }

        public bool ActionCartes(string codeEffet, string idJoueur, string value)
        {
            JoueurPartie joueurCourant = Partie.JoueurCourant;
            Carte carte = joueurCourant.Cartes.FirstOrDefault(c => c.CodeEffet.Equals(codeEffet));

            if (joueurCourant.Des.Count(d => d.Valeur.Equals("w")) >= carte.Cout &&
                Etat.ActionCartes(codeEffet, idJoueur, value))
            {
                joueurCourant.Cartes.Remove(carte);
                Partie.Pioche.Add(carte);

                context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}