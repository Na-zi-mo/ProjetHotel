//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré à partir d'un modèle.
//
//     Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//     Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace hotel24Eq5.Models
{
    using System;
    using System.Collections.ObjectModel;
    
    public partial class DE
    {
        public int Id_De { get; set; }
        public Nullable<int> Id_Reserve { get; set; }
        public Nullable<int> Id_Chambre { get; set; }
        // 
        public Nullable<bool> Attribuee { get; set; }
    
        public virtual CHAMBRE CHAMBRE { get; set; }
        public virtual RESERVE RESERVE { get; set; }
    }
}
