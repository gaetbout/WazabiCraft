using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Wazabi.Model;

namespace Wazabi.Logique.StrategyCarteImpl
{
    public class SupprimerNbeDe : StrategyCarte
    {
        /**
         * Permet de supprimer nbDe au joueur courant
         * */
        public bool faireOperation(Partie partie, Joueur joueurAdverse, int nbDe)
        {
            if (nbDe < 0 || nbDe > 2)
            {
                throw new ArgumentException("Le nombre de Dé a supprimer doit être égal à 1 ou 2");
            }
            for (int i = 0; i < nbDe;i++ )
            {
                try
                {
                De deTmp = partie.JoueurCourant.Des.Last();
                partie.JoueurCourant.Des.Remove(deTmp);
                }
                catch (ArgumentException e)
                {
                    return true;
                }
            }
            return true;
        }
    }
}