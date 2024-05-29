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
        [Fact]
        public void Klant_Valid()
        {
            Klant klant = new("Lina", "Bijlokevest 4");
            Assert.Equal("Lina", klant.Naam);
            Assert.Equal("Bijlokevest 4", klant.Adres);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(" \n")]
        public void KlantNaam_InValid(string naam)
        {
            Assert.Throws<TuincentrumException>(() => new Klant(naam, "Bijlokevest 4"));
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(" \n")]
        public void KlantAdres_InValid(string adres)
        {
            Assert.Throws<TuincentrumException>(() => new Klant("Lina", adres));
        }
    }
}
