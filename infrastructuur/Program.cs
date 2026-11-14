using System;
using System.Data;
 using System.Data.SqlClient;

class Program
{
    static void Main()
    {
        string connectionString = @"Data Source=(localdb)\MSSQLLocalDB; Initial Catalog=Zorgcentrum; Integrated Security=True";
        const string queryString =
//    "SELECT Rijkregisternummer, Voornaam, Naam FROM dbo.Patienten ORDER BY naam DESC;";
        //    "SELECT Rijkregisternummer, Voornaam, Naam FROM dbo.Artsen ORDER BY naam DESC;";
        "SELECT AfspraakId, Type, DatumTijdStart, DuurMin, RijksregisterNrPatiënt, RijksregisterNrArts," +
            " AfdelingsCode, extra1, extra2 FROM dbo.Afspraken ;";


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
    }
}

// CRUD functies voor de tabellen Patienten, Artsen, Afspraken in de database die via ApplicationServices Patienten, Artsen, Afspraken in de DB aanmaken, wijzigen, verwijderen, lezen
// => Create, Update, Read en Delete
// functie voor het lezen van Patienten, Artsen, Afspraken aan de tabellen uit de database en het weergeven in de console.
// functies voor het toevoegen van Patienten, Artsen, Afspraken aan de tabellen in de database via ApplicationServices.
// functies voor het verwijderen van Patienten, Artsen, Afspraken uit de tabellen in de database via ApplicationServices.
