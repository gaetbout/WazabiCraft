using System;
using System.Linq;
using Wazabi.Model;

namespace Wazabi.Logique
{
    public abstract class StrategyCarte
    {
        /**
         * Cette méthode vérifier que le joueur actuel de la partie n'est pas le joueur passé en paramètre 
         **/

        protected internal void verifierJoueurCourrantDifferentJoueurParam(Partie partie, Joueur joueurACheck)
        {
            if (partie.JoueurCourant.Id != (joueurACheck.Id))
                throw new ArgumentException("Joueur actuel identique au joueur dont il faut faire l'action");
        }

        /**
         * Permet de renvoyer le joueur ayant le maximum de cartes
         * Vérifie que celui-ci est différent du joueur courant
         **/

        protected internal JoueurPartie joueurMaxCartes(Partie partie)
        {
            IOrderedEnumerable<JoueurPartie> joueursOrderByCarteCount =
                partie.Joueurs.OrderByDescending(x => x.Cartes.Count);
            JoueurPartie tmp = joueursOrderByCarteCount.FirstOrDefault();

            if (tmp.Equals(partie.JoueurCourant))
            {
                tmp = joueursOrderByCarteCount.Skip(1).FirstOrDefault();
            }
            return tmp;
        }

        //bool faireOperation(Partie partie, Joueur joueurAdverse, int nbDe);
    }
}