using BL.Exceptions;
using BL.Managers;
using BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TuincentrumTest
{
    public class UnitTestProduct
    {
        [Fact]
        public void Product_Valid()
        {
            Product p = new("Plantje", "P", 4.5, "20cm Plantje");
            Assert.Equal("Plantje", p.Naam_nl);
            Assert.Equal("P", p.Naam_W);
            Assert.Equal(4.5, p.Prijs);
            Assert.Equal("20cm Plantje", p.Beschrijving);
        }

        [Theory]
        [InlineData(" ")]
        [InlineData("\n")]
        [InlineData("")]
        public void Product_InValid(string naam)
        {
            Assert.Throws<TuincentrumException>(() => new Product(naam, "P", 4.5, "Beschrijving"));
            Assert.Throws<TuincentrumException>(() => new Product("Plantje", naam, 4.5, "Beschrijving"));
        }
    }
}
