using Wazabi.Model;

namespace Wazabi.UCCImpl
{
    public abstract class EtatImpl
    {
        protected internal readonly WazabiEntities context;
        protected internal readonly Partie partie;

        public EtatImpl(WazabiEntities cont, Partie partie)
        {
            this.context = cont;
            this.partie = partie;
        }

        public abstract bool LancerPartie();
        public abstract bool RejoindrePartie(JoueurClient joueur);
        public abstract void InitPlateau(int nbCarteJoueur);
        public abstract void TourSuivant(GestionDe gestionDe);
        public abstract JoueurPartie Suivant();
    }
}