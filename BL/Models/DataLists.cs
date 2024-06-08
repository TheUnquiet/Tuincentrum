using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Models
{
    public class DataLists
    {
        public List<Klant> Klanten {  get; set; }
        public List<Offerte> Offertes { get; set; }
        public List<Product> Producten { get; set; }

        public DataLists()
        {
            Klanten = new();
            Offertes = new();
            Producten = new();
        }
    }
}
