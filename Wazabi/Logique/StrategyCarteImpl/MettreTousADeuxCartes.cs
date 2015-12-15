using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Wazabi.Model;

namespace Wazabi.Logique.StrategyCarteImpl
{
    public class MettreTousADeuxCartes : StrategyCarte
    {
        /**
         * Permet de mettre tous les joueurs à deux carte random ssi il ne s'agit pas du joueurCourant
         **/
        public bool faireOperation(Partie partie, Joueur joueurAdverse, int nbDe)
        {
            foreach (JoueurPartie jpTmp in partie.Joueurs)
            {
                if (jpTmp.Id != (partie.JoueurCourant.Id) && jpTmp.Cartes.Count() > 2)
                {
                    Random random = new Random();

                    Carte cTmpUn = jpTmp.Cartes.ElementAt(random.Next(jpTmp.Cartes.Count));
                    jpTmp.Cartes.Remove(cTmpUn);
                    Carte cTmpDeux = jpTmp.Cartes.ElementAt(random.Next(jpTmp.Cartes.Count));
                    jpTmp.Cartes.Clear();

                    jpTmp.Cartes.Add(cTmpUn);
                    jpTmp.Cartes.Add(cTmpDeux);
                }
            }
            return true;
        }
    }
}