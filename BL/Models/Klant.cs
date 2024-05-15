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
        public Klant(int id, string naam, string adres)
        {
            Id = id;
            Naam = naam;
            Adres = adres;
        }
        private int id;
        public int Id {
            get { return id; }
            set
            {
                if (value < 0) throw new DomeinException("Id is onder nul"); id = value;
            }
        }

        private string naam;
        public string Naam { 
            get { return naam; } 
            private set { 
                if (string.IsNullOrEmpty(value)) 
                    throw new DomeinException($"Klant naam is leeg"); 
                naam = value; 
            }
        }

        private string adres;
        public string Adres
        {
            get { return adres; }
            private set
            {
                if (string.IsNullOrEmpty(value))
                    throw new DomeinException($"Klant naam is leeg");
                adres = value;
            }
        }

        public override string ToString()
        {
            return $"{Id} {Naam} {Adres}";
        }
    }
}
