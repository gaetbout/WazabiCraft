using Wazabi.Client;
using Wazabi.Model;

namespace Wazabi.UCCImpl
{
    public abstract class EtatImpl
    {
        protected internal readonly GestionPartie gestionPartie;

        public EtatImpl(GestionPartie gestionPartie)
        {
            this.gestionPartie = gestionPartie;
        }

        public abstract bool LancerPartie();
        public abstract bool RejoindrePartie(Joueur joueur);
        public abstract void InitPlateau(int nbCarteJoueur);
        public abstract void TourSuivant(GestionDe gestionDe);
        public abstract JoueurPartie Suivant();
        public abstract void QuitterPartie(JoueurClient joueur);
        public abstract void CloturerPartie(Joueur vainqueur);
        public abstract PartieClient PartieCourante();
        public abstract bool ActionDes(System.Collections.Generic.Dictionary<string, string> dictionary);
        public abstract bool ActionCartes(string codeEffet, string idJoueur = "0", string value = "0");
    }
}