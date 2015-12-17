using System.Collections.Generic;
using System.Runtime.Serialization;
using Wazabi.Client;
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

            Joueurs = new List<JoueurPartieClient>();
            foreach (JoueurPartie joueurPartie in partie.Joueurs)
            {
                Joueurs.Add(new JoueurPartieClient(joueurPartie));
            }

            JoueurCourant = new JoueurPartieClient(partie.JoueurCourant);
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
        public List<JoueurPartieClient> Joueurs { get; set; }

        [DataMember]
        public JoueurPartieClient JoueurCourant { get; set; }
    }
}