using BL.Exceptions;
using BL.Interfaces;
using BL.Models;
using System.Diagnostics.Metrics;
using System.Linq.Expressions;

namespace BL.Managers
{
    public class TuincentrumManager
    {
        private ITuincentrumRepository tuincentrumRepository;
        private FileManager fileManager;

        public TuincentrumManager(ITuincentrumRepository tuincentrumRepository, FileManager fileManager)
        {
            this.tuincentrumRepository = tuincentrumRepository;
            this.fileManager = fileManager;
        }

        public List<Klant> MaakKlanten(List<string> klantenLijst)
        {
            Dictionary<int, Klant> klanten = new Dictionary<int, Klant>();

            foreach (string klant in klantenLijst)
            {
                string[] strings = klant.Split('|');
                Klant k = new(int.Parse(strings[0]), strings[1], strings[2]);

                klanten.Add(k.Id, k);
                
            }
            return klanten.Values.ToList();
        }

        public List<Bestelling> MaakBestellingen(List<string> bestellingenLijst)
        {
            List<Bestelling> bestellingen = new List<Bestelling>();
            foreach (string bestelling in  bestellingenLijst)
            {
                string[] strings = bestelling.Split('|');
                Bestelling b = new(int.Parse(strings[0]), int.Parse(strings[1]), int.Parse(strings[2]));

                bestellingen.Add(b);
            }
            return bestellingen;
        }

        private List<Offerte> MaakOffertes(List<string> offertesLijst)
        {
            Dictionary<int, Offerte> offertes = new Dictionary<int, Offerte>();
            foreach (string offerte in offertesLijst)
            {
                string[] strings = offerte.Split('|');
                Offerte o = new(int.Parse(strings[0]), DateTime.Parse(strings[1], int.Parse(strings[2]), );
            }
        }

        private List<Product> MaakProducten(List<string> producten)
        {
            throw new NotImplementedException();
        }

        public void UploadBestellingen(string filename)
        {
            try
            {
                List<string> bestellingen = fileManager.Readfile(filename);
                List<Bestelling> bestellingenObjecten = MaakBestellingen(bestellingen);
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
                List<string> klanten = fileManager.Readfile(filename);
                List<Klant> klantObjecten = MaakKlanten(klanten);
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
                List<string> offertes = fileManager.Readfile(filename);
                List<Offerte> offerteObjecten = MaakOffertes(offertes);
                foreach (Offerte o in offerteObjecten)
                {
                    if (!tuincentrumRepository.HeeftOfferte(o)) tuincentrumRepository.SchrijfOfferte(o);
                }
            } catch (Exception ex) { throw new DomeinException($"UplpadOffertes - {ex.Message}", ex); }
        }

        public void UploadProducten(string filename)
        {
            try
            {
                List<string> producten = fileManager.Readfile(filename);
                List<Product> productObjecten = MaakProducten(producten);
                foreach (Product p in productObjecten)
                {
                    if (!tuincentrumRepository.HeeftProduct(p)) tuincentrumRepository.SchrijfProduct(p);
                }
            } catch (Exception ex) { throw new DomeinException($"UploadProduct - {ex.Message}", ex); }
        }
    }
}
