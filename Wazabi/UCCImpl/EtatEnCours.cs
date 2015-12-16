using System;
using System.Collections.Generic;
using System.Linq;
using Wazabi.Model;
using Wazabi.UCC;

namespace Wazabi.UCCImpl
{
    public class EtatEnCours : EtatImpl
    {
        private GestionCarte gestionCarte = new GestionCarte();

        public EtatEnCours(WazabiEntities cont, Partie partie)
            : base(cont, partie)
        {
        }

        public override bool LancerPartie()
        {
            return false;
        }

        public override bool RejoindrePartie(JoueurClient joueur)
        {
            return false;
        }

        public override void InitPlateau(int nbCarteJoueur)
        {
            partie.Pioche = gestionCarte.GenererPioche();
            foreach (JoueurPartie j in partie.Joueurs)
            {
                for (int i = 0; i < nbCarteJoueur; i++)
                {
                    int rand = new Random().Next(partie.Pioche.Count() + 1);
                    j.Cartes.Add(partie.Pioche.ElementAt(rand));
                    partie.Pioche.Remove(partie.Pioche.ElementAt(rand));
                }
            }
        }

        public override void TourSuivant(GestionDe gestionDe)
        {
            IList<De> des = gestionDe.MelangerDe(partie.JoueurCourant.Des.Count);
            for (int i = 0; i < des.Count(d => d.Valeur == "c"); i++)
            {
                Carte pioche = partie.Pioche.FirstOrDefault();
                partie.Pioche.Remove(pioche);
                partie.JoueurCourant.Cartes.Add(pioche);
            }
        }

        public override JoueurPartie Suivant()
        {
            if (partie.Sens)
            {
                return partie.JoueurCourant = partie.Joueurs.ElementAt(partie.JoueurCourant.Ordre + 1);
            }
            else
            {
                return partie.JoueurCourant = partie.Joueurs.ElementAt(partie.JoueurCourant.Ordre - 1);
            }
        }
    }
}