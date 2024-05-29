using BL.Exceptions;
using BL.Interfaces;
using BL.Models;
using BL.Models.DTOS;
using System;
using System.Collections.Generic;
using System.Data;
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

        public List<Klant> GeefKlanten(string naam)
        {
            try
            {
                return tuincentrumRepository.LeesKlanten(naam);
            } catch (Exception ex) { throw new TuincentrumException($"GeefKlanten - {ex.Message}", ex); }
        }

        public Offerte GeefOfferte(int id)
        {
            try
            {
                Offerte offerte = tuincentrumRepository.LeesOfferte(id);
                if (offerte != null)
                {
                    offerte.producten = tuincentrumRepository.LeesProductenOfferte(id);
                }
                return offerte;
                
            } catch (Exception ex) { throw new TuincentrumException($"GeefOfferte - {ex.Message}", ex); }
        }

        public List<Product> GeefProducten()
        {
            try
            {
                return tuincentrumRepository.LeesProducten();
            } catch (Exception ex) { throw new TuincentrumException($"GeefProducten - {ex.Message}", ex); }
        }

        public Product GeefProduct(int id)
        {
            try
            {
                return tuincentrumRepository.LeesProduct(id);
            }catch (Exception ex) { throw new TuincentrumException($"GeefProduct - {ex.Message}", ex); }
        }

        public void UploadOfferte(Offerte o)
        {
            tuincentrumRepository.SchrijfOfferte(o);
        }

        public void UploadBestelling(Bestelling b)
        {
            tuincentrumRepository.SchrijfBestelling(b);
        }

        public Offerte GeefLaatsteOfferte()
        {
            try
            {
                return tuincentrumRepository.LeesLaatsteOfferte();
            } catch (Exception ex) { throw new TuincentrumException($"GeefLaatsteOfferte - {ex.Message}", ex); }
        }

        public double GeefPrijsOfferte(List<Product> producten)
        {
            if (producten == null || !producten.Any())
            {
                return 0;
            }

            return producten.Sum(product => product.Prijs);
        }
    }
}

