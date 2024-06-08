using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Models
{
    public class Data
    {
        public Dictionary<int, Klant> Klanten {  get; set; }
        public Dictionary<int, Offerte> Offertes { get; set; }
        public Dictionary<int, Product> Producten { get; set; }

        public Data()
        {
            Klanten = new();
            Offertes = new();
            Producten = new();
        }
    }
}
