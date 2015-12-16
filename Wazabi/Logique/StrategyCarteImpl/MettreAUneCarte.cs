using System;
using System.Linq;
using Wazabi.Model;

namespace Wazabi.Logique.StrategyCarteImpl
{
    public class MettreAUneCarte : StrategyCarte
    {
        /**
         * Permet de mettre joueurAdverse à un carte random
         **/

        public bool faireOperation(Partie partie, Joueur joueurAdverse, int nbDe)
        {
            base.verifierJoueurCourrantDifferentJoueurParam(partie, joueurAdverse);

            JoueurPartie jpTmp = partie.Joueurs.Where(x => x.Id == joueurAdverse.Id).FirstOrDefault();

            Random random = new Random();
            Carte cTmp = jpTmp.Cartes.ElementAt(random.Next(jpTmp.Cartes.Count));
            jpTmp.Cartes.Clear();
            jpTmp.Cartes.Add(cTmp);
            return true;
        }
    }
}