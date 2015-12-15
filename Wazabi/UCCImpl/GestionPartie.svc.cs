using System;
using System.Linq;
using System.ServiceModel;
using Wazabi.Model;
using Wazabi.UCC;

namespace Wazabi.UCCImpl
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)] // 1 seule instance est renvoyé
    public class GestionPartie : IGestionPartie
    {
        private readonly WazabiEntities context = new WazabiEntities();
        public Partie partie = null;

        public void CreerPartie(JoueurClient joueur, string nom)
        {
            Joueur temp = context.Joueurs.FirstOrDefault(j => j.Id == joueur.Id);
            if (temp == null)
            {
                throw new Exception("Le joueur qui essaie de creer la partie n'existe pas en base de donnée");
            }

            Partie partieEnCours =
                context.Parties.FirstOrDefault(
                    p => p.EtatType == Partie.State.CREATION || p.EtatType == Partie.State.EN_COURS);
            if (partieEnCours != null)
            {
                throw new Exception("Une partie est déja en cours, vous ne pouvez pas en créer une nouvelle!");
            }

            partie = new Partie();
            partie.Createur = temp;
            partie.EtatType = Partie.State.CREATION;
            partie.DateHeureCreation = DateTime.Now;
            partie.Nom = nom;

            context.Parties.Add(partie);
            context.SaveChanges();
        }


        public bool LancerPartie()
        {
            if (partie == null || partie.EtatType != Partie.State.CREATION)
            {
                return false;
            }

            partie.EtatType = Partie.State.EN_COURS;
            int i = partie.Joueurs.Count();
            JoueurPartie courant = partie.Joueurs.ElementAt(new Random().Next(i) + 1);

            partie.JoueurCourant = courant;
            context.SaveChanges();
            return true;
        }
    }
}