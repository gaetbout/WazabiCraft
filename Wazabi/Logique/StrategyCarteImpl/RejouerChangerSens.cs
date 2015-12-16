using Wazabi.Model;
using System;

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
                //partie.JoueurCourant = partie.Joueurs.
            }
            else // mauvais sens => joueurCourant = joueur d'après
            {
            }
            partie.Sens = !partie.Sens;
            throw new ArgumentException("Pas encore fait");
            //return false;
        }
    }
}