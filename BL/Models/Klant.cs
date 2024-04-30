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
        public Klant(int id, string name, string adres)
        {
            Id = id;
            Name = name;
            Adres = adres;
        }

        public int? Id;

        private string name;
        public string Name { 
            get { return name; } 
            set { if (string.IsNullOrEmpty(value)) 
                    throw new DomeinException($"Klant naam is leeg"); 
                name = value; }
        }

        private string adres;
        public string Adres
        {
            get { return adres; }
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new DomeinException($"Klant naam is leeg");
                adres = value;
            }
        }

        public override string ToString()
        {
            return $"{Id} {Name} {Adres}";
        }
    }
}
