using System;
using System.Linq;
using System.ServiceModel;
using Wazabi.Model;
using Wazabi.UCC;
using System.Xml.Linq;
using System.Collections.Generic;
using System.IO;
using System.Web.Hosting;

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



        public GestionPartie()
        {

        }
        private readonly WazabiEntities context = new WazabiEntities();
        public Partie partie = null;

        public void CreerPartie(JoueurClient joueur, string nom)
        {
            Joueur temp = context.Joueurs.FirstOrDefault(j => j.Id == joueur.Id);
            if (temp == null)
            {
                throw new Exception("Le joueur qui essaie de creer la partie n'existe pas en base de donnée");
            }

            Partie partieEnCours =
                context.Parties.FirstOrDefault(
                    p => p.EtatType == Partie.State.CREATION || p.EtatType == Partie.State.EN_COURS);
            if (partieEnCours != null)
            {
                throw new Exception("Une partie est déja en cours, vous ne pouvez pas en créer une nouvelle!");
            }

            partie = new Partie();
            partie.Createur = temp;
            partie.EtatType = Partie.State.CREATION;
            partie.DateHeureCreation = DateTime.Now;
            partie.Nom = nom;

            context.Parties.Add(partie);
            context.SaveChanges();
        }


        public bool LancerPartie()
        {
            if (partie == null || partie.EtatType != Partie.State.CREATION)
            {
                return false;
            }

            partie.EtatType = Partie.State.EN_COURS;
            int i = partie.Joueurs.Count();
            JoueurPartie courant = partie.Joueurs.ElementAt(new Random().Next(i) + 1);

            partie.JoueurCourant = courant;
            context.SaveChanges();
            return true;
        }

        public void init()
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

            GestionDe gestionDe = new GestionDe(int.Parse(des.First().Attribute("nbParJoueur").Value), int.Parse(des.First().Attribute("nbTotalDes").Value), lesDes);

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
                context.Cartes.Add(tmp);
            }
            context.SaveChanges();
        }
    }
}