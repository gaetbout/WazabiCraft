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
        public string But;
        public int NbCartesJoueur;
        public int NbCartesTotal;
        public int MinJoueurs;
        public int MaxJoueurs;

        private readonly WazabiEntities context = new WazabiEntities();
        private GestionDe gestionDe;
        private GestionCarte gestionCarte = new GestionCarte();
        public Partie Partie = null;
        public EtatImpl Etat;

        public void Init()
        {
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
            this.But = donnees.First().Attribute("but").Value;
            this.NbCartesJoueur = int.Parse(donnees.First().Attribute("nbCartesParJoueur").Value);
            this.NbCartesTotal = int.Parse(donnees.First().Attribute("nbCartesTotal").Value);
            this.MinJoueurs = int.Parse(donnees.First().Attribute("minJoueurs").Value);
            this.MaxJoueurs = int.Parse(donnees.First().Attribute("maxJoueurs").Value);

            //Charges les données des dés 
            IEnumerable<XElement> des = from de in xdoc.Descendants("de") select de;
            IList<De> lesDes = new List<De>();
            foreach (var face in des.Descendants("face"))
            {
                string check = face.Attribute("identif").Value;
                if (!context.Des.Any(d => d.Valeur.Equals(check)))
                {
                    for (int i = 0; i < int.Parse(face.Attribute("nbFaces").Value); i++)
                    {
                        De tmp = new De();
                        tmp.Valeur = face.Attribute("identif").Value;
                        lesDes.Add(tmp);
                        context.Des.Add(tmp);
                    }
                }
            }

            GestionDe gestionDe = new GestionDe(int.Parse(des.First().Attribute("nbParJoueur").Value),
                int.Parse(des.First().Attribute("nbTotalDes").Value), lesDes);

            //Charger les cartes 
            IEnumerable<XElement> cartes = from carte in xdoc.Descendants("carte") select carte;
            // Read cartes
            foreach (var carte in cartes)
            {
                string check = carte.Attribute("codeEffet").Value;
                if (!context.Cartes.Any(c => c.CodeEffet.Equals(check)))
                {
                    for (int i = 0; i < int.Parse(carte.Attribute("nb").Value); i++)
                    {
                        Carte tmp = new Carte();
                        tmp.Cout = int.Parse(carte.Attribute("cout").Value);
                        tmp.CodeEffet = carte.Attribute("codeEffet").Value;
                        tmp.Description = carte.Value;
                        tmp.Effet = carte.Attribute("effet").Value;
                        tmp.ImageRef = carte.Attribute("src").Value;
                        context.Cartes.Add(tmp);    
                    }
                }
            }
            context.SaveChanges();
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

            JoueurPartie joueurPartie = new JoueurPartie();
            joueurPartie.Joueur = temp;
            joueurPartie.Ordre = 0;

            Partie.Joueurs.Add(joueurPartie);

            context.Parties.Add(Partie);
            context.SaveChanges();

            Etat = new EtatCreation(context, Partie);
            return true;
        }


        public bool LancerPartie()
        {
            if (Etat.LancerPartie())
            {
                Etat = new EtatEnCours(context, Partie);
                InitPlateau();
                return true;
            }
            return false;
        }

        public bool RejoindrePartie(JoueurClient joueur)
        {
            Joueur temp = context.Joueurs.FirstOrDefault(j => j.Id == joueur.Id);
            if (temp == null)
            {
                throw new Exception("Le joueur qui essaie de rejoindre la partie n'existe pas en base de donnée");
            }
            if (Partie == null)
            {
                return false;
            }
            if (Etat.RejoindrePartie(temp))
            {
                if (Partie.Joueurs.Count() == this.MinJoueurs) LancerPartie();
                return true;
            }
            return false;
        }

        public PartieClient PartieCourante()
        {
            if (Partie == null)
            {
                return null;
            }

            return new PartieClient(Partie);
        }

        private void InitPlateau()
        {
            Etat.InitPlateau(this.NbCartesJoueur);
        }

        private void TourSuivant()
        {
            Etat.TourSuivant(this.gestionDe);
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
                JoueurPartieClient jpc = new JoueurPartieClient();
                jpc.Id = partie.JoueurCourant.Id;
                jpc.Pseudo = partie.JoueurCourant.Joueur.Pseudo;
                PartieClient temp = new PartieClient();
                temp.Etat = partie.Etat;
                temp.Id = partie.Id;
                temp.Nom = partie.Nom;
                temp.Sens = partie.Sens;
                temp.JoueurCourant = jpc;
                collection.Add(temp);
            }
            return collection;
        }


        public ICollection<JoueurPartieClient> GetJoueurPartie(int idPartie)
        {
            ICollection<JoueurPartieClient> collection = new List<JoueurPartieClient>();

            Partie partie = context.Parties.FirstOrDefault(p => p.Id == idPartie);

            foreach (JoueurPartie jp in partie.Joueurs)
            {
                JoueurPartieClient jpc = new JoueurPartieClient();
                jpc.Id = jp.Id;
                jpc.Pseudo = jp.Joueur.Pseudo;
                collection.Add(jpc);
            }

            return collection;
        }


        public void ClearBD()
        {
            context.Database.ExecuteSqlCommand("DELETE FROM JoueurParties");
            context.Database.ExecuteSqlCommand("DELETE FROM Parties");
            context.Database.ExecuteSqlCommand("DELETE FROM Joueurs");
        }
    }
}