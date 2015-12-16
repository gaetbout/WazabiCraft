using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Wazabi.Model;

namespace Wazabi.Logique.StrategyCarteImpl
{
    public class PiocherTroisCartes : StrategyCarte
    {

        /**
        * Permet de piocher 3 cartes
        **/
 
        public override bool faireOperation(Partie partie, Joueur joueurAdverse, int nbCartes)
        {
            if (nbCartes != 3)
            {
                throw new ArgumentException("Peut piocher uniquement 3 cartes");
            }
            for (int i = 0; i < nbCartes; i++)
            {
                Carte tmp = partie.Pioche.FirstOrDefault();
                partie.Pioche.Remove(tmp);
                if (tmp == null)
                {
                    // Dans le cas ou la pioche est vide
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