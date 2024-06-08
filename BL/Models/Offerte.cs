using BL.Exceptions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Models
{
    public class Offerte
    {
        public int? Id { get; set; }

        public DateTime Datum { get; set; }

        public bool Afhaal { get; set; }

        public bool Aanleg { get; set; }

        public int AantalProducten { get; set; }

        public double Prijs { get; set; }

        public Klant Klant { 
            get { return klant; }
            set
            {
                if ((value != null) && (value != klant))
                {
                    klant = value;
                }
            }
        }

        private Klant klant;

        private Dictionary<Product, int> producten = new();

        public Offerte(int? id, DateTime datum, bool afhaal, bool aanleg, int aantalProducten, Klant? klant)
        {
            Id = id;
            Datum = datum;
            Afhaal = afhaal;
            Aanleg = aanleg;
            AantalProducten = aantalProducten;
            Klant = klant;
        }

        public Offerte(DateTime datum, bool afhaal, bool aanleg, int aantalProducten, Klant klant)
        {
            Datum = datum;
            Afhaal = afhaal;
            Aanleg = aanleg;
            AantalProducten = aantalProducten;
            Klant = klant;
        }

        public IReadOnlyDictionary<Product, int> Producten => producten;

        public void VoegProduct(Product product, int aantal)
        {
            if ((product == null) || (aantal <= 0)) throw new TuincentrumException("VoegProduct - product en aantal zijn fout");
            if (producten.ContainsKey(product))
            {
                producten[product] += aantal;
            }
            else
            {
                producten.Add(product, aantal);
            }
        }

        public void VerwijderProduct(Product product, int aantal)
        {
            if ((product == null)
                || (aantal <= 0)
                || (!producten.ContainsKey(product))
                || (producten[product] < aantal)) throw new TuincentrumException("VerwijderProduct");
            if (producten[product] == aantal) producten.Remove(product);
            else producten[product] -= aantal;
        }

        public void RekenAf()
        {
            Prijs = Math.Round(BerekenPrijs(), 2);
        }

        public double BerekenPrijs()
        {
            double prijs = 0;
            foreach (var p in producten)
            {
                prijs += (p.Key.Prijs * p.Value);
            }

            // korting (overall)
            if (prijs > 5000)
            {
                prijs *= 0.90;
            }
            else  if (prijs > 2000)
            {
                prijs *= 0.95;
            }

            // korting en afhaal
            if (!Afhaal)
            {
                if (prijs < 500)
                {
                    prijs += 100; 
                }
                else if (prijs < 1000)
                {
                    prijs += 50;
                }
            }

            if (Aanleg)
            {
                if (prijs > 5000)
                {
                    prijs *= 1.05; // 5%
                }
                else if (prijs > 2000)
                {
                    prijs *= 1.10; // 10%
                }
                else
                {
                    prijs *= 1.15; // 15%
                }
            }

            return (double)prijs;
        }

        public int BerekenTotaleAantalProducten()
        {
            int total = 0;
            foreach (var aantal in Producten.Values)
            {
                total += aantal;
            }
            return total;
        }

        public override string? ToString()
        {
            RekenAf();
            return $"Offerte nummer : {Id}";
        }
    }
}
