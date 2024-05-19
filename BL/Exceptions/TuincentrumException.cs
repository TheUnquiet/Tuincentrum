using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Exceptions
{
    public class TuincentrumException : Exception
    {
        public TuincentrumException(string? message) : base(message)
        {
        }

        public TuincentrumException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
