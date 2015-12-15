using System.Runtime.Serialization;

namespace Wazabi
{
    [DataContract]
    public class PartieClient
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public System.DateTime DateHeureCreation { get; set; }

        [DataMember]
        public bool Sens { get; set; }

        [DataMember]
        public int Etat { get; set; }

        [DataMember]
        public string Nom { get; set; }
    }
}