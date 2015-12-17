using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests.ServiceJoueur;
using Tests.ServicePartie;

namespace Test
{
    class Program
    {

        static void Main(string[] args)
        {
            IGestionJoueur gestionJoueur = new GestionJoueurClient();
            IGestionPartie gestionPartie = new GestionPartieClient();
            
            gestionPartie.ClearBD();

            Console.WriteLine("Inscription du joueur Quentin avec comme mdp quentin");
            Tests.ServiceJoueur.JoueurClient joueur = new Tests.ServiceJoueur.JoueurClient();
            joueur.Pseudo = "Quentin";
            joueur.Mdp = "quentin";
            joueur.ConfirmPassword = "quentin";
            if (!gestionJoueur.Inscription(joueur))
            {
                Console.WriteLine("Probleme lors d'une inscription");
            }
            else
            {
                Console.WriteLine("Inscription ok : ");
            }
            foreach (Tests.ServiceJoueur.JoueurClient joueurs in gestionJoueur.GetJoueurs())
            {
                Console.WriteLine("Pseudo : " + joueurs.Pseudo + " mdp: " + joueurs.Mdp);
            }

            Console.WriteLine("Connexion de Quentin");
            Tests.ServiceJoueur.JoueurClient jClient;
            if ((jClient = gestionJoueur.Connexion(joueur)) == null)
            {
                Console.WriteLine("Probleme pour se connecter");
            }
            else
            {
                Console.WriteLine("Connexion ok : " + jClient.Pseudo + " " + jClient.Mdp);
            }

            Console.WriteLine("Creation  d'une partie");
            Tests.ServicePartie.JoueurClient createur = new Tests.ServicePartie.JoueurClient();
            createur.Id = jClient.Id;
            createur.Pseudo = jClient.Pseudo;
            createur.Mdp = jClient.Pseudo;
            if (!gestionPartie.CreerPartie(createur, "partie 1"))
            {
                Console.WriteLine("Erreur lors d'une création d'une partie!!");
            }
            else
            {
                Console.WriteLine("Creation ok : ");
            }
            foreach (Tests.ServicePartie.PartieClient partie in gestionPartie.GetParties())
            {
                Console.WriteLine("Partie => " + partie.Nom + " etat " + partie.Etat);
            }

            Console.WriteLine("Piotr rejoind la partie!");
            Tests.ServiceJoueur.JoueurClient joueur2 = new Tests.ServiceJoueur.JoueurClient();
            joueur2.Pseudo = "Piotr";
            joueur2.Mdp = "piotr";
            joueur2.ConfirmPassword = "piotr";
            gestionJoueur.Inscription(joueur2);
            Tests.ServiceJoueur.JoueurClient jClient2 = gestionJoueur.Connexion(joueur2);
            Tests.ServicePartie.JoueurClient participant = new Tests.ServicePartie.JoueurClient();
            participant.Id = jClient2.Id;
            participant.Pseudo = jClient2.Pseudo;
            participant.Mdp = jClient2.Mdp;
            if (!gestionPartie.RejoindrePartie(participant))
            {
                Console.WriteLine("Impossible pour Piotr de rejoindre la partie");
            }
            else
            {
                Console.WriteLine("Piotr a rejoind la partie");
                PartieClient temp = gestionPartie.PartieCourante();
                Console.WriteLine("Partie " + temp.Nom + " avec comme etat " + temp.Etat + " (1 => EnCours)");
                foreach (Tests.ServicePartie.JoueurPartieClient jpc in gestionPartie.GetJoueurPartie(temp.Id))
                {
                    Console.WriteLine("JoueurPartie : " + jpc.Pseudo);
                }
            }

            Console.ReadLine();
        }
    }
}
