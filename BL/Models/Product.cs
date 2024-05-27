using BL.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Models
{
    public class Product
    {
        public int? Id;
        private string naam_nl;
        public string Naam_nl
        {
            get { return naam_nl; }
            set
            {
                if (string.IsNullOrWhiteSpace(value)) throw new TuincentrumException("Naam nl is leeg");
                naam_nl = value;
            }
        }
        public string Naam_W;
        public double Prijs;
        public string Beschrijving;

        public Product(int id, string naam_nl, string naam_W, double prijs, string beschrijving)
        {
            Id = id;
            Naam_nl = naam_nl;
            Naam_W = naam_W;
            Prijs = prijs;
            Beschrijving = beschrijving;
        }

        public Product(string naam_nl, string naam_W, double prijs, string beschrijving)
        {
            Naam_nl = naam_nl;
            Naam_W = naam_W;
            Prijs = prijs;
            Beschrijving = beschrijving;
        }

        public override string ToString()
        {
            return $"{Id} {Naam_nl} {Naam_W} {Prijs} {Beschrijving}";
        }
    }
}
