//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré à partir d'un modèle.
//
//     Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//     Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace hotel24
{
    using System;
    using System.Collections.ObjectModel;
    
    public partial class CLIENT
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CLIENT()
        {
            this.ARRIVE = new ObservableCollection <ARRIVE>();
            this.RESERVE = new ObservableCollection <RESERVE>();
        }
    
        public int Id_Client { get; set; }
        public string Nom { get; set; }
        public string Adresse { get; set; }
        public string Telephone { get; set; }
        public string Num_Carte { get; set; }
        public string Type_Carte { get; set; }
        public Nullable<System.DateTime> Date_Exp { get; set; }
        public Nullable<decimal> Solde_Du { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ObservableCollection<ARRIVE> ARRIVE { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ObservableCollection<RESERVE> RESERVE { get; set; }
    }
}
