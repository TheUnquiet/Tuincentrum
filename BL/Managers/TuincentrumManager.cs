using BL.Exceptions;
using BL.Interfaces;
using BL.Models;
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
                return tuincentrumRepository.LeesOfferte(id);
            }
            catch (Exception ex)
            {
                throw new TuincentrumException($"GeefOfferte - {ex.Message}", ex);
            }
        }
        
        public List<Product> GeefAlleProducten()
        {
            try
            {
                return tuincentrumRepository.LeesAlleProducten();
            } 
            catch (Exception ex) 
            {
                throw new TuincentrumException($"GeefProducten - {ex.Message}", ex); 
            }
        }

        public List<Product> GeefProduct(string naam)
        {
            try
            {
                return tuincentrumRepository.LeesProduct(naam);
            }
            catch (Exception ex) 
            { 
                throw new TuincentrumException($"GeefProduct - {ex.Message}", ex); 
            }
        }

        public void UploadOfferteEnProducten(Offerte offerte)
        {
            tuincentrumRepository.SchrijfOfferteEnProducten(offerte);
        }
    }
}

