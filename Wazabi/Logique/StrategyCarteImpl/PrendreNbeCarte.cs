using System;
using System.Linq;
using Wazabi.Model;

namespace Wazabi.Logique.StrategyCarteImpl
{
    public class PrendreNbeCarte : StrategyCarte
    {
        /**
         * Permet de piocher nbCartes  
         **/

        public bool faireOperation(Partie partie, Joueur joueurAdverse, int nbCartes)
        {
            if (nbCartes != 1 && nbCartes != 3)
            {
                throw new ArgumentException("Peut piocher soit 1 ou 3 cartes");
            }
            for (int i = 0; i < nbCartes; i++)
            {
                Carte tmp = partie.Pioche.FirstOrDefault();
                partie.Pioche.Remove(tmp);
                if (tmp == null)
                {
                    // Dans le cas ou on piche chez qqun d'autre 
                    // => on prends une carte random à la personne qui a le + de cartes
                    JoueurPartie jp = base.joueurMaxCartes(partie);
                    Random random = new Random();
                    tmp = jp.Cartes.ElementAt(random.Next(jp.Cartes.Count));
                    jp.Cartes.Remove(tmp);
                }
                partie.JoueurCourant.Cartes.Add(tmp);
            }
            return true;
        }
    }
}