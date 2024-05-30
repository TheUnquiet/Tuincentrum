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
        public bool Afhaal;
        public bool Aanleg;
        public int AantalProducten;
        public float Prijs;
        public List<Product> producten = new List<Product>();
        public Klant Klant;

        public Offerte(int id, DateTime datum, Klant klant,  bool afhaal, bool aanleg, int aantalProducten)
        {
            Id = id;
            Datum = datum;
            Klant = klant;
            Afhaal = afhaal;
            Aanleg = aanleg;
            AantalProducten = aantalProducten;
        }

        public Offerte(DateTime datum, Klant klant, bool afhaal, bool aanleg, int aantalProducten)
        {
            Datum = datum;
            Klant = klant;
            Afhaal = afhaal;
            Aanleg = aanleg;
            AantalProducten = aantalProducten;
        }

        public override string? ToString()
        {
            return $"Offerte nummer : {Id} Datum : {Datum.Date} Klantnummer : {Klant.Id} Afhaal : {Afhaal} Aanleg : {Aanleg} Aantal producten : {AantalProducten}";
        }
    }
}
