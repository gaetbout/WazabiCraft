using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.ServiceModel;
using System.Text;
using System.Web.ModelBinding;
using System.Web.UI.WebControls;
using Wazabi.Model;

namespace Wazabi.UCC
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom de classe "GestionJoueur" à la fois dans le code, le fichier svc et le fichier de configuration.
    // REMARQUE : pour lancer le client test WCF afin de tester ce service, sélectionnez GestionJoueur.svc ou GestionJoueur.svc.cs dans l'Explorateur de solutions et démarrez le débogage.
    public class GestionJoueur : IGestionJoueur
    {
        private readonly WazabiEntities context = new WazabiEntities();

        public void Inscription(JoueurClient joueur)
        {
            Joueur nouveauJoueur =  new Joueur();
            if (context.Joueurs.FirstOrDefault(j => j.Pseudo.Equals(joueur.Pseudo)) != null)
            {
                throw new Exception("Le pseudo est déjà utilisé");
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
    }
}
