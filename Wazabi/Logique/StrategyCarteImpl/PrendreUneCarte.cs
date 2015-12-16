using System;
using System.Linq;
using Wazabi.Model;

namespace Wazabi.Logique.StrategyCarteImpl
{
    public class PrendreUneCarte : StrategyCarte
    {
        /**
         * Permet de piocher voler une carte à l'adversaire
         **/

        public override bool faireOperation(Partie partie, Joueur joueurAdverse, int nbCartes)
        {
            if (nbCartes != 1)
            {
                throw new ArgumentException("Peut prendre voler une seule carte");
            }
            base.verifierJoueurCourrantDifferentJoueurParam(partie, joueurAdverse);
            JoueurPartie joueurPartieAdverse = partie.Joueurs.Where(x => x.Id == joueurAdverse.Id).FirstOrDefault();
            
            //Cas ou on vérifier que tous les joueurs nont plus de carte
            if (joueurPartieAdverse.Cartes.Count == 0)
            {
                foreach (JoueurPartie jp in partie.Joueurs)
                {
                    if (jp.Id != partie.JoueurCourant.Id && jp.Id != joueurPartieAdverse.Id)
                    {
                        if (jp.Cartes.Count > 0)
                            throw new ArgumentException("Le jouer adverse doit avoir au moins une carte et un autre joueur a au moins une carte");
                    }
                }
                //On va dans la pioche
                Carte c = partie.Pioche.FirstOrDefault();
                partie.Pioche.Remove(c);
                partie.JoueurCourant.Cartes.Add(c);
                return true;
            }

            //Cas ou le joueur adverse a au moins une carte
            Random random = new Random();
            Carte tmp = joueurPartieAdverse.Cartes.ElementAt(random.Next(joueurPartieAdverse.Cartes.Count));
            joueurPartieAdverse.Cartes.Remove(tmp);
            partie.JoueurCourant.Cartes.Add(tmp);
            return true;
        }
    }
}