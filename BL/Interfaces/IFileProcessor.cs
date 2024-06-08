using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.Models;

namespace BL.Interfaces
{
    public interface IFileProcessor
    {
        void CleanFolder(string folderName);

        List<string> GetFileNamesFromZip(string path);

        bool IsFolderEmpty(string folderName);

        void Unzip(string zipFileName, string destinationFolder);

        List<string> Readfile(string filename);

        public List<Klant> MaakKlanten(List<string> klantenLijst);

        public List<Product> MaakProducten(List<string> productenLijst);

        public List<Offerte> MaakOffertes(List<string> offertesLijst, List<Klant> klanten);

        public void LeesOfferteEnProducten(List<Offerte> offertes, List<Product> producten, string filename);
    }
}
