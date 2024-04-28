using BL.Interfaces;
using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DL_DataUpload
{
    public class FileProcessor : IFileProcessor
    {
        public List<string> GetFileNamesFromZip(string filename)
        {
            if (!File.Exists(filename)) throw new FileNotFoundException($"{filename} not found");
            using (var zipFile = ZipFile.OpenRead(filename))
            {
                return zipFile.Entries.Select(e => e.FullName).ToList();
            }
        }
    }
}
