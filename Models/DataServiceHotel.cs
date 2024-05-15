using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;


namespace hotel24Eq5.Models
{
    public class DataServiceHotel
    {
        string connectionString;



        ObservableCollection<CLIENT> listeClients;
        public ObservableCollection<CLIENT> ListeClients
        {
            get => listeClients;
            set
            {
                listeClients = value;
            }
        }


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

        private ObservableCollection<DE> des;
        public ObservableCollection<DE> Des
        {
            get => des;
            set
            {
                des = value;


            }
        }
        #endregion
        public DataServiceHotel()
        {
            connectionString = @"Data Source=LAPTOP-BA8LUHNR\SQLEXPRESS;Initial Catalog=bdhotel24;Integrated Security=True;Connect Timeout=30;Encrypt=False";
            connectionString = @"Data Source=155.138.137.213;Initial Catalog=db_erick;Persist Security Info=True;User ID=erick;Password=EVxrq47wDrjQp6Ty";
            LoadAll();

        }

        public void LoadAll()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                LoadClients(connection);

                LoadTypeCahmbres(connection);
                LoadChambres(connection);

                LoadReserves(connection);

                LoadArrives(connection);

            }
        }
        #region Actions
        public void Delete(ARRIVE arrive)
        {
            using (SqlConnection connection = new SqlConnection())
            {

                connection.ConnectionString = connectionString;
                connection.Open();

                string queryString = "DELETE TRX " +
                                   "WHERE(TRX.Id_Arrive = @Id_Arrive)";
                SqlCommand command = new SqlCommand(queryString, connection);

                command.Parameters.AddWithValue("@Id_Arrive", arrive.Id_Arrive);
                command.ExecuteNonQuery();

                queryString = "DELETE DEPART " +
                                   "WHERE(DEPART.Id_Arrive = @Id_Arrive)";
                command = new SqlCommand(queryString, connection);

                command.Parameters.AddWithValue("@Id_Arrive", arrive.Id_Arrive);
                command.ExecuteNonQuery();

                queryString = "DELETE ARRIVE " +
                                      "WHERE(ARRIVE.Id_Arrive = @Id_Arrive)";
                command = new SqlCommand(queryString, connection);

                command.Parameters.AddWithValue("@Id_Arrive", arrive.Id_Arrive);
                command.ExecuteNonQuery();



                queryString = "UPDATE DE" +
                   " SET Attribuee = @Attribuee " +
                   "WHERE Id_Chambre=@Id_Chambre";

                SqlCommand command2 = new SqlCommand(queryString, connection);

                command2.Parameters.AddWithValue("@Attribuee", 0);
                command2.Parameters.AddWithValue("@Id_Chambre", arrive.Id_Chambre);

                command2.ExecuteNonQuery();

            }

        }
        public void Save(ARRIVE arrive)
        {
            if (arrive.Id_Arrive == -1)
            {
                using (SqlConnection connection = new SqlConnection())
                {
                    connection.ConnectionString = connectionString;
                    connection.Open();
                    InsertArrive(connection, arrive);
                    connection.Close();
                }
            }
            else
            {
                UpdateArrive(arrive);
            }
        }


        private void InsertArrive(SqlConnection connection, ARRIVE arrive)
        {

            String queryString = "INSERT INTO ARRIVE (Id_Reserve, Id_Client, Date_Arrive, Id_Chambre, Recu_Par) VALUES (@Id_Reserve,  @Id_Client, @Date_Arrive, @Id_Chambre, @Recu_Par)";
            using (SqlCommand command = new SqlCommand(queryString, connection))
            {

                command.Parameters.AddWithValue("@Id_Reserve", arrive.Id_Reserve);
                command.Parameters.AddWithValue("@Id_Client", arrive.Id_Client);
                command.Parameters.AddWithValue("@Date_Arrive", arrive.Date_Arrive);
                command.Parameters.AddWithValue("@Id_Chambre", arrive.Id_Chambre);
                command.Parameters.AddWithValue("@Recu_Par", arrive.Recu_Par);

                command.ExecuteNonQuery();
            }

            queryString = "SELECT IDENT_CURRENT('ARRIVE')";

            using (SqlCommand command = new SqlCommand(queryString, connection))
            {
                arrive.Id_Arrive = Convert.ToInt32(command.ExecuteScalar());


                foreach (var item in arrive.RESERVE.DE)
                {
                    queryString = "UPDATE DE" +
                      " SET Attribuee=@Attribuee " +
                      "WHERE(Id_De=@Id_De)";


                    SqlCommand command2 = new SqlCommand(queryString, connection);

                    command2.Parameters.AddWithValue("@Attribuee", item.Attribuee);

                    command2.Parameters.AddWithValue("@Id_De", item.Id_De);

                    command2.ExecuteNonQuery();
                }
            }
        }

        public void UpdateArrive(ARRIVE arrive)
        {
            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = connectionString;
                connection.Open();

                string queryString = "UPDATE ARRIVE" +
                    " SET Id_Chambre = @Id_Chambre, " + "Recu_Par = @Recu_Par " +
                    "WHERE Id_Arrive=@Id_Arrive";

                SqlCommand command = new SqlCommand(queryString, connection);

                command.Parameters.AddWithValue("@Id_Chambre", arrive.Id_Chambre);
                command.Parameters.AddWithValue("@Recu_Par", arrive.Recu_Par);
                command.Parameters.AddWithValue("@Id_Arrive", arrive.Id_Arrive);

                var result = 0;
                try
                {

                    result = command.ExecuteNonQuery();
                    if (result > 0)
                    {
                        foreach (var item in arrive.RESERVE.DE)
                        {
                            queryString = "UPDATE DE" +
                              " SET Attribuee=@Attribuee " +
                              "WHERE(Id_De=@Id_De)";


                            SqlCommand command2 = new SqlCommand(queryString, connection);

                            command2.Parameters.AddWithValue("@Attribuee", item.Attribuee);

                            command2.Parameters.AddWithValue("@Id_De", item.Id_De);

                            command2.ExecuteNonQuery();
                        }

                    }

                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }

            }
        }


        public void ReloadArrive(ARRIVE arrive)
        {
            int index = Arrives.IndexOf(arrive);
            Arrives.RemoveAt(index);

            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = connectionString;
                connection.Open();

                string queryString = "SELECT * " +
                  "FROM ARRIVE " +
                  "WHERE Id_Arrive=@Id_Arrive";

                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@Id_Arrive", arrive.Id_Arrive);
                command.ExecuteNonQuery();

                using (SqlDataReader reader = command.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        ARRIVE arr = new ARRIVE();
                        ReaderToArrive(reader, arrive);

                        Arrives.Insert(index, arrive);
                    }

                }
                foreach (var arr in arrives)
                {
                    arr.CLIENT = Clients.FirstOrDefault(i => i.Id_Client == arr.Id_Client);

                    arr.CHAMBRE = Chambres.FirstOrDefault(i => i.Id_Chambre == arr.Id_Chambre);

                    arr.RESERVE = Reserves.FirstOrDefault(i => i.Id_Reserve == arr.Id_Reserve);

                    LoadTrxs(connection, arr);
                    LoadDeparts(connection, arr);
                }
            }
        }
        #endregion
        #region Loaders
        private void LoadTypeCahmbres(SqlConnection connection)
        {

            TypesChambres = new ObservableCollection<TYPECHAMBRE>();

            string queryString = "SELECT * FROM TYPECHAMBRE";

            SqlCommand command = new SqlCommand(queryString, connection);
            command.ExecuteNonQuery();

            using (SqlDataReader reader = command.ExecuteReader())
            {

                while (reader.Read())
                {
                    TYPECHAMBRE typeChambre = new TYPECHAMBRE();

                    ReaderToTypeChambre(reader, typeChambre);
                    TypesChambres.Add(typeChambre);
                }
            }
        }

        private void ReaderToTypeChambre(SqlDataReader reader, TYPECHAMBRE typeChambre)
        {
            typeChambre.Nbr_Dispo = (Int32)reader["Nbr_Dispo"];
            typeChambre.Code_TypeChambre = (string)reader["Code_TypeChambre"];
            typeChambre.Description = (string)reader["Description"];
        }
        private void LoadDes(SqlConnection connection, RESERVE reserve)
        {
            string queryString = "SELECT * FROM DE WHERE Id_Reserve=@Id_Reserve";

            SqlCommand command = new SqlCommand(queryString, connection);
            command.Parameters.AddWithValue("@Id_Reserve", reserve.Id_Reserve);
            command.ExecuteNonQuery();

            using (SqlDataReader reader = command.ExecuteReader())
            {

                while (reader.Read())
                {
                    DE de = new DE();

                    ReaderToDe(reader, de);
                    de.CHAMBRE = Chambres.FirstOrDefault(i => i.Id_Chambre == de.Id_Chambre);
                    reserve.DE.Add(de);
                }
            }
        }

        private void ReaderToDe(SqlDataReader reader, DE de)
        {
            de.Id_De = (Int32)reader["Id_De"];
            de.Id_Reserve = (Int32)reader["Id_Reserve"];
            de.Id_Chambre = (Int32)reader["Id_Chambre"];

            de.Attribuee = (bool)reader["Attribuee"];
        }

        private void LoadTrxs(SqlConnection connection, ARRIVE arrive)
        {
            Trxs = new ObservableCollection<TRX>();

            string queryString = "SELECT * FROM TRX " +
                "WHERE Id_Arrive=@Id_Arrive";

            SqlCommand command = new SqlCommand(queryString, connection);
            command.Parameters.AddWithValue("@Id_Arrive", arrive.Id_Arrive);

            using (SqlDataReader reader = command.ExecuteReader())
            {

                while (reader.Read())
                {
                    TRX trx = new TRX();

                    ReaderToTrx(reader, trx);

                    Trxs.Add(trx);
                }
            }
        }

        private void ReaderToTrx(SqlDataReader reader, TRX trx)
        {
            trx.Id_Trx = (Int32)reader["Id_Trx"];
            trx.Id_Arrive = (Int32)reader["Id_Arrive"];

            trx.Date_Trx = (DateTime)reader["Date_Trx"];
            trx.Code_TypeTrx = (string)reader["Code_TypeTrx"];

            trx.Montant = !reader.IsDBNull(reader.GetOrdinal("Montant")) ? (decimal?)reader["Montant"] : null;

            trx.Conf_Par = (string)reader["Conf_Par"];
            trx.Reportee = (bool)reader["Reportee"];
        }

        private void LoadDeparts(SqlConnection connection, ARRIVE arrive)
        {
            Departs = new ObservableCollection<DEPART>();

            string queryString = "SELECT * FROM DEPART " + "WHERE Id_Arrive=@Id_Arrive";

            SqlCommand command = new SqlCommand(queryString, connection);
            command.Parameters.AddWithValue("@Id_Arrive", arrive.Id_Arrive);
            using (SqlDataReader reader = command.ExecuteReader())
            {

                while (reader.Read())
                {
                    DEPART depart = new DEPART();

                    ReaderToDepart(reader, depart);

                    Departs.Add(depart);
                }
            }
        }

        private void ReaderToDepart(SqlDataReader reader, DEPART depart)
        {
            depart.Id_Depart = (Int32)reader["Id_Depart"];
            depart.Id_Arrive = (Int32)reader["Id_Arrive"];

            depart.Date_Depart = (DateTime)reader["Date_Depart"];
            depart.Conf_Par = (string)reader["Conf_Par"];
        }

        private void LoadArrives(SqlConnection connection)
        {
            Arrives = new ObservableCollection<ARRIVE>();

            string queryString = "SELECT * FROM ARRIVE";

            SqlCommand command = new SqlCommand(queryString, connection);

            using (SqlDataReader reader = command.ExecuteReader())
            {

                while (reader.Read())
                {
                    ARRIVE arrive = new ARRIVE();

                    ReaderToArrive(reader, arrive);

                    Arrives.Add(arrive);
                }

            }
            foreach (var arrive in arrives)
            {
                arrive.CLIENT = Clients.FirstOrDefault(i => i.Id_Client == arrive.Id_Client);

                arrive.CHAMBRE = Chambres.FirstOrDefault(i => i.Id_Chambre == arrive.Id_Chambre);

                arrive.RESERVE = Reserves.FirstOrDefault(i => i.Id_Reserve == arrive.Id_Reserve);

                LoadTrxs(connection, arrive);
                LoadDeparts(connection, arrive);
            }
        }

        private void ReaderToArrive(SqlDataReader reader, ARRIVE arrive)
        {
            arrive.Id_Arrive = (Int32)reader["Id_Arrive"];
            arrive.Id_Client = (Int32)reader["Id_Client"];
            arrive.Id_Reserve = (Int32)reader["Id_Reserve"];

            arrive.Id_Chambre = (Int32)reader["Id_Chambre"];

            arrive.Date_Arrive = (DateTime)reader["Date_Arrive"];
            arrive.Recu_Par = (string)reader["Recu_Par"];
        }

        private void LoadReserves(SqlConnection connection)
        {
            Reserves = new ObservableCollection<RESERVE>();

            string queryString = "SELECT * FROM RESERVE";

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
            foreach (var reserve in Reserves)
            {
                reserve.CLIENT = Clients.FirstOrDefault(i => i.Id_Client == reserve.Id_Client);
                LoadDes(connection, reserve);
            }
        }

        private void ReaderToReserve(SqlDataReader reader, RESERVE reserve)
        {
            reserve.Id_Reserve = (Int32)reader["Id_Reserve"];
            reserve.Id_Client = (Int32)reader["Id_Client"];

            reserve.Date_Reserve = (DateTime)reader["Date_Reserve"];
            reserve.Date_Debut = (DateTime)reader["Date_Debut"];
            reserve.Date_Fin = (DateTime)reader["Date_Fin"];

            reserve.Confirme = (bool)reader["Confirme"];
        }

        private void LoadChambres(SqlConnection connection)
        {
            Chambres = new ObservableCollection<CHAMBRE>();

            string queryString = "SELECT * FROM CHAMBRE";

            SqlCommand command = new SqlCommand(queryString, connection);

            using (SqlDataReader reader = command.ExecuteReader())
            {

                while (reader.Read())
                {
                    CHAMBRE chambre = new CHAMBRE();

                    ReaderToChambre(reader, chambre);


                    chambre.TYPECHAMBRE = TypesChambres.FirstOrDefault(i => i.Code_TypeChambre == chambre.Code_TypeChambre);
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

            string queryString = "SELECT * FROM  CLIENT";

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
            client.Solde_Du = (decimal)reader["Solde_Du"];
        }
    }
    #endregion
}
