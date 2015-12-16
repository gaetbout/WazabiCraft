using System;
using System.Linq;
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
            JoueurPartie courant = partie.Joueurs.ElementAt(new Random().Next(i) + 1);

            partie.JoueurCourant = courant;
            context.SaveChanges();
            return true;
        }

        public override bool RejoindrePartie(JoueurClient joueur)
        {
            JoueurPartie temp = new JoueurPartie();
            temp.Joueur = context.Joueurs.FirstOrDefault(j => j.Id == joueur.Id);
            temp.Joueur_Id = joueur.Id;
            temp.Ordre = (context.Joueurs.Count() + 1);
            temp.PartieJoueurPartie_JoueurPartie_Id = partie.Id;
            this.partie.Joueurs.Add(temp);
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
    }
}