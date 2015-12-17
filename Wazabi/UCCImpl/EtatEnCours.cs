using System;
using System.Collections.Generic;
using System.Linq;
using Wazabi.Client;
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

        public override bool RejoindrePartie(Joueur joueur)
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
            context.SaveChanges();
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
            context.SaveChanges();
        }

        public override JoueurPartie Suivant()
        {
            if (partie.Sens)
            {
                if (partie.JoueurCourant.Ordre == partie.Joueurs.Count)
                {
                    return partie.Joueurs.ElementAt(0);
                }
                return partie.JoueurCourant = partie.Joueurs.ElementAt(partie.JoueurCourant.Ordre + 1);
            }
            else
            {
                if (partie.JoueurCourant.Ordre == 1)
                {
                    return partie.Joueurs.ElementAt(partie.Joueurs.Count - 1);
                }
                return partie.JoueurCourant = partie.Joueurs.ElementAt(partie.JoueurCourant.Ordre - 1);
            }
        }

        public override void QuitterPartie(JoueurClient joueur)
        {
            partie.Joueurs.Remove(partie.Joueurs.FirstOrDefault(j => j.Joueur_Id == joueur.Id));
            if (partie.Joueurs.Count() == 1)
            {
                Joueur vainqueur = (partie.Joueurs.FirstOrDefault()).Joueur;
                CloturerPartie(vainqueur);
            }
            else
            {
                this.partie.JoueurCourant = Suivant();
            }
            context.SaveChanges();
        }

        public override void CloturerPartie(Joueur vainqueur)
        {
            this.partie.EtatType = Partie.State.FINIE;
            this.partie.Etat = (int) Partie.State.FINIE;
            this.partie.Vainqueur = context.Joueurs.FirstOrDefault(j => j.Id == vainqueur.Id);
            this.partie.Vainqueur_Id = this.partie.Vainqueur.Id;
            context.SaveChanges();
        }

        public override PartieClient PartieCourante()
        {
            return new PartieClient(partie);
        }
    }
}