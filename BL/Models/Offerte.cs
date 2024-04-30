using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Models
{
    public class Offerte
    {
        public int Id { get; internal set; }
        public DateTime Datum { get; set; }
        public bool Afhaal { get; set; }
        public int MyProperty { get; set; }
        public bool Aanleg { get; set; }
        public int AantalProducten { get; set; }
    }
}
