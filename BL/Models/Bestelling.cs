using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Models
{
    public class Bestelling
    {
        public int Offerte_Id { get; set; }
        public int Product_Id {  get; set; }
        public int Aantal_Product {  get; set; }

        public Bestelling(int offerte_Id, int product_Id, int aantal_Product)
        {
            Offerte_Id = offerte_Id;
            Product_Id = product_Id;
            Aantal_Product = aantal_Product;
        }
    }
}
