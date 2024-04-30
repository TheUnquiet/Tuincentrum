using BL.Exceptions;
using BL.Interfaces;
using BL.Models;
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
        private const string productsFile = "producten.txt";
        private const string klantenFile = "klanten.txt";
        private const string offertesFile = "offertes.txt";
        private const string offerte_productenFile = "offerte_producten.txt";

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

        public bool IsFolderEmpty(string folderName)
        {
            try
            {
                return processor.IsFolderEmpty(folderName);
            } catch (Exception e) { throw new FileManagerException($"IsFolderEmpty - {e.Message}", e); }
        }

        public void CleanFolder(string folderName)
        {
            processor.CleanFolder(folderName);
        }

        public void ProcessZip(string zipFileName, string destinationFolder)
        {
            List<string> messages = new List<string>();
            processor.Unzip(zipFileName, destinationFolder);
            //List<string> klanten = LeesKlanten();
        }

        public List<Klant> MaakKlanten(List<string> klanten)
        {
            List<Klant> klantenLijst = new List<Klant>();
            List<string> klantenLijstFile = processor.ReadFile(klantenFile);

            foreach (string klant in klantenLijstFile)
            {
                string[] strings = klant.Split(klant, '|');
                klantenLijst.Add(new Klant(int.Parse(strings[0]), strings[1], strings[2]));
            }
            return klantenLijst;
        }
    }
}
