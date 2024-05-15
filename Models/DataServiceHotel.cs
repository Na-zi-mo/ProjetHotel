using hotel24Eq5.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Markup;

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
        private ObservableCollection<AYANT> ayants;
        public ObservableCollection<AYANT> Ayants
        {
            get => ayants;
            set
            {
                ayants = value;


            }
        }

        #endregion
        public DataServiceHotel()
        {
            connectionString = @"Data Source = laptopnath\sqlexpress; Initial Catalog = bdhotel24; Integrated Security = True;Connect Timeout=30;Encrypt=False";
			connectionString = @"Data Source=155.138.137.213;Initial Catalog=db_erick;Persist Security Info=True;User ID=erick;Password=EVxrq47wDrjQp6Ty";
			LoadAll();

		}

        #region Commands

        public List<COMMODITE> GetCommoditesNonInclusChambre(CHAMBRE c)
        {
            List<COMMODITE> commoditeNonInclusChambres = new List<COMMODITE>();
            commoditeNonInclusChambres.Clear();
            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = connectionString;
                connection.Open();

                string queryString = "SELECT * FROM COMMODITE " +
                                    "WHERE Code_Commodite NOT IN (SELECT Code_Commodite FROM AYANT " +
                                    "WHERE Id_Chambre = @Id_Chambre)";

                SqlCommand cmd = new SqlCommand(queryString, connection);

                cmd.Parameters.AddWithValue("@Id_Chambre", c.Id_Chambre);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        COMMODITE co = new COMMODITE();
                        co.Code_Commodite = (string)reader["Code_Commodite"];
                        co.Description = (string)reader["Description"];

                        commoditeNonInclusChambres.Add(co);
                    }
                }
                return commoditeNonInclusChambres;
            }
        }

        public void DeleteChambre(CHAMBRE c)
        {
            c.AYANT.Clear();
            Chambres.Remove(c);
            using (SqlConnection connection = new SqlConnection())
            {
				connection.ConnectionString = connectionString;
				connection.Open();

                string queryString = "SELECT COUNT(*) FROM DE " +
									 "WHERE Id_Chambre = @Id_Chambre";

				SqlCommand command = new SqlCommand(queryString, connection);
				command.Parameters.AddWithValue("@Id_Chambre", c.Id_Chambre);

				int result = Convert.ToInt32(command.ExecuteScalar());

				if (result > 0)
                {
                    MessageBox.Show("Tu ne peux pas supprimer une chambre qui est reservee!");                   
                    return;
                }
                else
                {
					queryString = "DELETE AYANT " +
							  "WHERE(AYANT.Id_Chambre = @Id_Chambre)";

					command = new SqlCommand(queryString, connection);

					command.Parameters.AddWithValue("@Id_Chambre", c.Id_Chambre);
					command.ExecuteNonQuery();

					queryString = "DELETE CHAMBRE " +
										  "WHERE(CHAMBRE.Id_Chambre = @Id_Chambre)";
					command = new SqlCommand(queryString, connection);
					command.Parameters.AddWithValue("@Id_Chambre", c.Id_Chambre);
					command.ExecuteNonQuery();
				}
                MessageBox.Show("La chambre a bien ete supprimee");
			}
        }

        public void Save(CHAMBRE chambre)
        {
			using (SqlConnection connection = new SqlConnection())
			{                
				connection.ConnectionString = connectionString;
				connection.Open();

				string queryString = "";
				SqlCommand cmd = new SqlCommand(queryString, connection);

				if (chambre.Id_Chambre == -1)
				{                                        
					InsertChambre(connection, chambre);
                    if (chambre.Etat == true)
                    {
						chambre.TYPECHAMBRE.Nbr_Dispo += 1;
						queryString = "UPDATE TYPECHAMBRE " +
                                            "SET Nbr_Dispo = @Nbr_Dispo " +
                                            "WHERE Id_Chambre = @Id_Chambre";    
                        
                        cmd = new SqlCommand(queryString, connection);
                        cmd.Parameters.AddWithValue("@Nbr_Dispo", chambre.TYPECHAMBRE.Nbr_Dispo);

						cmd.ExecuteNonQuery();
					}
				}
				else
				{
					queryString = "SELECT COUNT(*) FROM DE " +
									 "WHERE Id_Chambre = @Id_Chambre";

					cmd = new SqlCommand(queryString, connection);
					cmd.Parameters.AddWithValue("@Id_Chambre", chambre.Id_Chambre);

					int result = Convert.ToInt32(cmd.ExecuteScalar());
					
				    UpdateChambre(connection, chambre);
				    foreach (var a in chambre.AYANT)
				    {
					    queryString = "SELECT COUNT(*) FROM AYANT " +
										    "WHERE (AYANT.Id_Chambre = @Id_Chambre AND AYANT.Id_Ayant = @Id_Ayant)";

					    cmd = new SqlCommand(queryString, connection);

					    cmd.Parameters.AddWithValue("@Id_Chambre", a.Id_Chambre);
					    SqlParameter paramIdChambre = new SqlParameter("@Id_Chambre", SqlDbType.Int);
					    paramIdChambre.Direction = ParameterDirection.Input;

					    cmd.Parameters.AddWithValue("@Id_Ayant", a.Id_Ayant);
					    SqlParameter paramIdAyant = new SqlParameter("@Id_Ayant", SqlDbType.Int);
					    paramIdAyant.Direction = ParameterDirection.Input;

					    result = (int)cmd.ExecuteScalar();

					    if (result > 0)
					    {
						    UpdateAyantChambre(connection, chambre, a);
					    }
					    else
					    {
						    InsertAyantChambre(connection, a);
					    }
				    }
										
                }
			}            
		}

		public void UpdateChambre(SqlConnection connection, CHAMBRE chambre)
		{			
			string queryString = "UPDATE CHAMBRE" +
			" SET Prix = @Prix, Etat = @Etat, Code_TypeChambre = @Code_TypeChambre, Code_Localisation = @Code_Localisation, Memo = @Memo " +
			"WHERE Id_Chambre = @Id_Chambre";

			SqlCommand cmd = new SqlCommand(queryString, connection);

			cmd.Parameters.AddWithValue("@Etage", chambre.Etage);
			cmd.Parameters.AddWithValue("@Prix", chambre.Prix);
			cmd.Parameters.AddWithValue("@Etat", chambre.Etat);
			cmd.Parameters.AddWithValue("@Code_TypeChambre", chambre.Code_TypeChambre);
			cmd.Parameters.AddWithValue("@Code_Localisation", chambre.Code_Localisation);
			cmd.Parameters.AddWithValue("@Memo", chambre.Memo);
			cmd.Parameters.AddWithValue("@Id_Chambre", chambre.Id_Chambre);

			int result = 0;

			cmd.CommandText = queryString;

			try
			{
				result = cmd.ExecuteNonQuery();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}					
		}

		private void UpdateAyantChambre(SqlConnection conneciton, CHAMBRE c, AYANT a)
        {
            string queryString = "UPDATE AYANT " +
                                "SET Code_Commodite = @Code_Commodite " +
                                "WHERE Id_Chambre = @Id_Chambre AND Id_Ayant = @Id_Ayant";

			SqlCommand cmd = new SqlCommand(queryString, conneciton);

            cmd.Parameters.AddWithValue("@Id_Chambre", a.Id_Chambre);
            cmd.Parameters.AddWithValue("@Code_Commodite", a.Code_Commodite);
            cmd.Parameters.AddWithValue("@Id_Ayant", a.Id_Ayant);

			cmd.CommandText = queryString;		
            
            cmd.ExecuteNonQuery();
        }

        private void InsertAyantChambre(SqlConnection connection, AYANT a)
        {            
            string queryString = "INSERT INTO AYANT (Id_Chambre, Code_Commodite) " +
                                "VALUES (@Id_Chambre, @Code_Commodite)";

			SqlCommand cmd = new SqlCommand(queryString, connection);

            cmd.Parameters.AddWithValue("@Id_Chambre", a.Id_Chambre);
            cmd.Parameters.AddWithValue("@Code_Commodite", a.Code_Commodite);

			cmd.CommandText = queryString;

			cmd.ExecuteNonQuery();
		}

        private void InsertChambre(SqlConnection connection, CHAMBRE chambre)
        {
            int id = Chambres.Where(item => item.Etage == chambre.Etage).Max(item => item.Id_Chambre) + 1 == 0 ? chambre.Etage.Value * 100+1 : chambre.Etage.Value * 100 + Chambres.Where(item => item.Etage == chambre.Etage).Max(item => item.Id_Chambre) % 100 + 1;

            chambre.Id_Chambre = id;

            string queryString = "INSERT INTO CHAMBRE " +
                                    "(Id_Chambre, Etage, Prix, Etat, Code_TypeChambre, Code_Localisation, Memo) " +
									"VALUES (@Id_Chambre, @Etage, @Prix, @Etat, @Code_TypeChambre, @Code_Localisation, @Memo)";

            using (SqlCommand command = new SqlCommand(queryString, connection))
            {
                command.Parameters.AddWithValue("@Etage", chambre.Etage);
				command.Parameters.AddWithValue("@Prix", chambre.Prix);
				command.Parameters.AddWithValue("@Etat", chambre.Etat);
				command.Parameters.AddWithValue("@Code_TypeChambre", chambre.Code_TypeChambre);
				command.Parameters.AddWithValue("@Code_Localisation", chambre.Code_Localisation);
				command.Parameters.AddWithValue("@Memo", chambre.Memo);
				command.Parameters.AddWithValue("@Id_Chambre", chambre.Id_Chambre).Direction = ParameterDirection.Input;

				command.ExecuteNonQuery();

				foreach (var a in chambre.AYANT)
				{
					a.Id_Chambre = chambre.Id_Chambre;
					InsertAyantChambre(connection, a);
				}
            }
		}

        public void DeleteAyantChambre(AYANT a)
        {
            using (SqlConnection connection = new SqlConnection())
            {
				connection.ConnectionString = connectionString;
				connection.Open();

                string queryString = "DELETE AYANT " +
                                    "WHERE (AYANT.Id_Chambre = @Id_Chambre " +
                                    "AND AYANT.Code_Commodite = @Code_Commodite)";

                SqlCommand cmd = new SqlCommand(queryString, connection);

                cmd.Parameters.AddWithValue("@Id_Chambre", a.Id_Chambre).Direction = ParameterDirection.Input;
                cmd.Parameters.AddWithValue("@Code_Commodite", a.Code_Commodite).Direction = ParameterDirection.Input;

                cmd.ExecuteNonQuery();
			}
        }

		public void Reload(CHAMBRE chambre)
        {
			int index = chambres.IndexOf(chambre);
            chambres.RemoveAt(index);
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
				connection.ConnectionString = connectionString;
				connection.Open();

				string queryString = "SELECT * " +
					"FROM CHAMBRE " +
					"WHERE Id_Chambre = @Id_Chambre ";

				SqlCommand cmd = new SqlCommand(queryString, connection);
				cmd.Parameters.AddWithValue("@Id_Chambre", chambre.Id_Chambre);
				cmd.ExecuteNonQuery();

				using (SqlDataReader reader = cmd.ExecuteReader())
				{
					while (reader.Read())
					{
						ReaderToChambre(reader, chambre);
						Chambres.Insert(index, chambre);
                        chambre.TYPECHAMBRE = typeChambres.FirstOrDefault(i => i.Code_TypeChambre == chambre.Code_TypeChambre);
					}
				}
			}
		}
        
		#endregion
		
		public void ReaderToChambre(SqlDataReader reader, CHAMBRE chambre)
		{
			chambre.Id_Chambre = (int)reader["Id_Chambre"];
			chambre.Etage = (int)reader["Etage"];
			chambre.Prix = (decimal)reader["Prix"];
			chambre.Etat = (bool)reader["Etat"];
			chambre.Code_TypeChambre = (string)reader["Code_TypeChambre"];
			chambre.Code_Localisation = (string)reader["Code_Localisation"];
			chambre.Memo = (string)reader["Memo"];
		}

        public void LoadAll()
        {
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				connection.Open();

                LoadCommodites(connection);

				LoadTypesChambres(connection);                
				
				LoadLocalisations(connection);
                
				LoadChambres(connection);
			}
		}

		private void LoadCommodites(SqlConnection connection)
        {
			Commodites = new ObservableCollection<COMMODITE>();

			string queryString = "SELECT * FROM COMMODITE";

			SqlCommand cmd = new SqlCommand(queryString, connection);

			using (SqlDataReader reader = cmd.ExecuteReader())
			{
				while (reader.Read())
				{
					COMMODITE c = new COMMODITE();

					ReaderToCommodite(reader, c);		
                    
                    Commodites.Add(c);
				}
			}
		}

        public void ReaderToCommodite(SqlDataReader reader, COMMODITE c)
        {
            c.Code_Commodite = (string)reader["Code_Commodite"];
            c.Description = (string)reader["Description"];
        }

		private void LoadAyants(SqlConnection connection, CHAMBRE c)
        {            
            string queryString = "SELECT * FROM AYANT WHERE Id_Chambre = @Id_Chambre";

            SqlCommand cmd = new SqlCommand(queryString, connection);

            cmd.Parameters.AddWithValue("@Id_Chambre", c.Id_Chambre);

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    AYANT a = new AYANT();

                    ReaderToAyant(reader, a);

                    a.COMMODITE = Commodites.FirstOrDefault(i => i.Code_Commodite == a.Code_Commodite);

                    c.AYANT.Add(a);
                }
                
            }
        }

        public void ReaderToAyant(SqlDataReader reader, AYANT a)
        {
            a.Id_Ayant = (int)reader["Id_Ayant"];
            a.Id_Chambre = (int)reader["Id_Chambre"];
            a.Code_Commodite = (string)reader["Code_Commodite"];
        }

		private void LoadLocalisations(SqlConnection connection)
        {
            Localisations = new ObservableCollection<LOCALISATION>();

            string queryString = "SELECT * FROM LOCALISATION";

            SqlCommand command = new SqlCommand(queryString, connection);

            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    LOCALISATION l = new LOCALISATION();

                    ReaderToLocalisation(reader, l);
                    
                    Localisations.Add(l);
                }
            }
        }

        public void ReaderToLocalisation(SqlDataReader reader, LOCALISATION l)
        {
            l.Code_Localisation = (string)reader["Code_Localisation"];

            l.Description = (string)reader["Description"];
        }

        private void LoadTypesChambres(SqlConnection connection)
        {
            TypesChambres = new ObservableCollection<TYPECHAMBRE>();

            string queryString = "SELECT * FROM TYPECHAMBRE";

			SqlCommand command = new SqlCommand(queryString, connection);

            using (SqlDataReader reader = command.ExecuteReader()) 
            {
                while (reader.Read())
                {
                    TYPECHAMBRE tc = new TYPECHAMBRE();

                    ReaderToTypeChambre(reader, tc );

                    TypesChambres.Add(tc);
                }
            }
		}

        public void ReaderToTypeChambre(SqlDataReader reader, TYPECHAMBRE tc)
        {
            tc.Code_TypeChambre = (string)reader["Code_TypeChambre"];

            tc.Description = (string)reader["Description"];
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

                    Chambres.Add(chambre);
					chambre.TYPECHAMBRE = typeChambres.FirstOrDefault(i => i.Code_TypeChambre == chambre.Code_TypeChambre);
				}
            }
            foreach(CHAMBRE c in Chambres)
            {
                LoadAyants(connection, c);
            }

        }
	}
}
