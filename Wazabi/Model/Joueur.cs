//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré à partir d'un modèle.
//
//     Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//     Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Wazabi.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class Joueur
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Joueur()
        {
            this.JoueurParties = new HashSet<JoueurPartie>();
            this.PartiesCrees = new HashSet<Partie>();
            this.PartiesGagnées = new HashSet<Partie>();
        }
    
        public int Id { get; set; }
        public string Pseudo { get; set; }
        public byte[] MotDePasse { get; set; }
        public byte[] Sel { get; set; }
        public int CodeBloc { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<JoueurPartie> JoueurParties { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Partie> PartiesCrees { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Partie> PartiesGagnées { get; set; }
    }
}
