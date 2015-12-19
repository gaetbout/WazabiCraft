using System.Linq;
using Wazabi.Model;

namespace Wazabi.Logique.StrategyCarteImpl
{
    public class FairePasserTour : StrategyCarte
    {
        /**
         * Permet de faire passe le tour au joueurAdverse 
         **/

        public override bool faireOperation(Partie partie, JoueurPartie joueurAdverse, int nbDe)
        {
            base.verifierJoueurCourrantDifferentJoueurParam(partie, joueurAdverse);

            partie.Joueurs.FirstOrDefault(j => j.Id == joueurAdverse.Id).NbSkips++;
            return true;
        }
    }
}