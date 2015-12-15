using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Wazabi
{
    [DataContract]
    public class JoueurClient
    {
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