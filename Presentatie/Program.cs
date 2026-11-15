
using System;
using Zorgcentrum.domein;
using Zorgcentrum.ApplicationServices;


internal class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Welkom wil je runnen");
        int keuze = int.Parse(Console.ReadLine());
        Console.WriteLine(keuze);
        switch (keuze)
        {
            case 1:
                ApplicationPatienten.RunPatiënten();
                break;

            case 2:
                ApplicationArtsen.RunArtsen();
                break;
            case 3:
                ApplicationAfspraken.RunAfspraken();
                break;


        }
    }
}