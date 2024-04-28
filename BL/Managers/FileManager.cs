using BL.Exceptions;
using BL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Managers
{
    public class FileManager
    {
        private IFileProcessor processor;
        private const string products = "producten.txt";
        private const string klanten = "klanten.txt";
        private const string offertes = "offertes.txt";
        private const string offerte_producten = "offerte_producten.txt";

        public FileManager(IFileProcessor processor)
        {
            this.processor = processor;
        }

        public List<string> GetFilesFromZip(string zipFilename)
        {
            try
            {
                var fileNames = processor.GetFileNamesFromZip(zipFilename);
                return fileNames;
            } catch (Exception e) { throw new FileManagerException($"GetFilesFromZip - {e.Message}", e); }
        }
    }
}
