using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zorgcentrum.domein;
using Zorgcentrum.infrastructuur;

namespace Zorgcentrum.ApplicationServices
{
    public class ApplicationArtsen
    {
        public static void RunArtsen()
        {
            string filePath = "C:\\Users\\Sebas\\source\\repos\\Solution1\\infrastructuur\\Artsen.csv";
       

            List<Arts> artsen = LoadCsvArtsen(filePath);

            List<Arts> artsenmetfoutRIZIV = new List<Arts>();
            foreach (var persoon in artsen)
            {

                // Console.Write($"{persoon.Rijksregisternummer} - {persoon.Naam} - {persoon.Voornaam} -  {persoon.RIZIV} - {persoon.Specialisatie} - " +
             //            $"- ");
                //   Program.DatabaseActions.InsertArts(persoon.Rijksregisternummer, persoon.Naam, persoon.Voornaam, persoon.RIZIV, persoon.Specialisatie);

      /*          if (persoon.RIZIV.Replace(".", "").Replace("-", "").Length != 15)
                {


                    artsenmetfoutRIZIV.Add(persoon);

                }

                */

            //    Console.Write($"{persoon.Rijksregisternummer} - {persoon.Naam} - {persoon.Voornaam} - " +
               //         $"{persoon.RIZIV} - {persoon.Specialisatie} - ");
                foreach (var afdeling in persoon.Afdelingen)
                {
                    string afdelingAsString = afdeling.ToString();
                    Program.DatabaseActions.InsertAfdelingenVoorPatient(persoon.Rijksregisternummer, afdelingAsString);

                    Console.Write(afdeling);

                }

                Console.WriteLine();


            }


            /*

            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine("Artsen met een ongeldige RIZIV:");
            foreach (var arts in artsenmetfoutRIZIV)
            {
                Console.WriteLine($"{arts.Rijksregisternummer} - {arts.Naam} - {arts.Voornaam} - " +
                    $"{arts.RIZIV} - {arts.Specialisatie} -   {arts.Afdelingen}");
            }

            */
            static List<Arts> LoadCsvArtsen(string path)
            {
                var result = new List<Arts>();

                if (!File.Exists(path))
                    throw new FileNotFoundException("CSV file not found.", path);

                using (var reader = new StreamReader(path))
                {
                    bool isHeader = true;
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        if (string.IsNullOrWhiteSpace(line))
                            continue; // Skip empty lines

                        // Skip header row
                        if (isHeader)
                        {
                            isHeader = false;
                            continue;
                        }

                        // Split by comma (basic CSV parsing)
                        var values = line.Split(';');

                        if (values.Length < 3)
                            throw new FormatException("CSV row does not have enough columns.");

                        // Map to class with validation


                        result.Add(new Arts(
                            values[0], // rijksregisternummer
                            values[1], // naam
                            values[2],
                             values[3],
                              values[4],
                            values[5].Split('|').ToArray()
                        ));
                    }
                }

                return result;
            }

        }
    }
}
    



