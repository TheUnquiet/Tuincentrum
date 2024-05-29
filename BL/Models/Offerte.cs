using BL.Exceptions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Models
{
    public class Offerte
    {
        public int? Id;
        public DateTime Datum;
        private int klantnummer;
        public int KlantNummer { 
            get { return klantnummer; }
            set { if (value <= 0) throw new TuincentrumException("Klantnummer is niet geldig"); klantnummer = value; }
        }
        public bool Afhaal;
        public bool Aanleg;
        public int AantalProducten;
        public float Prijs;
        public List<Product> producten = new List<Product>();
        public Klant klant;

        public Offerte(int id, DateTime datum, int klantNr,  bool afhaal, bool aanleg, int aantalProducten)
        {
            Id = id;
            Datum = datum;
            KlantNummer = klantNr;
            Afhaal = afhaal;
            Aanleg = aanleg;
            AantalProducten = aantalProducten;
        }

        public Offerte(DateTime datum, int klantNr, bool afhaal, bool aanleg, int aantalProducten)
        {
            Datum = datum;
            KlantNummer = klantNr;
            Afhaal = afhaal;
            Aanleg = aanleg;
            AantalProducten = aantalProducten;
        }

        public override string? ToString()
        {
            return $"Offerte nummer : {Id} Datum : {Datum.Date} Klantnummer : {KlantNummer} Afhaal : {Afhaal} Aanleg : {Aanleg} Aantal producten : {AantalProducten}";
        }
    }
}
