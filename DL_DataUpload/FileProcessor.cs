using BL.Exceptions;
using BL.Interfaces;
using BL.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DL_DataUpload
{
    public class FileProcessor : IFileProcessor
    {
        public void CleanFolder(string folderName)
        {
            DirectoryInfo dir = new DirectoryInfo(folderName);
            foreach (FileInfo fileInfo in dir.GetFiles())
            {
                fileInfo.Delete();
            }
            foreach (DirectoryInfo directoryInfo in dir.GetDirectories())
            {
                CleanFolder(directoryInfo.FullName);
                directoryInfo.Delete();
            }
        }

        public List<string> GetFileNamesFromZip(string filename)
        {
            if (!File.Exists(filename)) throw new FileNotFoundException($"{filename} not found");
            using (var zipFile = ZipFile.OpenRead(filename))
            {
                return zipFile.Entries.Select(e => e.FullName).ToList();
            }
        }

        public bool IsFolderEmpty(string folderName)
        {
            DirectoryInfo dir = new DirectoryInfo(folderName);
            return (dir.GetFiles().Length == 0 && dir.GetDirectories().Length == 0);
        }

        public void Unzip(string zipFileName, string destinationFolder)
        {
            try
            {
                ZipFile.ExtractToDirectory(zipFileName, destinationFolder);
            } catch (Exception ex)
            {
                throw new Exception();
            }
        }

        public List<string> Readfile(string fileName)
        {
            List<string> results = new List<string>();
            using (StreamReader sr = new StreamReader(fileName))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    results.Add(line);
                }
            }
            return results;
        }

        public List<Klant> MaakKlanten(List<string> klantenLijst)
        {
            try
            {
                List<Klant> klanten = new();
                foreach (string klant in klantenLijst)
                {
                    string[] strings = klant.Split('|');
                    Klant k = new(strings[1], strings[2]);

                    klanten.Add(k);

                }
                return klanten;
            }
            catch (Exception ex) { throw new TuincentrumException($"MaakKlanten - {ex.Message}", ex); }
        }

        public List<Product> MaakProducten(List<string> productenLijst)
        {
            try
            {
                List<Product> producten = new List<Product>();
                foreach (string product in productenLijst)
                {
                    string[] strings = product.Split('|');
                    if (string.IsNullOrWhiteSpace(strings[1]))
                    {
                        continue;
                    }
                    Product p = new(strings[1], strings[2], float.Parse(strings[3]), strings[4]);
                    producten.Add(p);
                }

                return producten;
            }
            catch (Exception ex) { throw new FileExcpetion($"MaakProducten - {ex.Message}", ex); }
        }

        public List<Offerte> MaakOffertes(List<string> offertesLijst, List<Klant> klanten)
        {
            List<Offerte> offertes = new();
            foreach (string s in offertesLijst)
            {
                string[] strings = s.Split('|');
                var klant = klanten.Find(k => k.Id == int.Parse(strings[2]));

                Offerte o = new Offerte(
                    DateTime.Parse(strings[1]),
                    bool.Parse(strings[3]),
                    bool.Parse(strings[4]),
                    int.Parse(strings[5]),
                    klant
                );

                offertes.Add(o);
            }

            return offertes;
        }

        public void LeesOfferteEnProducten(List<Offerte> offertes, List<Product> producten, string filename)
        {
            var lines = Readfile(filename);
            foreach (var line in lines)
            {
                string[] strings = line.Split('|');
                var offerteId = int.Parse(strings[0]);
                var productId = int.Parse(strings[1]);
                var quantity = int.Parse(strings[2]);

                var offerte = offertes.Find(o => o.Id == offerteId);
                var product = producten.Find(p => p.Id == productId);

                if (offerte != null && product != null)
                {
                    offerte.VoegProduct(product, quantity);
                }
            }
        }
    }
}
