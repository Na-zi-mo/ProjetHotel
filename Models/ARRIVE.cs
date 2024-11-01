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

    public partial class ARRIVE
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ARRIVE()
        {
            this.DEPART = new ObservableCollection<DEPART>();
            this.TRX = new ObservableCollection<TRX>();
        }

        public int Id_Arrive { get; set; } = -1;
        public Nullable<int> Id_Reserve { get; set; }
        public Nullable<int> Id_Client { get; set; }
        public Nullable<System.DateTime> Date_Arrive { get; set; } = DateTime.Today;
        public Nullable<int> Id_Chambre { get; set; }
        public string Recu_Par { get; set; }

        public virtual CHAMBRE CHAMBRE { get; set; }
        public virtual CLIENT CLIENT { get; set; }
        public virtual RESERVE RESERVE { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ObservableCollection<DEPART> DEPART { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ObservableCollection<TRX> TRX { get; set; }

        public ARRIVE(int? id_Reserve, int? id_Client, DateTime? date_Arrive, int? id_Chambre, string recu_Par = "null")
        {
            Id_Reserve = id_Reserve;
            Id_Client = id_Client;
            Date_Arrive = date_Arrive;
            Id_Chambre = id_Chambre;
            Recu_Par = recu_Par;
        }
        public ARRIVE(DateTime? date_Arrive, string recu_Par, CHAMBRE cHAMBRE, int? id_Chambre, CLIENT cLIENT, int? id_Client, RESERVE rESERVE, int? id_Reserve)// ObservableCollection<DEPART> dEPART, ObservableCollection<TRX> tRX
        {
            Id_Reserve = id_Reserve;
            Id_Client = id_Client;
            Date_Arrive = date_Arrive;
            Id_Chambre = id_Chambre;
            Recu_Par = recu_Par;
            CHAMBRE = cHAMBRE;
            CLIENT = cLIENT;
            RESERVE = rESERVE;
        }
        public ARRIVE(int id_Arrive, int? id_Reserve, int? id_Client, DateTime? date_Arrive, int? id_Chambre, string recu_Par, CHAMBRE cHAMBRE, CLIENT cLIENT, RESERVE rESERVE, ObservableCollection<DEPART> dEPART, ObservableCollection<TRX> tRX)
        {
            Id_Arrive = id_Arrive;
            Id_Reserve = id_Reserve;
            Id_Client = id_Client;
            Date_Arrive = date_Arrive;
            Id_Chambre = id_Chambre;
            Recu_Par = recu_Par;
            CHAMBRE = cHAMBRE;
            CLIENT = cLIENT;
            RESERVE = rESERVE;
            DEPART = dEPART;
            TRX = tRX;
        }
    }
}
