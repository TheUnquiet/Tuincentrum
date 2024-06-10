using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using BL.Exceptions;
using BL.Models;
using Xunit;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TuincentrumTest
{
    public class UnitTestOfferte
    {
        private Product product1, product2;
        private Klant k1;

        public UnitTestOfferte()
        {
            product1 = new Product("Plantje", "Plant", 100, "plantje 2cm");
            product2 = new Product("Boompje", "Boom", 4.1, "boompje 1m");
            k1 = new Klant("Lina", "Bijlokehof 4");
        }

        [Fact]
        public void Ctor_Valid()
        {
            var Datum = DateTime.Now;

            Offerte offerte = new Offerte(Datum, true, true, 10, k1);

            Assert.Equal(Datum, offerte.Datum);
            Assert.True(offerte.Afhaal);
            Assert.True(offerte.Aanleg);
            Assert.Equal(10, offerte.AantalProducten);
            Assert.Equal(k1, offerte.Klant);
        }

        [Fact]
        public void Ctor_InValid()
        {
            var Datum = DateTime.Now;

            k1 = null;

            Assert.Throws<TuincentrumException>(() => new Offerte(Datum, true, true, 10, k1));
        }

        [Fact]
        public void VoegProduct_Valid()
        {
            var Datum = DateTime.Now;

            Offerte offerte = new Offerte(Datum, true, true, 0, k1);

            offerte.VoegProduct(product1, 10);

            Assert.Contains(product1, offerte.Producten.Keys);
            Assert.Equal(10, offerte.Producten[product1]);
            Assert.Equal(10, offerte.AantalProducten);
            Assert.Single(offerte.Producten);

            offerte.VoegProduct(product1, 2);

            Assert.Contains(product1, offerte.Producten.Keys);
            Assert.Equal(12, offerte.Producten[product1]);
            Assert.Equal(12, offerte.AantalProducten);
            Assert.Single(offerte.Producten);

            offerte.VoegProduct(product2, 4);

            Assert.Contains(product1, offerte.Producten.Keys);
            Assert.Equal(12, offerte.Producten[product1]);
            Assert.Contains(product2, offerte.Producten.Keys);
            Assert.Equal(4, offerte.Producten[product2]);
            Assert.Equal(2, offerte.Producten.Count);
            Assert.Equal(16, offerte.AantalProducten);
        }

        [Fact]
        public void VoegProduct_Invalid()
        {
            var Datum = DateTime.Now;

            Offerte offerte = new Offerte(Datum, true, true, 0, k1);

            Assert.Throws<TuincentrumException>(() => offerte.VoegProduct(null, 2));
            Assert.Throws<TuincentrumException>(() => offerte.VoegProduct(product1, 0));
            Assert.Throws<TuincentrumException>(() => offerte.VoegProduct(product2, 0));
        }

        [Fact]
        public void VerwijderProduct_Valid()
        {
            var Datum = DateTime.Now;

            Offerte offerte = new Offerte(Datum, true, true, 0, k1);

            offerte.VoegProduct(product1, 10);

            offerte.VerwijderProduct(product1, 5);

            Assert.Contains(product1, offerte.Producten.Keys);
            Assert.Single(offerte.Producten);
            Assert.Equal(10 - 5, offerte.AantalProducten);

            offerte.VerwijderProduct(product1, 5);

            Assert.False(offerte.Producten.ContainsKey(product1));
            Assert.Empty(offerte.Producten);
        }

        [Fact]
        public void VerwijderProduct_InValid()
        {
            var Datum = DateTime.Now;

            Offerte offerte = new Offerte(Datum, true, true, 0, k1);

            offerte.VoegProduct(product1, 10);

            Assert.Throws<TuincentrumException>(() => offerte.VerwijderProduct(product1, 15));
            Assert.Throws<TuincentrumException>(() => offerte.VerwijderProduct(null, 1));
        }

        [Fact]
        public void OfferteKlant_InValid()
        {
            var Datum = DateTime.Now;

            Offerte offerte = new Offerte(Datum, true, true, 0, k1);

            Assert.Throws<TuincentrumException>(() => offerte.Klant = null);
        }

        [Fact]
        public void BerkenPrijs_Valid()
        {
            var Datum = DateTime.Now;

            Offerte offerte = new Offerte(Datum, true, false, 0, k1);

            offerte.VoegProduct(product1, 60); // totaal 6000

            offerte.RekenAf();

            Assert.Equal(5400, offerte.Prijs); // 10% korting
        }
    }
}
