using Wazabi.Model;

namespace Wazabi.Logique.StrategyCarteImpl
{
    public class RejouerChangerSens : StrategyCarte
    {
        /**
         * Permet de faire rejouer le joueur courrant et change le sens de jeu 
         **/

        public override bool faireOperation(Partie partie, JoueurPartie joueurAdverse, int nbDe)
        {
            foreach (var joueur in partie.Joueurs)
            {
                if (partie.JoueurCourant.Id != joueur.Id)
                {
                    joueur.NbSkips++;
                }
            }

            partie.Sens = !partie.Sens;
            return true;
        }
    }
}