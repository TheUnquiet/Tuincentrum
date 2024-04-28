using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Interfaces
{
    public interface IFileProcessor
    {
        List<string> GetFileNamesFromZip(string path);
    }
}
