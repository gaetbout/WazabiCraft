using System;
using System.Linq;
using Wazabi.Model;

namespace Wazabi.Logique.StrategyCarteImpl
{
    public class PrendreUneCarte : StrategyCarte
    {
        /**
         * Permet de voler une carte à l'adversaire
         **/

        public override bool faireOperation(Partie partie, JoueurPartie joueurAdverse, int nbCartes)
        {
            base.verifierJoueurCourrantDifferentJoueurParam(partie, joueurAdverse);

            JoueurPartie joueurPartieAdverse = partie.Joueurs.FirstOrDefault(x => x.Id == joueurAdverse.Id);
            //Cas ou on vérifier que tous les joueurs n'ont plus de carte
            if (joueurPartieAdverse.Cartes.Count == 0)
            {
                //Vérification si les autres joueurs ont encore des cartes
                foreach (JoueurPartie jp in partie.Joueurs)
                {
                    if (jp.Id != partie.JoueurCourant.Id && jp.Id != joueurPartieAdverse.Id)
                    {
                        if (jp.Cartes.Count > 0)
                        {
                            throw new ArgumentException(
                                "Le jouer adverse doit avoir au moins une carte et un autre joueur a au moins une carte");
                        }
                    }
                }
                //On va dans la pioche
                Carte c = partie.Pioche.FirstOrDefault();
                try
                {
                    partie.Pioche.Remove(c);
                    partie.JoueurCourant.Cartes.Add(c);
                }
                catch (Exception e)
                {
                    //Le joueur courrant posède toutes les cartes
                }
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