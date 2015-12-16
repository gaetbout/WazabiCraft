using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Web.Hosting;
using System.Xml.Linq;
using Wazabi.Model;
using Wazabi.UCC;

namespace Wazabi.UCCImpl
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)] // 1 seule instance est renvoyé
    public class GestionPartie : IGestionPartie
    {
        public string BUT;
        public int NB_CARTES_JOUEUR;
        public int NB_CARTES_TOTAL;
        public int MIN_JOUEURS;
        public int MAX_JOUEURS;

        private readonly WazabiEntities context = new WazabiEntities();
        private GestionDe gestionDe;
        private GestionCarte gestionCarte = new GestionCarte();
        public Partie partie = null;
        public EtatImpl etat;

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
            this.BUT = donnees.First().Attribute("but").Value;
            this.NB_CARTES_JOUEUR = int.Parse(donnees.First().Attribute("nbCartesParJoueur").Value);
            this.NB_CARTES_TOTAL = int.Parse(donnees.First().Attribute("nbCartesTotal").Value);
            this.MIN_JOUEURS = int.Parse(donnees.First().Attribute("minJoueurs").Value);
            this.MAX_JOUEURS = int.Parse(donnees.First().Attribute("maxJoueurs").Value);

            //Charges les données des dés 
            IEnumerable<XElement> des = from de in xdoc.Descendants("de") select de;
            IList<De> lesDes = new List<De>();
            foreach (var face in des.Descendants("face"))
            {
                De tmp = new De();
                tmp.Valeur = face.Attribute("identif").Value;
                context.Des.Add(tmp);
                for (int i = 0; i < int.Parse(face.Attribute("nbFaces").Value); i++)
                {
                    lesDes.Add(tmp);
                }
            }
            context.SaveChanges();

            GestionDe gestionDe = new GestionDe(int.Parse(des.First().Attribute("nbParJoueur").Value),
                int.Parse(des.First().Attribute("nbTotalDes").Value), lesDes);

            //Charger les cartes 
            IEnumerable<XElement> cartes = from carte in xdoc.Descendants("carte") select carte;
            // Read cartes
            foreach (var carte in cartes)
            {
                Carte tmp = new Carte();
                tmp.Cout = int.Parse(carte.Attribute("cout").Value);
                tmp.CodeEffet = carte.Attribute("codeEffet").Value;
                tmp.Description = carte.Value;
                tmp.Effet = carte.Attribute("effet").Value;
                tmp.ImageRef = carte.Attribute("src").Value;
                tmp.NbCartes = int.Parse(carte.Attribute("nb").Value);
                context.Cartes.Add(tmp);
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

            partie = new Partie();
            partie.Createur = temp;
            partie.EtatType = Partie.State.CREATION;
            partie.DateHeureCreation = DateTime.Now;
            partie.Nom = nom;
            partie.Sens = true;

            etat = new EtatCreation(context, partie);

            context.Parties.Add(partie);
            context.SaveChanges();

            return true;
    }


        public bool LancerPartie()
        {
            return this.etat.LancerPartie();
        }

        public bool RejoindrePartie(JoueurClient joueur)
        {
            Joueur temp = context.Joueurs.FirstOrDefault(j => j.Id == joueur.Id);
            if (temp == null)
            {
                throw new Exception("Le joueur qui essaie de rejoindre la partie n'existe pas en base de donnée");
            }
            if (this.partie == null)
            {
                return false;
            }
            if (this.etat.RejoindrePartie(joueur))
            {
                if (this.partie.Joueurs.Count() == this.MIN_JOUEURS) LancerPartie();
                return true;
            }
            return false;
        }

        public PartieClient PartieCourante()
        {
            if (this.partie == null)
            {
                return null;
            }
            PartieClient partie = new PartieClient();
            partie.Id = this.partie.Id;
            partie.Nom = this.partie.Nom;
            partie.DateHeureCreation = this.partie.DateHeureCreation;
            partie.Etat = this.partie.Etat;
            partie.Sens = this.partie.Sens;
            return partie;
        }

        private void InitPlateau()
        {
            this.etat.InitPlateau(this.NB_CARTES_JOUEUR);
        }


        private void TourSuivant()
        {
            this.etat.TourSuivant(this.gestionDe);
        }

        private JoueurPartie Suivant()
        {
            return this.etat.Suivant();
        }
    }
}