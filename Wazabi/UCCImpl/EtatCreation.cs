using System;
using System.Diagnostics;
using System.Linq;
using Wazabi.Client;
using Wazabi.Model;

namespace Wazabi.UCCImpl
{
    public class EtatCreation : EtatImpl
    {
        public EtatCreation(WazabiEntities cont, Partie partie)
            : base(cont, partie)
        {
        }

        public override bool LancerPartie()
        {
            partie.EtatType = Partie.State.EN_COURS;
            int i = partie.Joueurs.Count();
            JoueurPartie courant = partie.Joueurs.ElementAt(new Random().Next(i));

            partie.JoueurCourant = courant;
            context.SaveChanges();
            return true;
        }

        public override bool RejoindrePartie(Joueur joueur)
        {
            JoueurPartie temp = new JoueurPartie();
            temp.Joueur = joueur;
            temp.Ordre = partie.Joueurs.Count();

            this.partie.Joueurs.Add(temp);
            context.SaveChanges();
            return true;
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

        public override void QuitterPartie(JoueurClient joueur)
        {
            partie.Joueurs.Remove(partie.Joueurs.FirstOrDefault(j => j.Joueur_Id == joueur.Id));
            context.SaveChanges();
        }

        public override void CloturerPartie(Joueur vainqueur)
        {
            throw new Exception("Impossible de cloturer la partie car elle n'a pas démarrer!");
        }

        public override PartieClient PartieCourante()
        {
            return new PartieClient(partie);
        }
    }
}