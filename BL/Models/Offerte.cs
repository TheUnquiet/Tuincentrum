﻿using System;
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
        public int KlantNummer;
        public bool Afhaal;
        public bool Aanleg;
        public int AantalProducten;
        public float Prijs;
        public List<Product> producten = new List<Product>();

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
            return $"{Id} {Datum.Date} {KlantNummer} {Afhaal} {Aanleg} {AantalProducten}";
        }
    }
}
