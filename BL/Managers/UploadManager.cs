using BL.Exceptions;
using BL.Interfaces;
using BL.Models;
using System.Diagnostics.Metrics;
using System.Linq.Expressions;

namespace BL.Managers
{
    public class UploadManager
    {
        private ITuincentrumRepository tuincentrumRepository;
        private IFileProcessor fileprocessor;

        public UploadManager(ITuincentrumRepository tuincentrumRepository, IFileProcessor fileprocessor)
        {
            this.tuincentrumRepository = tuincentrumRepository;
            this.fileprocessor = fileprocessor;
        }

        public void UploadKlanten(string filename)
        {
            var klanten = fileprocessor.MaakKlanten(fileprocessor.Readfile(filename));

            foreach (var k in klanten)
            {
                tuincentrumRepository.SchrijfKlant(k);
            }
        }

        public void UploadProducten(string filename)
        {
            var producten = fileprocessor.MaakProducten(fileprocessor.Readfile(filename));
            foreach (var p in producten)
            {
                tuincentrumRepository.SchrijfProduct(p);
            }
        }

        public void UploadOffertes(string filenameOffertes, string filenameRelatie)
        {
            var klanten = tuincentrumRepository.LeesKlanten("");
            var producten = tuincentrumRepository.LeesAlleProducten();
            var offertes = fileprocessor.MaakOffertes(fileprocessor.Readfile(filenameOffertes), klanten);
            fileprocessor.LeesOfferteEnProducten(offertes, producten, filenameRelatie);

            foreach (var offerte in offertes)
            {
                tuincentrumRepository.SchrijfOfferteEnProducten(offerte);
            }
        }
    }
}
