using System;
using System.Collections.Generic;
using System.Linq;
using Wazabi.Client;
using Wazabi.Model;

namespace Wazabi.UCCImpl
{
    public class EtatCreation : EtatImpl
    {
        public EtatCreation(GestionPartie gestionPartie)
            : base(gestionPartie)
        {
        }

        public override bool LancerPartie()
        {
            gestionPartie.Partie.EtatType = Partie.State.EN_COURS;
            JoueurPartie courant =
                gestionPartie.Partie.Joueurs.ElementAt(new Random().Next(gestionPartie.Partie.Joueurs.Count()));

            gestionPartie.Partie.JoueurCourant = courant;
            gestionPartie.context.SaveChanges();

            gestionPartie.Etat = new EtatEnCours(gestionPartie);
            gestionPartie.InitPlateau();
            return true;
        }

        public override bool RejoindrePartie(Joueur joueur)
        {
            JoueurPartie temp = new JoueurPartie();
            temp.Joueur = joueur;
            temp.Ordre = gestionPartie.Partie.Joueurs.Count();

            gestionPartie.Partie.Joueurs.Add(temp);
            gestionPartie.context.SaveChanges();

            if (gestionPartie.Partie.Joueurs.Count() == gestionPartie.MinJoueurs)
            {
                LancerPartie();
            }
            return true;
        }

        public override void QuitterPartie(JoueurClient joueur)
        {
            gestionPartie.Partie.Joueurs.Remove(
                gestionPartie.Partie.Joueurs.FirstOrDefault(j => j.Joueur_Id == joueur.Id));

            int nombreJoueurs = gestionPartie.Partie.Joueurs.Count();
            if (nombreJoueurs == 0)
            {
                gestionPartie.context.Parties.Remove(gestionPartie.Partie);
                gestionPartie.Etat = null;
            }
            else
            {
                for (int i = 0; i < nombreJoueurs; i++)
                {
                    gestionPartie.Partie.Joueurs.ElementAt(i).Ordre = i;
                }
            }

            gestionPartie.context.SaveChanges();
        }

        public override PartieClient PartieCourante()
        {
            return new PartieClient(gestionPartie.Partie);
        }

        public override bool ActionDes(Dictionary<string, string> dictionary)
        {
            throw new Exception("Partie en création!");
        }

        public override bool ActionCartes(string codeEffet, string idJoueur, string value)
        {
            throw new Exception("Partie pas encore lancée!");
        }

        public override void InitPlateau(int nbCarteJoueur)
        {
            throw new Exception("Partie pas encore lancée!");
        }

        public override void TourSuivant(GestionDe gestionDe)
        {
            throw new Exception("Partie pas encore lancée!");
        }

        public override JoueurPartie Suivant()
        {
            throw new Exception("Partie pas encore lancée!");
        }

        public override void CloturerPartie(Joueur vainqueur)
        {
            throw new Exception("Impossible de cloturer la partie car elle n'a pas démarrer!");
        }
    }
}