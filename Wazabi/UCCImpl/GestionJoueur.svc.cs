using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Wazabi.Client;
using Wazabi.Model;
using Wazabi.UCC;

namespace Wazabi.UCCImpl
{
    public class GestionJoueur : IGestionJoueur
    {
        private readonly WazabiEntities context = new WazabiEntities();

        public bool Inscription(JoueurClient joueur)
        {
            Joueur nouveauJoueur = new Joueur();
            if (context.Joueurs.FirstOrDefault(j => j.Pseudo.Equals(joueur.Pseudo)) != null)
            {
                return false;
            }

            byte[] mdp = Encoding.UTF8.GetBytes(joueur.Mdp);

            byte[] sel = new byte[20];
            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(sel);
            }

            byte[] mdpCryptee = ProtectedData.Protect(mdp, sel, DataProtectionScope.CurrentUser);

            nouveauJoueur.Pseudo = joueur.Pseudo;
            nouveauJoueur.MotDePasse = mdpCryptee;
            nouveauJoueur.Sel = sel;


            context.Joueurs.Add(nouveauJoueur);
            context.SaveChanges();
            return true;
        }

        public JoueurClient Connexion(JoueurClient joueur)
        {
            Joueur joueurEspere = context.Joueurs.FirstOrDefault(j => j.Pseudo.Equals(joueur.Pseudo));

            //Identifiant inexistant
            if (joueurEspere == null)
            {
                return null;
            }

            byte[] decodage = ProtectedData.Unprotect(joueurEspere.MotDePasse, joueurEspere.Sel,
                DataProtectionScope.CurrentUser);

            if (Encoding.UTF8.GetBytes(joueur.Mdp).SequenceEqual(decodage))
            {
                joueur.Id = joueurEspere.Id;
                return joueur;
            }

            // Mdp incorrecte
            return null;
        }

        public ICollection<JoueurClient> GetJoueurs()
        {
            ICollection<JoueurClient> collection = new List<JoueurClient>();

            foreach (Joueur joueur in context.Joueurs)
            {
                JoueurClient temp = new JoueurClient();
                temp.ConfirmPassword = System.Text.Encoding.Default.GetString(joueur.MotDePasse);
                temp.Id = joueur.Id;
                temp.Mdp = System.Text.Encoding.Default.GetString(joueur.MotDePasse);
                temp.Pseudo = joueur.Pseudo;
                collection.Add(temp);
            }
            return collection;
        }
    }
}