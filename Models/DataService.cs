using hotel24;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace hotel24Eq5.Models
{
    public class DataService
    {
        //string  connectionString = @"Data Source=155.138.137.213;Initial Catalog=db_erick;Persist Security Info=True;User ID=erick;Password=EVxrq47wDrjQp6Ty";


        string connectionString = @"Data Source=155.138.137.213;Initial Catalog=db_erick;Persist Security Info=True;User ID=erick;Password=EVxrq47wDrjQp6Ty";

        private ObservableCollection<COMMODITE> commodites;
        private ObservableCollection<LOCALISATION> localisations;
        private ObservableCollection<TYPECHAMBRE> typeChambres;
        private ObservableCollection<CHAMBRE> chambres;
        private ObservableCollection<DE> des;
        private ObservableCollection<CLIENT> clients;
        private ObservableCollection<RESERVE> reserves;
        private ObservableCollection<ARRIVE> arrives;
        private ObservableCollection<DEPART> departs;
        private ObservableCollection<TYPETRX> typeTrxs;
        private ObservableCollection<TRX> trxs;

        public ObservableCollection<COMMODITE> Commodites
        {
            get => commodites;
            set => commodites = value;
        }
        public ObservableCollection<LOCALISATION> Localisations
        {
            get => localisations;
            set => localisations = value;
        }
        public ObservableCollection<TYPECHAMBRE> TypeChambres
        {
            get => typeChambres;
            set => typeChambres = value;
        }
        public ObservableCollection<CHAMBRE> Chambres
        {
            get => chambres;
            set => chambres = value;
        }
        public ObservableCollection<CLIENT> Clients
        {
            get => clients;
            set => clients = value;
        }
        public ObservableCollection<RESERVE> Reserves
        {
            get => reserves;
            set => reserves = value;
        }
        public ObservableCollection<ARRIVE> Arrives
        {
            get => arrives;
            set => arrives = value;
        }
        public ObservableCollection<DEPART> Departs
        {
            get => departs;
            set => departs = value;
        }
        public ObservableCollection<TYPETRX> TypeTrxs
        {
            get => typeTrxs;
            set => typeTrxs = value;
        }
        public ObservableCollection<TRX> Trxs
        {
            get => trxs;
            set => trxs = value;
        }
        public ObservableCollection<DE> Des
        { get => des; set => des = value; }

        public DataService()
        {
            //if (Environment.MachineName == "DESKTOP-RHHOCM0")
            //{
            //    connectionString = @"Data Source=DESKTOP-RHHOCM0\SQLEXPRESS; Initial Catalog = bdhotel24; Integrated Security = True";
            //}
            //else

            //{
            //    if (!connectionString.Contains(Environment.MachineName.ToLower()))
            //    {
            //        MessageBox.Show("Ta connectionstring est encore la mienne je pense" + connectionString);

            //    }

            //}
            LoadAll();

        }

        public void LoadAll()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // ou connection.ConnectionString = connectionString;
                connection.Open();

                LoadChambres(connection);
                LoadArrives(connection);
                LoadClients(connection);
                LoadReserves(connection);


            }
        }

        private void LoadClients(SqlConnection connection)
        {
            Clients = new ObservableCollection<CLIENT>();
            string queryString = "SELECT * FROM CLIENT";
            SqlCommand command = new SqlCommand(queryString, connection);


            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    CLIENT client = new CLIENT();

                    ReaderToOneClient(reader, client);

                    Clients.Add(client);

                }
            }
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

                    ReaderToOneChambre(reader, chambre);

                    Chambres.Add(chambre);

                }
            }
        }

        private void ReaderToOneChambre(SqlDataReader reader, CHAMBRE chambre)
        {
            chambre.Id_Chambre = (int)reader["Id_Chambre"];
            chambre.Etage = (int)reader["Etage"];
            chambre.Prix = (Decimal)reader["Prix"];
            chambre.Etat = (bool)reader["Etat"];
            chambre.Code_TypeChambre = (String)reader["Code_TypeChambre"];
            chambre.Code_Localisation = (String)reader["Code_Localisation"];
            chambre.Memo = (String)reader["Memo"];

        }

        private void ReaderToOneClient(SqlDataReader reader, CLIENT client)
        {
            client.Nom = (String)reader["Nom"];
            client.Adresse = (String)reader["Adresse"];
            client.Id_Client = (Int32)reader["Id_Client"];
            client.Telephone = (String)reader["Telephone"];
            client.Num_Carte = (String)reader["Num_Carte"];
            client.Type_Carte = (String)reader["Type_Carte"];
            client.Date_Exp = (DateTime)reader["Date_Exp"];
            client.Solde_Du = (Decimal)reader["Solde_Du"];
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

                    ReaderToOneArrive(reader, arrive);

                    Arrives.Add(arrive);

                }
            }
        }

        private void ReaderToOneArrive(SqlDataReader reader, ARRIVE arrive)
        {
            arrive.Id_Arrive = (Int32)reader["Id_Arrive"];
            arrive.Id_Reserve = (Int32)reader["Id_Reserve"];
            arrive.Id_Client = (Int32)reader["Id_Client"];
            arrive.Date_Arrive = (DateTime)reader["Date_Arrive"];
            arrive.Id_Chambre = (Int32)reader["Id_Chambre"];
            arrive.Recu_Par = (String)reader["Recu_Par"];
        }

        private void LoadDes(SqlConnection connection, RESERVE reserve)
        {
            Des = new ObservableCollection<DE>();
            string queryString = "SELECT * FROM DE WHERE @Id_Reserve=Id_Reserve";
            SqlCommand command = new SqlCommand(queryString, connection);
            command.Parameters.AddWithValue("@Id_Reserve", reserve.Id_Reserve);


            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    DE de = new DE();
                    ReaderToOneDe(reader, de);

                    reserve.DE.Add(de);
                    de.CHAMBRE = Chambres.FirstOrDefault(i => i.Id_Chambre == de.Id_Chambre);
                    // MessageBox.Show(de.CHAMBRE.Id_Chambre.ToString());


                }
            }
        }

        private void ReaderToOneDe(SqlDataReader reader, DE de)
        {
            de.Id_De = (int)reader["Id_De"];
            de.Id_Reserve = (int)reader["Id_Reserve"];
            de.Id_Chambre = (int)reader["Id_Chambre"];
            de.Attribuee = (bool)reader["Attribuee"];
        }

        public void LoadReserves(SqlConnection connection)
        {
            Reserves = new ObservableCollection<RESERVE>();

            string queryString = "SELECT * FROM RESERVE";
            SqlCommand command = new SqlCommand(queryString, connection);
            using (SqlDataReader reader = command.ExecuteReader())
            {

                while (reader.Read())
                {
                    RESERVE reserve = new RESERVE();


                    ReaderToOneReserve(reader, reserve);
                    reserve.CLIENT = Clients.FirstOrDefault(i => i.Id_Client == reserve.Id_Client);



                    Reserves.Add(reserve);
                }
            }
            foreach (var reserve in Reserves)
            {
                reserve.ARRIVE.Add( Arrives.FirstOrDefault(i => i.Id_Reserve == reserve.Id_Reserve));

                LoadDes(connection, reserve);
                

            }
        }


        public void ReaderToOneReserve(SqlDataReader reader, RESERVE reserve)
        {

            reserve.Id_Reserve = (Int32)reader["Id_Reserve"];
            reserve.Id_Client = (Int32)reader["Id_Client"];
            reserve.Date_Reserve = (DateTime)reader["Date_Reserve"];
            reserve.Date_Debut = (DateTime)reader["Date_Debut"];
            reserve.Date_Fin = (DateTime)reader["Date_Fin"];
            reserve.Confirme = (bool)reader["Confirme"];
        }

        public void Save(RESERVE reserve)
        {
            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = connectionString;
                connection.Open();
                try {
                    DeleteAllDeReserve(connection, reserve);

                    if (reserve.Id_Reserve == -1)
                    {
                        InsertReserve(connection, reserve);
                    }
                    else
                    {
                        UpdateReserve(connection, reserve);
                        // Vérifier si la liste DE est vide
                        //if (reserve.DE != null && reserve.DE.Count > 1)
                        //{

                        foreach (var c in reserve.DE)
                        {
                            string queryString = "SELECT COUNT(*) FROM DE " +
                            "WHERE Id_Reserve = @Id_Reserve AND Id_Chambre = @Id_Chambre AND Id_De <> @Id_De";
                            SqlCommand command = new SqlCommand(queryString, connection);
                            command.Parameters.AddWithValue("@Id_Reserve", c.Id_Reserve);
                            command.Parameters.AddWithValue("@Id_Chambre", c.Id_Chambre);
                            command.Parameters.AddWithValue("@Id_De", c.Id_De);

                            int result = (int)command.ExecuteScalar();
                            if (result > 0)
                            {
                                UpdateDeReserve(connection, reserve, c);
                            }
                            else
                            {
                                InsertDeReserve(connection, reserve, c);
                            }
                        }

                        //    }
                        //    else
                        //    {
                        //        // Afficher un message à l'utilisateur pour lui demander d'insérer au moins un détail de réservation
                        //        // Par exemple, vous pouvez utiliser MessageBox ou tout autre moyen d'interface utilisateur pour afficher le message.
                        //        MessageBox.Show("Veuillez insérer au moins une chambre a reserver avant de sauvegarder.");
                        //    }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Une erreur est survenue : " + ex.Message);
                }
            }
        }


        private void UpdateReserve(SqlConnection connection, RESERVE reserve)
        {


            string queryString = "UPDATE RESERVE " +
                  "SET Id_Client=@Id_Client, " +
                  "Date_Reserve = @Date_Reserve, " +
                  "Date_Debut = @Date_Debut, " +
                  "Date_Fin = @Date_Fin, " +
                  "Confirme = @Confirme " +
                  "WHERE Id_Reserve=@Id_Reserve";


            SqlCommand command = new SqlCommand(queryString, connection);
            //  moins compliqué que le tableau mais je voulais vous montrer les tableaux aussi
            command.Parameters.AddWithValue("@Id_Reserve", reserve.Id_Reserve);
            command.Parameters.AddWithValue("@Id_Client", reserve.Id_Client);
            command.Parameters.AddWithValue("@Date_Reserve", reserve.Date_Reserve);
            command.Parameters.AddWithValue("@Date_Debut", reserve.Date_Debut);
            command.Parameters.AddWithValue("@Date_Fin", reserve.Date_Fin);
            command.Parameters.AddWithValue("@Confirme", reserve.Confirme);
            command.ExecuteNonQuery();

        }
        private void DeleteAllDeReserve(SqlConnection connection, RESERVE reserve)
        {
            string queryString = "DELETE FROM DE WHERE Id_Reserve = @Id_Reserve";
            SqlCommand command = new SqlCommand(queryString, connection);
            command.Parameters.AddWithValue("@Id_Reserve", reserve.Id_Reserve);
            command.ExecuteNonQuery();
        }
        private void InsertReserve(SqlConnection connection, RESERVE reserve)
        {
            //connection.ConnectionString = connectionString;
            //connection.Open();
            String queryString = "INSERT INTO RESERVE (Id_Client, Date_Reserve, Date_Debut, Date_Fin, Confirme) VALUES (@Id_Client,  @Date_Reserve, @Date_Debut,@Date_Fin, @Confirme)";
            using (SqlCommand command = new SqlCommand(queryString, connection))
            {

                var parameters = new SqlParameter[5];
                parameters[0] = new SqlParameter("@Id_Client", reserve.Id_Client);
                parameters[1] = new SqlParameter("@Date_Reserve", reserve.Date_Reserve);
                parameters[2] = new SqlParameter("@Date_Debut", reserve.Date_Debut);
                parameters[3] = new SqlParameter("@Date_Fin", reserve.Date_Fin);
                parameters[4] = new SqlParameter("@Confirme", reserve.Confirme);

                
                var result = 0;
                command.CommandText = queryString;
                command.Parameters.AddRange(parameters);
                result = command.ExecuteNonQuery();

            }
            queryString = "SELECT IDENT_CURRENT('RESERVE')";
            using (SqlCommand command = new SqlCommand(queryString, connection))
            {
                reserve.Id_Reserve = Convert.ToInt32(command.ExecuteScalar());


                foreach (var de in reserve.DE)
                {
                    de.Id_Reserve = reserve.Id_Reserve; ///*****
                    InsertDeReserve(connection, reserve, de);
                }
            }



        }
    
        
        
        private void InsertDeReserve(SqlConnection connection, RESERVE reserve, DE de)
        {
            string queryString = "INSERT INTO DE (Id_Reserve,Id_Chambre,Attribuee) " +
                                 "VALUES (@Id_Reserve, @Id_Chambre, @Attribuee)";
            var parameters = new SqlParameter[3];

            parameters[0] = new SqlParameter("@Id_Reserve", DbType.Int32){Value =de.Id_Reserve};
            parameters[1] = new SqlParameter("@Id_Chambre", DbType.Int32) {Value =de.Id_Chambre};
            parameters[2] = new SqlParameter("@Attribuee", DbType.Boolean){Value =de.Attribuee};
            SqlCommand command = new SqlCommand(queryString, connection);

            var result = 0;
            command.CommandText = queryString;
            command.Parameters.AddRange(parameters);
            result = command.ExecuteNonQuery();

        }
        //A REVOIR
        private void UpdateDeReserve(SqlConnection connection, RESERVE reserve, DE de)
        {
            string queryString = "UPDATE CONTIENT " +
                         "SET Id_Reserve = @Id_Reserve, Id_Chambre = @Id_Chambre, Attribuee = @Attribuee " +
                         "WHERE Id_De = @Id_De";
            //A REVOIR
            var parameters = new SqlParameter[4];
            parameters[0] = new SqlParameter("@Id_De", DbType.Int32) { Value = de.Id_De };
            parameters[1] = new SqlParameter("@Id_Reserve", DbType.Int32) { Value = de.Id_Reserve };
            parameters[2] = new SqlParameter("@Id_Chambre", DbType.Int32) { Value = de.Id_Chambre };
            parameters[3] = new SqlParameter("@Attribuee", DbType.Boolean) { Value = de.Attribuee };
            SqlCommand command = new SqlCommand(queryString, connection);
            var result = 0;
            command.CommandText = queryString;
            command.Parameters.AddRange(parameters);
            result = command.ExecuteNonQuery();
        }

        public void Reload(RESERVE reserve)
        {
            reserve.DE.Clear();
            string queryString = "SELECT * FROM RESERVE " +
            "WHERE(RESERVE.Id_Reserve = @Id_Reserve)";
            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = connectionString;
                connection.Open();
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@Id_Reserve", reserve.Id_Reserve);
                command.ExecuteNonQuery();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ReaderToOneReserve(reader, reserve);
                    }
                } // fin using reader
                reserve.CLIENT = Clients.FirstOrDefault(a => a.Id_Client == reserve.Id_Client);
                LoadDes(connection, reserve); // donc doit vider la liste au

            }
        }



        public void DeleteReserve(RESERVE reserve)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                try
                {
                    // Delete TRX 
                    string queryString = "DELETE FROM TRX WHERE Id_Arrive IN (SELECT Id_Arrive FROM ARRIVE WHERE Id_Reserve = @Id_Reserve)";
                    using (SqlCommand command = new SqlCommand(queryString, connection))
                    {
                        command.Parameters.AddWithValue("@Id_Reserve", reserve.Id_Reserve);
                        command.ExecuteNonQuery();
                    }

                    // Delete related DEPART records
                    queryString = "DELETE FROM DEPART WHERE Id_Arrive IN (SELECT Id_Arrive FROM ARRIVE WHERE Id_Reserve = @Id_Reserve)";
                    using (SqlCommand command = new SqlCommand(queryString, connection))
                    {
                        command.Parameters.AddWithValue("@Id_Reserve", reserve.Id_Reserve);
                        command.ExecuteNonQuery();
                    }

                    // Delete ARRIVE records
                    queryString = "DELETE FROM ARRIVE WHERE Id_Reserve = @Id_Reserve";
                    using (SqlCommand command = new SqlCommand(queryString, connection))
                    {
                        command.Parameters.AddWithValue("@Id_Reserve", reserve.Id_Reserve);
                        command.ExecuteNonQuery();
                    }

                    // Delete DE records
                    queryString = "DELETE FROM DE WHERE Id_Reserve = @Id_Reserve";
                    using (SqlCommand command = new SqlCommand(queryString, connection))
                    {
                        command.Parameters.AddWithValue("@Id_Reserve", reserve.Id_Reserve);
                        command.ExecuteNonQuery();
                    }

                    // Delete reservation
                    queryString = "DELETE FROM RESERVE WHERE Id_Reserve = @Id_Reserve";
                    using (SqlCommand command = new SqlCommand(queryString, connection))
                    {
                        command.Parameters.AddWithValue("@Id_Reserve", reserve.Id_Reserve);
                        command.ExecuteNonQuery();
                    }

                    MessageBox.Show("La suppression de la réservation et de ses enregistrements associés a été réalisée avec succès.", "Suppression réussie", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    throw new Exception("Erreur de suppression: " + ex.Message);
                }
            }
        }



        public void DeleteDeReserve(RESERVE reserve, DE de)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
               
                    connection.Open();

                    string queryString = "DELETE FROM DE " +
                                         "WHERE Id_Reserve = @Id_Reserve AND Id_Chambre = @Id_Chambre AND Id_De = @Id_De";

                    SqlCommand command = new SqlCommand(queryString, connection);
                    command.Parameters.AddWithValue("@Id_Reserve", de.Id_Reserve);
                    command.Parameters.AddWithValue("@Id_Chambre", de.Id_Chambre);
                    command.Parameters.AddWithValue("@Id_De", de.Id_De);

                    command.ExecuteNonQuery();
                

            }
        }
        public int GetNbDe() // return le dernier numéro de Bon
        {
            return -1;
            int derniernumde = -1;
            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = connectionString;
                try
                {
                    SqlCommand command = new SqlCommand(
                    "SELECT IDENT_CURRENT('DE')", connection);
                    connection.Open();
                    derniernumde = Convert.ToInt32(command.ExecuteScalar());
                    connection.Close();
                    //MessageBox.Show(derniernumde+1.ToString());
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            return derniernumde + 1;
        }



    }
}
