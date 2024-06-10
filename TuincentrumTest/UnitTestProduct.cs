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
        private Product product;

        public UnitTestProduct()
        {
            product = new Product("Plantje", "Plant", 100, "Heel duur");
        }

        [Fact]
        public void Ctor_Valid()
        {
            Assert.Equal("Plantje", product.Naam_nl);
            Assert.Equal("Plant", product.Naam_W);
            Assert.Equal(100, product.Prijs);
            Assert.Equal("Heel duur", product.Beschrijving);
        }

        [Fact]
        public void Ctor_InValid()
        {
            Assert.Throws<TuincentrumException>(() => product.Naam_nl = "");
            Assert.Throws<TuincentrumException>(() => product.Naam_W = "");
        }
    }
}
