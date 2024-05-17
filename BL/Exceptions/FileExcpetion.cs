using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Exceptions
{
    public class FileExcpetion : Exception
    {
        public FileExcpetion(string? message) : base(message)
        {
        }

        public FileExcpetion(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
