using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Models
{
    public class Product
    {
        private int? Id;
        private string Naam_nl;
        private string Naam_W;
        private int Euro;
        private string Beschrijving;

        public Product(int id, string naam_nl, string naam_W, int euro, string beschrijving)
        {
            Id = id;
            Naam_nl = naam_nl;
            Naam_W = naam_W;
            Euro = euro;
            Beschrijving = beschrijving;
        }

        public Product(string naam_nl, string naam_W, int euro, string beschrijving)
        {
            Naam_nl = naam_nl;
            Naam_W = naam_W;
            Euro = euro;
            Beschrijving = beschrijving;
        }
    }
}
