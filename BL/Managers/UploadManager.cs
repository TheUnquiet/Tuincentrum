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

        public void UploadBestellingen(string filename)
        {
            try
            {
                List<string> bestellingen = fileprocessor.Readfile(filename);
                List<Bestelling> bestellingenObjecten = fileprocessor.MaakBestellingen(bestellingen);
                foreach (Bestelling b in bestellingenObjecten)
                {
                    if (!tuincentrumRepository.HeeftBestelling(b)) tuincentrumRepository.SchrijfBestelling(b);
                }
            } catch (Exception ex) { throw new DomeinException($"UploadBestellingen - {ex.Message}", ex); }
        }

        public void UploadKlanten(string filename)
        {
            try
            {
                List<string> klanten = fileprocessor.Readfile(filename);
                List<Klant> klantObjecten = fileprocessor.MaakKlanten(klanten);
                foreach (Klant k in klantObjecten)
                {
                    if (!tuincentrumRepository.HeeftKlant(k)) tuincentrumRepository.SchrijfKlant(k);
                }
            } catch (Exception ex) { throw new DomeinException($"UploadKlanten - {ex.Message}", ex); }
        }

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
            } catch (Exception ex) { throw new DomeinException($"UplpadOffertes - {ex.Message}", ex); }
            
        }

        public void UploadProducten(string filename)
        {
            try
            {
                List<string> producten = fileprocessor.Readfile(filename);
                List<Product> productObjecten = fileprocessor.MaakProducten(producten);
                foreach (Product p in productObjecten)
                {
                    if (!tuincentrumRepository.HeeftProduct(p)) tuincentrumRepository.SchrijfProduct(p);
                }
            } catch (Exception ex) { throw new DomeinException($"UploadProduct - {ex.Message}", ex); }
        }
    }
}
