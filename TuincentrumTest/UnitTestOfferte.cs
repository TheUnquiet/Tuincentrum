using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.Exceptions;
using BL.Models;
using Xunit;

namespace TuincentrumTest
{
    public class UnitTestOfferte
    {
        [Fact]
        public void Offerte_Valid()
        {
            Offerte offerte = new Offerte(DateTime.Now, 1, true, true, 4);
            Assert.Equal(1, offerte.KlantNummer);
            Assert.True(offerte.Afhaal);
            Assert.True(offerte.Aanleg);
            Assert.Equal(4, offerte.AantalProducten);
        }

        [Fact]
        public void Offerte_InValid()
        {
            Assert.Throws<TuincentrumException>(() => new Offerte(DateTime.Now, 0, true, true, 4));
        }
    }
}
