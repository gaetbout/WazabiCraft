using System.Collections.Generic;
using System.Runtime.Serialization;
using Wazabi.Model;

namespace Wazabi.Client
{
    [DataContract]
    public class PartieClient
    {
        public PartieClient()
        {
        }

        public PartieClient(Partie partie)
        {
            Id = partie.Id;
            Nom = partie.Nom;
            Sens = partie.Sens;
            Etat = partie.Etat;
            Createur = partie.Createur.Pseudo;
            DateCreation = partie.DateHeureCreation.ToString();

            Joueurs = new List<JoueurPartieClient>();
            foreach (JoueurPartie joueurPartie in partie.Joueurs)
            {
                Joueurs.Add(new JoueurPartieClient(joueurPartie));
            }
            if (partie.JoueurCourant != null)
            {
                JoueurCourant = new JoueurPartieClient(partie.JoueurCourant);
            }
            if (partie.Vainqueur != null)
            {
                Vainqueur = partie.Vainqueur.Pseudo;
            }
            else
            {
                Vainqueur = "Pas de Vainqueur";
            }
        }

        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public bool Sens { get; set; }

        [DataMember]
        public int Etat { get; set; }

        [DataMember]
        public string Nom { get; set; }

        [DataMember]
        public string Vainqueur { get; set; }

        [DataMember]
        public string Createur { get; set; }

        [DataMember]
        public string DateCreation { get; set; }

        [DataMember]
        public List<JoueurPartieClient> Joueurs { get; set; }

        [DataMember]
        public JoueurPartieClient JoueurCourant { get; set; }
    }
}