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

        private List<string> files = new List<string>()
        {
            @"producten.txt",
            @"klanten.txt",
            @"offertes.txt",
            @"offerte_producten.txt"
        };

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
            try
            {
                processor.Unzip(zipFileName, destinationFolder);
            } catch (Exception ex) { throw new FileManagerException($"ProcessZip - {ex.Message}"); }
        }
    }
}
