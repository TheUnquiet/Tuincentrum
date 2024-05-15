using BL.Exceptions;
using BL.Interfaces;
using BL.Models;
using System.Diagnostics.Metrics;

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
            Dictionary<int, Bestelling> bestellingen = new Dictionary<int, Bestelling>();
            foreach (string bestelling in  bestellingenLijst)
            {
                string[] strings = bestelling.Split('|');
                Bestelling b = new(int.Parse(strings[0]), int.Parse(strings[1]), int.Parse(strings[2]));

                bestellingen.Add()
            }
        }

        public void UploadBestellingen(string filename)
        {
             
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
            throw new NotImplementedException();
        }

        public void UploadProducten(string filename)
        {
            throw new NotImplementedException();
        }
    }
}
