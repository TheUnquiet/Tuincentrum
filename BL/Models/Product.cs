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
        public string Naam_nl;
        public string Naam_W;
        public float Prijs;
        public string Beschrijving;

        public Product(int id, string naam_nl, string naam_W, float prijs, string beschrijving)
        {
            Id = id;
            Naam_nl = naam_nl;
            Naam_W = naam_W;
            Prijs = prijs;
            Beschrijving = beschrijving;
        }

        public Product(string naam_nl, string naam_W, float prijs, string beschrijving)
        {
            Naam_nl = naam_nl;
            Naam_W = naam_W;
            Prijs = prijs;
            Beschrijving = beschrijving;
        }
    }
}
