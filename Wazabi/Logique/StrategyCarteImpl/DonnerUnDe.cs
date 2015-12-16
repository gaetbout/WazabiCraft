using System.Linq;
using Wazabi.Model;

namespace Wazabi.Logique.StrategyCarteImpl
{
    public class DonnerUnDe : StrategyCarte
    {
        /**
         * Permet de donner un dé au joueur passé en paramètre 
         * */

        public bool faireOperation(Partie partie, Joueur joueurAdverse, int nbDe)
        {
            base.verifierJoueurCourrantDifferentJoueurParam(partie, joueurAdverse);
            De deTmp = partie.JoueurCourant.Des.Last();
            partie.JoueurCourant.Des.Remove(deTmp);
            partie.Joueurs.Where(x => x.Id == joueurAdverse.Id).First().Des.Add(deTmp);
            return true;
        }
    }
}