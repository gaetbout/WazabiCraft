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
    
    public partial class JoueurPartie
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public JoueurPartie()
        {
            this.Des = new HashSet<De>();
            this.Cartes = new HashSet<Carte>();
        }
    
        public int Id { get; set; }
        public int Ordre { get; set; }
        public int Joueur_Id { get; set; }
        public int NbSkips { get; set; }
    
        public virtual Joueur Joueur { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<De> Des { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Carte> Cartes { get; set; }
    }
}
