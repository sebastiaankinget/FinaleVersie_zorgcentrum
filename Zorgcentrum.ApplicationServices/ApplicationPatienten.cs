using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zorgcentrum.domein;
using Zorgcentrum.infrastructuur;


namespace Zorgcentrum.ApplicationServices
{
    public class ApplicationPatienten
    {
        public static void RunPatiënten()
        {
            string filePath = "C:\\Users\\Sebas\\Desktop\\Patienten.csv";
       

            List<Patiënten> patiënten = LoadCsvPatiënten(filePath);

         
            foreach (var persoon in patiënten)
            {

                Console.Write($"{persoon.Rijksregisternummer} - {persoon.Naam} - {persoon.Voornaam} - " +
                         $"- ");
                Program.DatabaseActions.InsertPatient(persoon.Rijksregisternummer, persoon.Naam, persoon.Voornaam);

               Console.Write($"{persoon.Rijksregisternummer} - {persoon.Naam} - {persoon.Voornaam} - " +
                        $"- ");

                
                foreach (var afdeling in persoon.Afdelingen)

                {
                    Console.Write(afdeling); Console.Write(" ");

                }
                Console.WriteLine();

            
            }


            static List<Patiënten> LoadCsvPatiënten(string path)
            {
                var result = new List<Patiënten>();

                if (!File.Exists(path))
                    throw new FileNotFoundException("CSV file not found.", path);

                using (var reader = new StreamReader(path))
                {
                    bool isHeader = true;
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();




                        var values = line.Split(';');




                        result.Add(new Patiënten(
                            values[0], // rijksregisternummer
                            values[1], // naam
                            values[2], // voornaam
                            values[3].Split(',').ToArray()   // afdelingen (empty array as placeholder)
                        ));
                    }
                }

                return result;
            }
        }

    
    }
}
