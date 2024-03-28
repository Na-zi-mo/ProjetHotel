using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hotel24Eq5.Models
{
    public class DataServiceHotel
    {
        string connectionString;

        #region ObservableCollection

        private ObservableCollection<COMMODITE> commodites;
        public ObservableCollection<COMMODITE> Commodites
        {
            get => commodites;
            set
            {
                commodites = value;


            }
        }
        private ObservableCollection<LOCALISATION> localisations;
        public ObservableCollection<LOCALISATION> Localisations
        {
            get => localisations;
            set
            {
                localisations = value;


            }
        }
        private ObservableCollection<TYPECHAMBRE> typeChambres;
        public ObservableCollection<TYPECHAMBRE> TypesChambres
        {
            get => typeChambres;
            set
            {
                typeChambres = value;


            }
        }

        private ObservableCollection<CHAMBRE> chambres;
        public ObservableCollection<CHAMBRE> Chambres
        {
            get => chambres;
            set
            {
                chambres = value;


            }
        }
        private ObservableCollection<CLIENT> clients;
        public ObservableCollection<CLIENT> Clients
        {
            get => clients;
            set
            {
                clients = value;


            }
        }
        private ObservableCollection<RESERVE> reserves;
        public ObservableCollection<RESERVE> Reserves
        {
            get => reserves;
            set
            {
                reserves = value;


            }
        }
        private ObservableCollection<ARRIVE> arrives;
        public ObservableCollection<ARRIVE> Arrives
        {
            get => arrives;
            set
            {
                arrives = value;


            }
        }
        private ObservableCollection<DEPART> departs;
        public ObservableCollection<DEPART> Departs
        {
            get => departs;
            set
            {
                departs = value;


            }
        }
        private ObservableCollection<TYPETRX> typeTrxs;
        public ObservableCollection<TYPETRX> TypeTrxs
        {
            get => typeTrxs;
            set
            {
                typeTrxs = value;


            }
        }
        private ObservableCollection<TRX> trxs;
        public ObservableCollection<TRX> Trxs
        {
            get => trxs;
            set
            {
                trxs = value;


            }
        }
        #endregion
        public DataServiceHotel()
        {
            connectionString = @"Data Source=LAPTOP-BA8LUHNR\SQLEXPRESS;Initial Catalog=bdhotel24;Integrated Security=True;Connect Timeout=30;Encrypt=False";

            //LoadAll();

        }
    }
}
