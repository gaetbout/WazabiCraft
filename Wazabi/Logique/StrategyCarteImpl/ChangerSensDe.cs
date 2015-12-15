using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Wazabi.Model;

namespace Wazabi.Logique.StrategyCarteImpl
{
    public class ChangerSensDe : StrategyCarte
    {
        /**
         * Permet de donner ses dés à droite ou a gauche en fonction du int
         * si sens == 0 => droite
         * si sens == 1 => gauche
         **/
        public bool faireOperation(Partie partie, Joueur joueurAdverse, int sens)
        {
            if (sens != 0 && sens != 1)
            {
                throw new ArgumentException("Lit la Doc");
            }
            // Si bad sens, on reverse liste
            if (sens == 1)
            {
                partie.Joueurs.Reverse();
            }
            //on va du 2e au dernier 
            IList<De> dePrec = new List<De>(partie.Joueurs.FirstOrDefault().Des);
            for (int i = 1; i < partie.Joueurs.Count; i++)
            {
                JoueurPartie jTmp = partie.Joueurs.ElementAt(i);
                IList<De> deTmp = new List<De>(jTmp.Des);
                jTmp.Des = dePrec;
                dePrec = deTmp;
            }
            partie.Joueurs.FirstOrDefault().Des = dePrec;

            //re-reverse liste 
            if (sens == 1)
            {
                partie.Joueurs.Reverse();
            }
            return true;
        }
    }
}