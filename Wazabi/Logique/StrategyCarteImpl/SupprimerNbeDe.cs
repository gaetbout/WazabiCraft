using System;
using System.Linq;
using Wazabi.Model;

namespace Wazabi.Logique.StrategyCarteImpl
{
    public class SupprimerNbeDe : StrategyCarte
    {
        /**
         * Il faut vérifier la contrainte avant d'utiliser cette méhode :
         * Le dé est retiré de la partie.On ne peut pas jouer cette carte si on
         * obtient dans le même tour 2 figures <figure ref="d" /> ou plus avec ses dés
         * Permet de supprimer nbDe (1 ou 2) au joueur courant
         * */

        public override bool faireOperation(Partie partie, Joueur joueurAdverse, int nbDe)
        {
            if (nbDe < 0 || nbDe > 2)
            {
                throw new ArgumentException("Le nombre de Dé a supprimer doit être égal à 1 ou 2");
            }
            if (nbDe == 1)
            {
                int d = 0;
                foreach (De de in partie.JoueurCourant.Des)
                {
                    if (de.Valeur.Equals("d"))
                    {
                        d++;
                    }
                }
                if (d >= 2)
                {
                    return false;
                }
            }
            for (int i = 0; i < nbDe; i++)
            {
                try
                {
                    De deTmp = partie.JoueurCourant.Des.Last();
                    partie.JoueurCourant.Des.Remove(deTmp);
                }
                catch (ArgumentException e)
                {
                    //Return true signifie que le joueur n'a plus de Dés
                    return true;
                }
            }
            return true;
        }
    }
}