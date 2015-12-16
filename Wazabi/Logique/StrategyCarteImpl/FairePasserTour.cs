using System;
using Wazabi.Model;
using System.Linq;

namespace Wazabi.Logique.StrategyCarteImpl
{
    public class FairePasserTour : StrategyCarte
    {
        /**
         * Permet de faire passe le tour au joueurAdverse 
         **/

        public override bool faireOperation(Partie partie, Joueur joueurAdverse, int nbDe)
        {
            base.verifierJoueurCourrantDifferentJoueurParam(partie, joueurAdverse);
            if (partie.JoueursQuiDoiventPasser.Where(x=> x.Id == joueurAdverse.Id).Count() == 1)
            {
                throw new ArgumentException("Ce joueur doit déjà passer son tour");
            }
            partie.JoueursQuiDoiventPasser.Add(partie.Joueurs.Where(x => x.Id == joueurAdverse.Id).FirstOrDefault());
            return true;
        }
    }
}