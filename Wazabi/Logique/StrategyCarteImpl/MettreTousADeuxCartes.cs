using System;
using System.Linq;
using Wazabi.Model;

namespace Wazabi.Logique.StrategyCarteImpl
{
    public class MettreTousADeuxCartes : StrategyCarte
    {
        /**
         * Permet de mettre tous les joueurs à deux carte random ssi il ne s'agit pas du joueurCourant
         **/

        public override bool faireOperation(Partie partie, JoueurPartie joueurAdverse, int nbDe)
        {
            foreach (JoueurPartie jpTmp in partie.Joueurs)
            {
                //On vérifier pas joueur courant et que le joueur a plus de deux cartes (optimisation)
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