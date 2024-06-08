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

        private string naam_w;

        public string Naam_W {  
            get { return naam_w; }
            set
            {
                if (string.IsNullOrWhiteSpace(value)) throw new TuincentrumException("Naam w is leeg");
                naam_w = value;
            }
        }

        public double Prijs { get; set; }

        public string Beschrijving {  get; set; }
        

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
            return $"{Id} {naam_nl} {naam_w} €{Prijs} {Beschrijving}";
        }
    }
}
