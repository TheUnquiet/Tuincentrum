using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Models.DTOS
{
    public class OfferteInfo
    {
        public OfferteInfo(int id, float prijs)
        {
            Id = id;
            Prijs = prijs;
        }

        public OfferteInfo(int id)
        {
            Id = id;
        }

        public int Id { get; set; }

        public float Prijs { get; set; }

        public override string ToString()
        {
            return $"Offerte nummer : {Id} | Prijs : {Prijs}";
        }
    }
}
