using System;
using Wazabi.Model;

namespace Wazabi.UCCImpl
{
    public class EtatFinie : EtatImpl
    {
        public EtatFinie(WazabiEntities cont, Partie partie)
            : base(cont, partie)
        {
        }

        public override bool LancerPartie()
        {
            return false;
        }

        public override bool RejoindrePartie(Joueur joueur)
        {
            return false;
        }

        public override void InitPlateau(int nbCarteJoueur)
        {
            throw new Exception("Partie déja finie!");
        }

        public override void TourSuivant(GestionDe gestionDe)
        {
            throw new Exception("Partie déja finie!");
        }

        public override JoueurPartie Suivant()
        {
            throw new Exception("Partie déja finie!");
        }

        public override void QuitterPartie(JoueurClient joueur)
        {
            throw new Exception("Impossible de quitter la partie car elle est cloturé!");
        }

        public override void CloturerPartie(Joueur vainqueur)
        {
            throw new Exception("Impossible de cloturer la partie car elle est déja cloturé!");
        }
    }
}