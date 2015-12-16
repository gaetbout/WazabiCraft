using Wazabi.Model;
using System;
using System.Linq;

namespace Wazabi.Logique.StrategyCarteImpl
{
    public class RejouerChangerSens : StrategyCarte
    {
        /**
         * Permet de faire rejouer le joueur courrant et change le sens de jeu 
         **/

        public override bool faireOperation(Partie partie, Joueur joueurAdverse, int nbDe)
        {
            if (partie.Sens) //Bon sens joueur courant => joueur d'avant
            {
                partie.JoueurCourant = partie.Joueurs.ElementAt(partie.JoueurCourant.Ordre + 1);
            }
            else // mauvais sens => joueurCourant = joueur d'après
            {
                partie.JoueurCourant = partie.Joueurs.ElementAt(partie.JoueurCourant.Ordre - 1);
            }
            partie.Sens = !partie.Sens;
            return true;
        }
    }
}