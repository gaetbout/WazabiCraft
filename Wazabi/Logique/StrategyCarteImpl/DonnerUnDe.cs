using System.Linq;
using Wazabi.Model;

namespace Wazabi.Logique.StrategyCarteImpl
{
    public class DonnerUnDe : StrategyCarte
    {
        /**
         * Permet de donner un dé au joueur passé en paramètre 
         **/

        public override bool faireOperation(Partie partie, JoueurPartie joueurAdverse, int nbDe)
        {
            base.verifierJoueurCourrantDifferentJoueurParam(partie, joueurAdverse);

            De deTmp = partie.JoueurCourant.Des.Last();
            partie.JoueurCourant.Des.Remove(deTmp);
            partie.Joueurs.FirstOrDefault(x => x.Id == joueurAdverse.Id).Des.Add(deTmp);
            return true;
        }
    }
}