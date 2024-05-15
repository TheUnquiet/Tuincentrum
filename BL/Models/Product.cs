using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Models
{
    public class Product
    {
        public Product(int id, string naam_nl, string naam_W, int euro, string beschrijving)
        {
            Id = id;
            Naam_nl = naam_nl;
            Naam_W = naam_W;
            Euro = euro;
            Beschrijving = beschrijving;
        }

        public int Id { get; private set; }
        public string Naam_nl { get; set; }
        public string Naam_W { get; set; }
        public int Euro { get; set; }
        public string Beschrijving { get; set; }
    }
}
