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
    }
}

