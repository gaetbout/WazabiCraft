using System;
using System.Collections.Generic;
using System.Linq;
using Wazabi.Client;
using Wazabi.Model;

namespace Wazabi.UCCImpl
{
    public class EtatEnCours : EtatImpl
    {
        public EtatEnCours(GestionPartie gestionPartie)
            : base(gestionPartie)
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
            Partie partie = gestionPartie.Partie;

            partie.Pioche = gestionPartie.gestionCarte.GenererPioche();

            foreach (JoueurPartie j in partie.Joueurs)
            {
                Random rand = new Random();
                for (int i = 0; i < nbCarteJoueur; i++)
                {
                    Carte c = partie.Pioche.ElementAt(rand.Next(partie.Pioche.Count()));
                    j.Cartes.Add(c);
                    partie.Pioche.Remove(c);
                }
            }

            gestionPartie.gestionDe.GenererDes(gestionPartie.Partie.Joueurs);
            gestionPartie.gestionDe.MelangerDe(gestionPartie.Partie.JoueurCourant);
            gestionPartie.context.SaveChanges();
        }

        public override void TourSuivant(GestionDe gestionDe)
        {
            Partie partie = gestionPartie.Partie;
            if (!partie.JoueurCourant.Des.Any())
            {
                CloturerPartie(gestionPartie.context.Joueurs.FirstOrDefault(j => j.Id == partie.JoueurCourant.Joueur_Id));
            }
            partie.JoueurCourant = Suivant();
            while (partie.JoueurCourant.NbSkips > 0)
            {
                partie.JoueurCourant.NbSkips--;
                partie.JoueurCourant = Suivant();
            }
            gestionPartie.context.SaveChanges();

            gestionDe.MelangerDe(partie.JoueurCourant);

            for (int i = 0; i < partie.JoueurCourant.Des.Count(d => d.Valeur.Equals("c")); i++)
            {
                Carte c = partie.Pioche.First();
                partie.Pioche.Remove(c);
                partie.JoueurCourant.Cartes.Add(c);
            }

            gestionPartie.context.SaveChanges();
            return;
        }

        public override JoueurPartie Suivant()
        {
            Partie partie = gestionPartie.Partie;

            if (partie.Sens)
            {
                if (partie.JoueurCourant.Ordre == partie.Joueurs.Count - 1)
                {
                    return partie.Joueurs.ElementAt(0);
                }
                return partie.JoueurCourant = partie.Joueurs.ElementAt(partie.JoueurCourant.Ordre + 1);
            }
            else
            {
                if (partie.JoueurCourant.Ordre == 0)
                {
                    return partie.Joueurs.ElementAt(partie.Joueurs.Count - 1);
                }
                return partie.JoueurCourant = partie.Joueurs.ElementAt(partie.JoueurCourant.Ordre - 1);
            }
        }

        public override void QuitterPartie(JoueurClient joueur)
        {
<<<<<<< HEAD
            Partie partie = gestionPartie.Partie;
            JoueurPartie joueurASupprimer = partie.Joueurs.FirstOrDefault(j => j.Joueur_Id == joueur.Id);

            partie.Joueurs.Remove(joueurASupprimer);

=======
            (partie.Joueurs.FirstOrDefault(j => j.Joueur_Id == joueur.Id)).Cartes.Clear();
            (partie.Joueurs.FirstOrDefault(j => j.Joueur_Id == joueur.Id)).Des.Clear();
            partie.Joueurs.Remove(partie.Joueurs.FirstOrDefault(j => j.Joueur_Id == joueur.Id));
>>>>>>> origin/Default-branch
            if (partie.Joueurs.Count() == 1)
            {
                Joueur vainqueur = (partie.Joueurs.FirstOrDefault()).Joueur;
                CloturerPartie(vainqueur);
            }
            else
            {
                int i = 0;
                foreach (var joueurAModifier in partie.Joueurs)
                {
                    joueurAModifier.Ordre = i++;
                }
            }

            gestionPartie.context.SaveChanges();
        }

        public override void CloturerPartie(Joueur vainqueur)
        {
<<<<<<< HEAD
            Partie partie = gestionPartie.Partie;
            partie.Pioche.Clear();

            foreach (var joueur in partie.Joueurs)
            {
                joueur.Cartes.Clear();
                joueur.Des.Clear();
            }

            partie.EtatType = Partie.State.FINIE;
            partie.Vainqueur = gestionPartie.context.Joueurs.FirstOrDefault(j => j.Id == vainqueur.Id);
            gestionPartie.Partie = null;
            gestionPartie.Etat = null;

            gestionPartie.context.SaveChanges();
=======
            this.partie.EtatType = Partie.State.FINIE;
            this.partie.Etat = (int) Partie.State.FINIE;
            this.partie.Vainqueur = vainqueur;
            this.partie.Vainqueur_Id = this.partie.Vainqueur.Id;
            context.SaveChanges();
>>>>>>> origin/Default-branch
        }

        public override PartieClient PartieCourante()
        {
            return new PartieClient(gestionPartie.Partie);
        }

        public override bool ActionDes(Dictionary<string, string> dictionary)
        {
            Partie partie = gestionPartie.Partie;

            //Attribution des dés aux ≠ joueurs.
            foreach (string s in dictionary.Keys)
            {
                JoueurPartie joueur = partie.Joueurs.FirstOrDefault(j => j.Id == int.Parse(s));
                if (s == null)
                {
                    return false;
                }
                for (int i = 0; i < int.Parse(dictionary[s]); i++)
                {
                    De de = partie.JoueurCourant.Des.FirstOrDefault();
                    partie.JoueurCourant.Des.Remove(de);
                    joueur.Des.Add(de);
                }
            }


            //On termine la partie si le joueur n'a plus de dés.
            if (!partie.JoueurCourant.Des.Any())
            {
                this.CloturerPartie(
                    gestionPartie.context.Joueurs.FirstOrDefault(j => j.Id == partie.JoueurCourant.Joueur_Id));
            }

            gestionPartie.context.SaveChanges();
            return true;
        }

        public override bool ActionCartes(string codeEffet, string idJoueur = "0", string value = "0")
        {
            Partie partie = gestionPartie.Partie;
            Carte carte = partie.JoueurCourant.Cartes.FirstOrDefault(j => j.CodeEffet.Equals(codeEffet));
            JoueurPartie joueur = partie.Joueurs.FirstOrDefault(j => j.Id == int.Parse(idJoueur));
            int valeur = 0;
            if (codeEffet.Equals("1"))
            {
                valeur = 1;
            }
            else if (codeEffet.Equals("3"))
            {
                valeur = 2;
            }
            else
            {
                valeur = int.Parse(value);
            }

            return carte.executeStrategy(partie, joueur, valeur);
        }
    }
}