using System;
using System.Data;
 using System.Data.SqlClient;

namespace Zorgcentrum.infrastructuur
{
    class Program
    {
        static void Main()
        {


            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB; Initial Catalog=Zorgcentrum; Integrated Security=True";
          

            //   const string queryString =
            //    "SELECT Rijkregisternummer, Voornaam, Naam FROM dbo.Patienten ORDER BY naam DESC;";
            //    "SELECT Rijkregisternummer, Voornaam, Naam FROM dbo.Artsen ORDER BY naam DESC;";
            //    "SELECT AfspraakId, Type, DatumTijdStart, DuurMin, RijksregisterNrPatiënt, RijksregisterNrArts," +
            //      " AfdelingsCode, extra1, extra2 FROM dbo.Afspraken ;";

            /*
                    using (SqlConnection connection = new SqlConnection(connectionString))

                    using (SqlCommand command = new SqlCommand(queryString, connection))
                    {
                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            Console.WriteLine("Rijkrzefzqef");
                            while (reader.Read())
                            {
                                Console.WriteLine($"{reader.GetString(0)}\t{reader.GetString(1)}\t{reader.GetDateTime(2)}");
                            }
                        }
                    }
                }*/
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
                const string queryString = "SELECT Rijkregisternummer, Voornaam, Naam FROM dbo.Patienten ORDER BY Naam DESC;";

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
                                Console.WriteLine($"{reader["Rijkregisternummer"]}\t{reader["Voornaam"]}\t{reader["Naam"]}");
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
                const string queryString = "SELECT Rijkregisternummer, Voornaam, Naam FROM dbo.Artsen ORDER BY Naam DESC;";

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
                                Console.WriteLine($"{reader["Rijkregisternummer"]}\t{reader["Voornaam"]}\t{reader["Naam"]}");
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
                const string queryString = "SELECT AfspraakId, Type, DatumTijdStart, DuurMin, RijksregisterNrPatiënt, RijksregisterNrArts, AfdelingsCode, extra1, extra2 FROM dbo.Afspraken;";

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
                                Console.WriteLine($"{reader["AfspraakId"]}\t{reader["Type"]}\t{reader["DatumTijdStart"]}\t{reader["DuurMin"]}\t{reader["RijksregisterNrPatiënt"]}\t{reader["RijksregisterNrArts"]}\t{reader["AfdelingsCode"]}\t{reader["extra1"]}\t{reader["extra2"]}");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Geen afspraken gevonden.");
                        }
                    }
                }
            }

            public void InsertPatient(string Rijkregisternummer, string Voornaam, string Naam)
            {
                const string insertSql = "INSERT INTO dbo.Patienten (Rijkregisternummer, Voornaam, Naam) VALUES (@Rijkregisternummer, @Voornaam, @Naam);";

                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (SqlCommand insertCommand = new SqlCommand(insertSql, connection))
                    {
                        insertCommand.Parameters.Add("@Rijkregisternummer", SqlDbType.VarChar).Value = Rijkregisternummer;
                        insertCommand.Parameters.Add("@Voornaam", SqlDbType.VarChar).Value = Voornaam;
                        insertCommand.Parameters.Add("@Naam", SqlDbType.VarChar).Value = Naam;

                        insertCommand.ExecuteNonQuery();
                        Console.WriteLine($"Patiënt {Naam}, {Voornaam} toegevoegd.");
                    }
                }
            }

            // Insert-acties
            public void InsertAfdelingVoorPatient(string Rijkregisternummer, string AfdelingCode)
            {
                const string insertSql = "INSERT INTO dbo.AfdelingVoorPatient (Rijkregisternummer, AfdelingCode) VALUES (@Rijkregisternummer, @AfdelingCode);";

                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (SqlCommand insertCommand = new SqlCommand(insertSql, connection))
                    {
                        insertCommand.Parameters.Add("@Rijkregisternummer", SqlDbType.VarChar).Value = Rijkregisternummer;
                        insertCommand.Parameters.Add("@AfdelingCode", SqlDbType.VarChar).Value = AfdelingCode;

                        insertCommand.ExecuteNonQuery();
                        Console.WriteLine($"Afdeling {AfdelingCode} voor Patiënt {Rijkregisternummer} toegevoegd.");
                    }
                }
            }

            public void InsertArts(string Rijkregisternummer, string Voornaam, string Naam)
            {
                const string insertSql = "INSERT INTO dbo.Artsen (Rijkregisternummer, Voornaam, Naam) VALUES (@Rijkregisternummer, @Voornaam, @Naam);";

                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (SqlCommand insertCommand = new SqlCommand(insertSql, connection))
                    {
                        insertCommand.Parameters.Add("@Rijkregisternummer", SqlDbType.VarChar).Value = Rijkregisternummer;
                        insertCommand.Parameters.Add("@Voornaam", SqlDbType.VarChar).Value = Voornaam;
                        insertCommand.Parameters.Add("@Naam", SqlDbType.VarChar).Value = Naam;

                        insertCommand.ExecuteNonQuery();
                        Console.WriteLine($"Arts {Naam}, {Voornaam} toegevoegd.");
                    }
                }
            }

            // Insert-acties
            public void InsertAfdelingVoorArts(string Rijkregisternummer, string AfdelingCode)
            {
                const string insertSql = "INSERT INTO dbo.AfdelingVoorArts (Rijkregisternummer, AfdelingCode) VALUES (@Rijkregisternummer, @AfdelingCode);";

                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (SqlCommand insertCommand = new SqlCommand(insertSql, connection))
                    {
                        insertCommand.Parameters.Add("@Rijkregisternummer", SqlDbType.VarChar).Value = Rijkregisternummer;
                        insertCommand.Parameters.Add("@AfdelingCode", SqlDbType.VarChar).Value = AfdelingCode;

                        insertCommand.ExecuteNonQuery();
                        Console.WriteLine($"Afdeling {AfdelingCode}voor Arts {Rijkregisternummer} toegevoegd.");
                    }
                }
            }

            public void InsertAfspraak(string AfspraakId, string Type, DateTime DatumTijdStart, DateTime DuurMin, string RijksregisterNrPatiënt, string RijksregisterNrArts, string AfdelingsCode, string extra1, string extra2)
            {
                const string insertSql = @"INSERT INTO dbo.Afspraken
                                      (AfspraakId, Type, DatumTijdStart, DuurMin, RijksregisterNrPatiënt, RijksregisterNrArts, AfdelingsCode, extra1, extra2)
                                      VALUES
                                      (@AfspraakId, @Type, @DatumTijdStart, @DuurMin, @RijksregisterNrPatiënt, @RijksregisterNrArts, @AfdelingsCode, @extra1, @extra2);";

                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (SqlCommand insertCommand = new SqlCommand(insertSql, connection))
                    {
                        insertCommand.Parameters.Add("@AfspraakId", SqlDbType.Int).Value = AfspraakId;
                        insertCommand.Parameters.Add("@Type", SqlDbType.VarChar).Value = Type;
                        insertCommand.Parameters.Add("@DatumTijdStart", SqlDbType.DateTime).Value = DatumTijdStart;
                        insertCommand.Parameters.Add("@DuurMin", SqlDbType.Int).Value = DuurMin;
                        insertCommand.Parameters.Add("@RijksregisterNrPatiënt", SqlDbType.VarChar).Value = RijksregisterNrPatiënt;
                        insertCommand.Parameters.Add("@RijksregisterNrArts", SqlDbType.VarChar).Value = RijksregisterNrArts;
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
                const string deleteSql = "DELETE FROM dbo.Patienten WHERE Rijkregisternummer = @Rijkregisternummer;";

                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (SqlCommand deleteCommand = new SqlCommand(deleteSql, connection))
                    {
                        deleteCommand.Parameters.Add("@Rijkregisternummer", SqlDbType.VarChar).Value = rijksregisternummer;
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
                const string deleteSql = "DELETE FROM dbo.Artsen WHERE Rijkregisternummer = @Rijkregisternummer;";

                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (SqlCommand deleteCommand = new SqlCommand(deleteSql, connection))
                    {
                        deleteCommand.Parameters.Add("@Rijkregisternummer", SqlDbType.VarChar).Value = rijksregisternummer;
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