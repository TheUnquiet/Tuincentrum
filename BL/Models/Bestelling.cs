using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Models
{
    public class Bestelling
    {
        public int Offerte_Id { get; private set; }
        public int Product_Id { get; private set; }
        public int Aantal_Product { get; private set; }
    }
}
