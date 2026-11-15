using System;
using System.Data;
 using System.Data.SqlClient;



namespace Zorgcentrum.infrastructuur
{

  
    public class Program
    {
        public void test()
        {
            Console.WriteLine("test");
        }
        static void Main()
        {


            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB; Initial Catalog=Zorgcentrum; Integrated Security=True";
           DatabaseActions dbActions = new DatabaseActions(connectionString);
            // Read-acties
            dbActions.PrintPatientsTable();
            dbActions.PrintArtsenTable();
            dbActions.PrintAfsprakenTable();
            // Insert-acties
        }

        // Database-acties
        public class DatabaseActions
        {
            private readonly string _connectionString;

            public DatabaseActions(string connectionString)
            {
                _connectionString = connectionString;
            }

            // Read-acties
            public void PrintPatientsTable()
            {
                const string queryString = "SELECT Rijksregisternummer, Voornaam, Naam FROM dbo.Patienten ORDER BY Naam DESC;";

                using (SqlConnection connection = new SqlConnection(_connectionString))
                using (SqlCommand command = new SqlCommand(queryString, connection))
                {
                    connection.Open();
                    Console.WriteLine("Patiënten ophalen...");

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                Console.WriteLine($"{reader["Rijksregisternummer"]}\t{reader["Voornaam"]}\t{reader["Naam"]}");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Geen patiënten gevonden.");
                        }
                    }
                }
            }

            public void PrintArtsenTable()
            {
                const string queryString = "SELECT Rijksregisternummer, Voornaam, Naam FROM dbo.Artsen ORDER BY Naam DESC;";

                using (SqlConnection connection = new SqlConnection(_connectionString))
                using (SqlCommand command = new SqlCommand(queryString, connection))
                {
                    connection.Open();
                    Console.WriteLine("Artsen ophalen...");

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                Console.WriteLine($"{reader["Rijksregisternummer"]}\t{reader["Voornaam"]}\t{reader["Naam"]}");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Geen artsen gevonden.");
                        }
                    }
                }
            }

            public void PrintAfsprakenTable()
            {
                const string queryString = "SELECT AfspraakId, Type, DatumTijdStart, DuurMin, rijksregisterNrPatiënt, rijksregisterNrArts, AfdelingsCode, extra1, extra2 FROM dbo.Afspraken;";

                using (SqlConnection connection = new SqlConnection(_connectionString))
                using (SqlCommand command = new SqlCommand(queryString, connection))
                {
                    connection.Open();
                    Console.WriteLine("Afspraken ophalen...");

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                Console.WriteLine($"{reader["AfspraakId"]}\t{reader["Type"]}\t{reader["DatumTijdStart"]}\t{reader["DuurMin"]}\t{reader["rijksregisterNrPatiënt"]}\t{reader["rijksregisterNrArts"]}\t{reader["AfdelingsCode"]}\t{reader["extra1"]}\t{reader["extra2"]}");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Geen afspraken gevonden.");
                        }
                    }
                }
            }

            public static void InsertPatient(string Rijksregisternummer, string Voornaam, string Naam)
            {
                const string insertSql = "INSERT INTO dbo.Patienten (Rijksregisternummer, Voornaam, Naam) VALUES (@Rijksregisternummer, @Voornaam, @Naam);";
                
                string defConnectionString = @"Data Source=(localdb)\MSSQLLocalDB; Initial Catalog=Zorgcentrum; Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(defConnectionString))
                {
                    connection.Open();
                    using (SqlCommand insertCommand = new SqlCommand(insertSql, connection))
                    {
                        insertCommand.Parameters.Add("@Rijksregisternummer", SqlDbType.VarChar).Value = Rijksregisternummer;
                        insertCommand.Parameters.Add("@Voornaam", SqlDbType.VarChar).Value = Voornaam;
                        insertCommand.Parameters.Add("@Naam", SqlDbType.VarChar).Value = Naam;

                        insertCommand.ExecuteNonQuery();
                        
                        Console.WriteLine($"Patiënt {Naam}, {Voornaam} toegevoegd.");
                    }
                }
            }

            // Insert-acties
            public void InsertAfdelingVoorPatient(string Rijksregisternummer, string AfdelingCode)
            {
                const string insertSql = "INSERT INTO dbo.AfdelingVoorPatient (Rijksregisternummer, AfdelingCode) VALUES (@Rijksregisternummer, @AfdelingCode);";

                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (SqlCommand insertCommand = new SqlCommand(insertSql, connection))
                    {
                        insertCommand.Parameters.Add("@Rijksregisternummer", SqlDbType.VarChar).Value = Rijksregisternummer;
                        insertCommand.Parameters.Add("@AfdelingCode", SqlDbType.VarChar).Value = AfdelingCode;

                        insertCommand.ExecuteNonQuery();
                        Console.WriteLine($"Afdeling {AfdelingCode} voor Patiënt {Rijksregisternummer} toegevoegd.");
                    }
                }
            }

            public static void InsertArts(string Rijksregisternummer, string Voornaam, string Naam, string RIZIV, string Specialisatie)
            {
                const string insertSql = "INSERT INTO dbo.Artsen (Rijksregisternummer, Voornaam, Naam, RIZIV, Specialisatie) VALUES (@Rijksregisternummer, @Voornaam, @Naam, @RIZIV, @Specialisatie);";
                string defConnectionString = @"Data Source=(localdb)\MSSQLLocalDB; Initial Catalog=Zorgcentrum; Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(defConnectionString))
                {
                    connection.Open();
                    using (SqlCommand insertCommand = new SqlCommand(insertSql, connection))
                    {
                        insertCommand.Parameters.Add("@Rijksregisternummer", SqlDbType.VarChar).Value = Rijksregisternummer;
                        insertCommand.Parameters.Add("@Voornaam", SqlDbType.VarChar).Value = Voornaam;
                        insertCommand.Parameters.Add("@Naam", SqlDbType.VarChar).Value = Naam;
                        insertCommand.Parameters.Add("@RIZIV", SqlDbType.VarChar).Value = RIZIV;
                        insertCommand.Parameters.Add("@Specialisatie", SqlDbType.VarChar).Value = Specialisatie;

                        insertCommand.ExecuteNonQuery();
                        Console.WriteLine($"Arts {Naam}, {Voornaam} toegevoegd.");
                    }
                }
            }

            // Insert-acties
            public void InsertAfdelingVoorArts(string Rijksregisternummer, string AfdelingCode)
            {
                const string insertSql = "INSERT INTO dbo.AfdelingVoorArts (Rijksregisternummer, AfdelingCode) VALUES (@Rijksregisternummer, @AfdelingCode);";

                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (SqlCommand insertCommand = new SqlCommand(insertSql, connection))
                    {
                        insertCommand.Parameters.Add("@Rijksregisternummer", SqlDbType.VarChar).Value = Rijksregisternummer;
                        insertCommand.Parameters.Add("@AfdelingCode", SqlDbType.VarChar).Value = AfdelingCode;

                        insertCommand.ExecuteNonQuery();
                        Console.WriteLine($"Afdeling {AfdelingCode}voor Arts {Rijksregisternummer} toegevoegd.");
                    }
                }
            }

            public void InsertAfspraak(int AfspraakId, string Type, DateTime DatumTijdStart, int DuurMin, string rijksregisterNrPatiënt, string rijksregisterNrArts, string AfdelingsCode, string extra1, string extra2)
            {
                const string insertSql = @"INSERT INTO dbo.Afspraken
                                      (AfspraakId, Type, DatumTijdStart, DuurMin, rijksregisterNrPatiënt, rijksregisterNrArts, AfdelingsCode, extra1, extra2)
                                      VALUES
                                      (@AfspraakId, @Type, @DatumTijdStart, @DuurMin, @rijksregisterNrPatiënt, @rijksregisterNrArts, @AfdelingsCode, @extra1, @extra2);";
                string defConnectionString = @"Data Source=(localdb)\MSSQLLocalDB; Initial Catalog=Zorgcentrum; Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(defConnectionString))
                {
                    connection.Open();
                    using (SqlCommand insertCommand = new SqlCommand(insertSql, connection))
                    {
                        insertCommand.Parameters.Add("@AfspraakId", SqlDbType.Int).Value = AfspraakId;
                        insertCommand.Parameters.Add("@Type", SqlDbType.VarChar).Value = Type;
                        insertCommand.Parameters.Add("@DatumTijdStart", SqlDbType.DateTime).Value = DatumTijdStart;
                        insertCommand.Parameters.Add("@DuurMin", SqlDbType.Int).Value = DuurMin;
                        insertCommand.Parameters.Add("@rijksregisterNrPatiënt", SqlDbType.VarChar).Value = rijksregisterNrPatiënt;
                        insertCommand.Parameters.Add("@rijksregisterNrArts", SqlDbType.VarChar).Value = rijksregisterNrArts;
                        insertCommand.Parameters.Add("@AfdelingsCode", SqlDbType.VarChar).Value = AfdelingsCode;
                        insertCommand.Parameters.Add("@extra1", SqlDbType.VarChar).Value = extra1 ?? (object)DBNull.Value;
                        insertCommand.Parameters.Add("@extra2", SqlDbType.VarChar).Value = extra2 ?? (object)DBNull.Value;

                        insertCommand.ExecuteNonQuery();
                        Console.WriteLine($"Afspraak voor {DatumTijdStart} toegevoegd.");
                    }
                }
            }
            public void DeletePatient(string rijksregisternummer)
            {
                const string deleteSql = "DELETE FROM dbo.Patienten WHERE Rijksregisternummer = @Rijksregisternummer;";

                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (SqlCommand deleteCommand = new SqlCommand(deleteSql, connection))
                    {
                        deleteCommand.Parameters.Add("@Rijksregisternummer", SqlDbType.VarChar).Value = rijksregisternummer;
                        int rowsAffected = deleteCommand.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            Console.WriteLine($"Patiënt met rijksregisternummer {rijksregisternummer} verwijderd.");
                        }
                        else
                        {
                            Console.WriteLine($"Geen patiënt gevonden met rijksregisternummer {rijksregisternummer}.");
                        }
                    }
                }
            }

            public void DeleteArts(string rijksregisternummer)
            {
                const string deleteSql = "DELETE FROM dbo.Artsen WHERE Rijksregisternummer = @Rijksregisternummer;";

                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (SqlCommand deleteCommand = new SqlCommand(deleteSql, connection))
                    {
                        deleteCommand.Parameters.Add("@Rijksregisternummer", SqlDbType.VarChar).Value = rijksregisternummer;
                        int rowsAffected = deleteCommand.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            Console.WriteLine($"Arts met rijksregisternummer {rijksregisternummer} verwijderd.");
                        }
                        else
                        {
                            Console.WriteLine($"Geen arts gevonden met rijksregisternummer {rijksregisternummer}.");
                        }
                    }
                }
            }

            public void DeleteAfspraak(int afspraakId)
            {
                const string deleteSql = "DELETE FROM dbo.Afspraken WHERE AfspraakId = @AfspraakId;";

                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (SqlCommand deleteCommand = new SqlCommand(deleteSql, connection))
                    {
                        deleteCommand.Parameters.Add("@AfspraakId", SqlDbType.Int).Value = afspraakId;
                        int rowsAffected = deleteCommand.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            Console.WriteLine($"Afspraak met ID {afspraakId} verwijderd.");
                        }
                        else
                        {
                            Console.WriteLine($"Geen afspraak gevonden met ID {afspraakId}.");
                        }
                    }
                }
            }
        }
    }
}