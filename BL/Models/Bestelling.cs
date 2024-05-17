using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Models
{
    public class Bestelling
    {
        private int Offerte_Id;
        private int Product_Id;
        private int Aantal_Product;

        public Bestelling(int offerte_Id, int product_Id, int aantal_Product)
        {
            Offerte_Id = offerte_Id;
            Product_Id = product_Id;
            Aantal_Product = aantal_Product;
        }
    }
}
