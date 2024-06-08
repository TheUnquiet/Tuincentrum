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

        public UploadManager(ITuincentrumRepository tuincentrumRepository, IFileProcessor fileManager)
        {
            this.tuincentrumRepository = tuincentrumRepository;
            this.fileprocessor = fileManager;
        }

        public void UploadKlanten(string filename)
        {
            try
            {
                List<string> klanten = fileprocessor.Readfile(filename);
                List<Klant> klantObjecten = fileprocessor.MaakKlanten(klanten);
                foreach (Klant k in klantObjecten)
                {
                    tuincentrumRepository.SchrijfKlant(k);
                }
            } catch (Exception ex) { throw new TuincentrumException($"UploadKlanten - {ex.Message}", ex); }
        }
        /*
        public void UploadOffertes(string filename)
        {
            try
            {
                List<string> offertes = fileprocessor.Readfile(filename);
                List<Offerte> offerteObjecten = fileprocessor.MaakOffertes(offertes);
                foreach (Offerte o in offerteObjecten)
                {
                    tuincentrumRepository.SchrijfOfferte(o);
                }
            } catch (Exception ex) { throw new TuincentrumException($"UplpadOffertes - {ex.Message}", ex); }
            
        }
        */

        public void UploadProducten(string filename)
        {
            try
            {
                List<string> producten = fileprocessor.Readfile(filename);
                List<Product> productObjecten = fileprocessor.MaakProducten(producten);
                foreach (Product p in productObjecten)
                {
                    try
                    {
                        tuincentrumRepository.SchrijfProduct(p);
                    } catch (TuincentrumException)
                    {
                        continue;
                    }
                }
            } catch (Exception ex) { throw new TuincentrumException($"UploadProduct - {ex.Message}", ex); }
        }
    }
}
