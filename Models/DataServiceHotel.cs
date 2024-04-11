using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
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

            LoadAll();

        }

        public void LoadAll()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                LoadClients(connection);
                LoadChambres(connection);
                LoadReserves(connection);
                //LoadDe(connection);

                LoadArrives(connection);

            }
        }

        private void LoadArrives(SqlConnection connection)
        {
            throw new NotImplementedException();
        }

        private void LoadReserves(SqlConnection connection)
        {
            Reserves = new ObservableCollection<RESERVE>();

            string queryString = "SELECT * FROM dbo.RESERVE";

            SqlCommand command = new SqlCommand(queryString, connection);

            using (SqlDataReader reader = command.ExecuteReader())
            {

                while (reader.Read())
                {
                    RESERVE reserve = new RESERVE();

                    ReaderToReserve(reader, reserve);

                    Reserves.Add(reserve);
                }
            }
        }

        private void ReaderToReserve(SqlDataReader reader, RESERVE reserve)
        {
            reserve.Id_Reserve = (Int32)reader["Id_Reserve"];
            reserve.Id_Client = (Int32)reader["Id_Client"];

            reserve.Date_Reserve = (DateTime)reader["Date_Reserve"];

            reserve.Etage = (int)reader["Etage"];
            reserve.Prix = !reader.IsDBNull(reader.GetOrdinal("Prix")) ? (decimal?)reader["Prix"] : null;
            reserve.Etat = (bool)reader["Etat"];
            reserve.Code_Typereserve = (string)reader["Code_Typereserve"];
            reserve.Code_Localisation = (string)reader["Code_Localisation"];
            reserve.Memo = (string)reader["Memo"];
        }

        private void LoadChambres(SqlConnection connection)
        {
            Chambres = new ObservableCollection<CHAMBRE>();

            string queryString = "SELECT * FROM dbo.CHAMBRE";

            SqlCommand command = new SqlCommand(queryString, connection);

            using (SqlDataReader reader = command.ExecuteReader())
            {

                while (reader.Read())
                {
                    CHAMBRE chambre = new CHAMBRE();

                    ReaderToChambre(reader, chambre);

                    Chambres.Add(chambre);
                }
            }
        }

        private void ReaderToChambre(SqlDataReader reader, CHAMBRE chambre)
        {
            chambre.Id_Chambre = (Int32)reader["Id_Chambre"];
            chambre.Etage = (int)reader["Etage"];
            chambre.Prix = !reader.IsDBNull(reader.GetOrdinal("Prix")) ? (decimal?)reader["Prix"] : null;
            chambre.Etat = (bool)reader["Etat"];
            chambre.Code_TypeChambre = (string)reader["Code_TypeChambre"];
            chambre.Code_Localisation = (string)reader["Code_Localisation"];
            chambre.Memo = (string)reader["Memo"];
        }

        private void LoadClients(SqlConnection connection)
        {
            Clients = new ObservableCollection<CLIENT>();

            string queryString = "SELECT * FROM dbo.CLIENT";

            SqlCommand command = new SqlCommand(queryString, connection);

            using (SqlDataReader reader = command.ExecuteReader())
            {

                while (reader.Read())
                {
                    CLIENT client = new CLIENT();

                    ReaderToClient(reader, client);

                    Clients.Add(client);
                }
            }
        }

        private void ReaderToClient(SqlDataReader reader, CLIENT client)
        {
            client.Id_Client = (Int32)reader["Id_Client"];
            client.Nom = (string)reader["Nom"];
            client.Adresse = (string)reader["Adresse"];
            client.Telephone = (string)reader["Telephone"];
            client.Num_Carte = (string)reader["Num_Carte"];
            client.Type_Carte = (string)reader["Type_Carte"];
            client.Date_Exp = (DateTime)reader["Date_Exp"];
            client.Solde_Du = !reader.IsDBNull(reader.GetOrdinal("Sole_Du")) ? (decimal?)reader["Sole_Du"] : null;
        }
    }
}
