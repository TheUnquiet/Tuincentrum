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
    public class TuincentrumManager
    {
        private ITuincentrumRepository tuincentrumRepository;

        public TuincentrumManager(ITuincentrumRepository tuincentrumRepository)
        {
            this.tuincentrumRepository = tuincentrumRepository;
        }

        public Dictionary<string, Klant> GeefKlanten()
        {
            try
            {
                return tuincentrumRepository.LeesKlanten();
            } catch (Exception ex) { throw new TuincentrumException($"GeefKlanten - {ex.Message}", ex); }
        }

        public Klant ZoekKlant(string naam, string adres)
        {
            string key = naam + adres;
            Dictionary<string, Klant> klanten = GeefKlanten();
            if (klanten.ContainsKey(key))
            {
                return klanten[key];
            }
            else
            {
                return null;
            }
        }

        public List<Offerte> GeefOffertesVoorKlant(Klant k)
        {
            try
            {
                return tuincentrumRepository.LeesOffertesVoorKlant(k);
            } catch (Exception ex) { throw new TuincentrumException($"", ex); }
        }
    }
}
