using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Models
{
    public class Offerte
    {
        public int? Id;
        private DateTime Datum;
        private bool Afhaal;
        private bool Aanleg;
        private int AantalProducten;
        private float Prijs;

        public Offerte(int id, DateTime datum, bool afhaal, bool aanleg, int aantalProducten)
        {
            Id = id;
            Datum = datum;
            Afhaal = afhaal;
            Aanleg = aanleg;
            AantalProducten = aantalProducten;
        }

        public Offerte(DateTime datum, bool afhaal, bool aanleg, int aantalProducten)
        {
            Datum = datum;
            Afhaal = afhaal;
            Aanleg = aanleg;
            AantalProducten = aantalProducten;
        }
    }
}
