using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Wazabi
{
    [DataContract]
    public class JoueurClient
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        [Required]
        public string Pseudo { get; set; }

        [DataMember]
        [Required]
        [Display(Name = "Mot de passe")]
        public string Mdp { get; set; }

        [DataMember]
        [Required]
        [Compare("Mdp")]
        [Display(Name = "Confirmer mot de passe")]
        public string ConfirmPassword { get; set; }
    }
}