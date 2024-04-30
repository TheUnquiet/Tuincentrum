using BL.Interfaces;
using BL.Models;
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
            ZipFile.ExtractToDirectory(zipFileName, destinationFolder);
        }

        public List<string> ReadFile(string filename)
        {
            try
            {
                List<string> klanten = new List<string>();
                using (StreamReader sr = new StreamReader(filename))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        klanten.Add(line);
                    }
                }
                return klanten;
            } catch (Exception e) { throw new FileNotFoundException(); }
        }
    }
}
