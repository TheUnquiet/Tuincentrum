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
        //void SchrijfOfferteProduct(Offerte offerte);

        void SchrijfKlant(Klant klant);

        //void SchrijfOfferte(Offerte o);

        void SchrijfProduct(Product p);

        List<Klant> LeesKlanten(string naam);

        Offerte LeesOfferte(int id);

        List<Product> LeesProduct(string naam);

        List<Product> LeesAlleProducten();

        void SchrijfOfferteEnProducten(Offerte offerte);
    }
}
