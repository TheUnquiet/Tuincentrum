using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using BL.Models;
using BL.Exceptions;
using System.Numerics;

namespace TuincentrumTest
{
    public class UnitTestKlant
    {
        private Klant klant;
        public UnitTestKlant()
        {
            klant = new Klant("Lina", "Bijlokehof 5");
        }

        [Fact]
        public void Ctor_Valid()
        {
            Assert.Equal("Lina", klant.Naam);
            Assert.Equal("Bijlokehof 5", klant.Adres);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("\n")]
        [InlineData(" ")]
        public void Ctor_InValid(string text)
        {
            Assert.Throws<TuincentrumException>(() => klant.Naam = text);
            Assert.Throws<TuincentrumException>(() => klant.Adres = text);
        }
    }
}
