using BL.Exceptions;
using BL.Interfaces;
using BL.Managers;
using BL.Models;
using DL_Data;
using DL_DataUpload;
using Microsoft.IdentityModel.Protocols;
using System.Configuration;

class Program
{
    static void Main(string[] args)
    {
        TuincentrumRepository tr = new(ConfigurationManager.ConnectionStrings["TuincentrumDBConnectionLaptop"].ToString());
        TuincentrumManager tm = new(tr);

        // Offerte offerte1 = tr.LeesOfferte(1);

        /*
        Console.WriteLine(offerte1 + "\n");
        foreach (var p in offerte1.Producten)
        {
            Console.WriteLine(p.Key + "\t" + p.Value + "\n");
        }

        Console.WriteLine(offerte1.Klant);
        Console.WriteLine(offerte1.Prijs);
        */

        /* Zoek product
        var res = tm.GeefProduct("Dwerg");
        foreach (var p in res)
        {
            Console.WriteLine(p.Naam_nl);
        }

        */

        Console.WriteLine(tr.LeesKlanten("").Count);
    }
}