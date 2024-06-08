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

        List<Klant> MaakKlanten(List<string> klantenLijst);

        List<Offerte> MaakOffertes(List<string> offerteLijst, List<Klant> klanten);

        List<Product> MaakProducten(List<string> productenLijst);
    }
}
