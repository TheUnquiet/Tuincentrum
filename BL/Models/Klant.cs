using BL.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Models
{
    public class Klant
    {
        public readonly List<Offerte> offertes;
        public int? Id;

        private string naam;
        public string Naam { 
            get { return naam; } 
            private set { 
                if (string.IsNullOrWhiteSpace(value))
                    throw new TuincentrumException($"Klant naam is leeg"); 
                naam = value;
            }
        }

        private string adres;
        public string Adres
        {
            get { return adres; }
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new TuincentrumException($"Klant naam is leeg");
                adres = value;
            }
        }

        public override string ToString()
        {
            return $"{Id} {Naam} {Adres}";
        }

        public Klant(int id, string naam, string adres)
        {
            Id = id;
            Naam = naam;
            Adres = adres;
        }

        public Klant(string naam, string adres)
        {
            Naam = naam;
            Adres = adres;
        }
    }
}
