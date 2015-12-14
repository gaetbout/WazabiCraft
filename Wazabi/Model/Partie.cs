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
    
    public partial class Partie
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Partie()
        {
            this.Cartes = new HashSet<Carte>();
            this.JoueurParties1 = new HashSet<JoueurPartie>();
        }
    
        public int Id { get; set; }
        public string DateHeureCreation { get; set; }
        public string Sens { get; set; }
        public string Etat { get; set; }
        public int JoueurCourant_Id { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Carte> Cartes { get; set; }
        public virtual JoueurPartie JoueurParties { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<JoueurPartie> JoueurParties1 { get; set; }
        public virtual Joueur Createur { get; set; }
        public virtual Joueur Vainqueur { get; set; }
    }
}
