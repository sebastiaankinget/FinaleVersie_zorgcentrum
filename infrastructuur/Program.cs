using System.Data.SqlClient;

string connectionString = @"Data Source=(localdb)\MSSQLLocalDB; Initial Catalog=Zorgcentrum; Integrated Security=True";
/*
using (SqlConnection conn = new SqlConnection(connectionString))
 * {
    conn.Open();
    //...
}
*/


internal partial class Program
{
   
    static void Main(string[] args)
    {
  
        static void PrintPatientenTable(string connectionString)
        {

            Console.WriteLine("test start infrastructure");
            using (SqlConnection connection = new(connectionString))
            {
                connection.Open();

                SqlCommand command = new("SELECT Rijkregisternummer, Naam, voornaam FROM Patienten;", connection);

                using (SqlDataReader dataReader = command.ExecuteReader())
                {
                    Console.WriteLine();
                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            string rijkregisternummer = (string)dataReader["Rijkregisternummer"];
                            string naam = (string)dataReader["Naam"];
                            string voornaam = (string)dataReader["Naam"];



                            Console.WriteLine($"{rijkregisternummer} | {naam} | {voornaam} ");
                        }
                    }
                    else { Console.WriteLine("Geen personen gevonden."); }
                }

                connection.Close();
            }
        }
    }
}