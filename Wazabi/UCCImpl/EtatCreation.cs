using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Wazabi.Model;

namespace Wazabi.UCCImpl
{
    public class EtatCreation : EtatImpl
    {

        public EtatCreation(WazabiEntities cont, Partie partie)
            : base(cont, partie)
        {
        }

        public override bool LancerPartie()
        {
            partie.EtatType = Partie.State.EN_COURS;
            int i = partie.Joueurs.Count();
            JoueurPartie courant = partie.Joueurs.ElementAt(new Random().Next(i) + 1);

            partie.JoueurCourant = courant;
            context.SaveChanges();
            return true;
        }

        public override bool RejoindrePartie(JoueurClient joueur)
        {
          /*  JoueurPartie temp = new JoueurPartie();
            Joueur j = new Joueur();
            j.IconeRef = joueur.
            temp.Joueur = joueur;
            temp.Joueur_Id = joueur.Id;
            this.partie.Joueurs.Add();*/
            return true;
        }

    }
}