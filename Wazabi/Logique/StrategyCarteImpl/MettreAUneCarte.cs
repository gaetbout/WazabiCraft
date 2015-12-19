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

        public override bool faireOperation(Partie partie, JoueurPartie joueurAdverse, int nbDe)
        {
            base.verifierJoueurCourrantDifferentJoueurParam(partie, joueurAdverse);

            JoueurPartie jpTmp = partie.Joueurs.FirstOrDefault(x => x.Id == joueurAdverse.Id);
            if (jpTmp.Cartes.Count < 2)
            {
                throw new ArgumentException("Le joueur adverse doit avoir au moins deux cartes");
            }
            Random random = new Random();
            Carte cTmp = jpTmp.Cartes.ElementAt(random.Next(jpTmp.Cartes.Count));
            jpTmp.Cartes.Clear();
            jpTmp.Cartes.Add(cTmp);
            return true;
        }
    }
}