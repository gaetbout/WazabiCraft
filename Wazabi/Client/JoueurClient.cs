using System.Runtime.Serialization;
using Wazabi.Model;

namespace Wazabi.Client
{
    [DataContract]
    public class JoueurClient
    {
        public JoueurClient()
        {
        }

        public JoueurClient(Joueur joueur)
        {
            Id = joueur.Id;
            Pseudo = joueur.Pseudo;
        }

        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Pseudo { get; set; }

        [DataMember]
        public string Mdp { get; set; }

        [DataMember]
        public string ConfirmPassword { get; set; }
    }
}