using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Zorgcentrum.domein
{
    public abstract class Persoon
    {
        private string _rijksregisternummer;
        private string _naam;
        private string _voornaam;

        public string Rijksregisternummer
        {
            get => _rijksregisternummer;
            init
            {


                _rijksregisternummer = value;
            }
        }



        public string Naam
        {
            get => _naam;

            init { _naam = value; }

        }
        public string Voornaam
        {
            get => _voornaam;

            init { _voornaam = value; }

        }



        public Persoon(string rijksregisternummer, string naam, string voornaam)
        {
            Rijksregisternummer = rijksregisternummer;

            Naam = naam;

            Voornaam = voornaam;
        }


    }


}
