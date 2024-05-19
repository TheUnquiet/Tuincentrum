using BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Interfaces
{
    public interface ITuincentrumRepository
    {
        bool HeeftBestelling(Bestelling b);
        bool HeeftKlant(Klant k);
        bool HeeftOfferte(Offerte o);
        bool HeeftProduct(Product p);
        void SchrijfBestelling(Bestelling b);
        void SchrijfKlant(Klant klant);
        void SchrijfOfferte(Offerte o);
        void SchrijfProduct(Product p);
        Dictionary<string, Klant> LeesKlanten();
        public List<Offerte> LeesOffertesVoorKlant(Klant k);
    }
}
