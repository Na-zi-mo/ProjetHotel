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
    
    public partial class AYANT
    {
        public int Id_Ayant { get; set; } = -1;
        public Nullable<int> Id_Chambre { get; set; }
        public string Code_Commodite { get; set; }
    
        public virtual COMMODITE COMMODITE { get; set; }
        public virtual CHAMBRE CHAMBRE { get; set; }
    }
}
