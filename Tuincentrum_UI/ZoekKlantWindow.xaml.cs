using BL.Interfaces;
using BL.Managers;
using BL.Models;
using DL_Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Tuincentrum_UI
{
    /// <summary>
    /// Interaction logic for ZoekKlant.xaml
    /// </summary>
    public partial class ZoekKlantWindow : Window
    {
        private ITuincentrumRepository tuincentrumRepository;
        private TuincentrumManager tuincentrumManager;
        public ZoekKlantWindow()
        {
            InitializeComponent();
            tuincentrumRepository = new TuincentrumRepository(ConfigurationManager.ConnectionStrings["TuincentrumDBConnectionLaptop"].ToString());
            tuincentrumManager = new TuincentrumManager(tuincentrumRepository);
        }

        private void ZoekKlantButtonClick(object sender, RoutedEventArgs e)
        {
            string naam = null;
            string adres = null;
            if (!string.IsNullOrWhiteSpace(ZoekNaam.Text)) naam = ZoekNaam.Text;
            if (!string.IsNullOrWhiteSpace(ZoekAdres.Text)) adres = ZoekAdres.Text;
            Klant gevondenKlant = tuincentrumManager.ZoekKlant(naam, adres);
            if (gevondenKlant == null) MessageBox.Show("Klant niet gevonden");
            else
            {
                var offertes = tuincentrumManager.GeefOffertesVoorKlant(gevondenKlant);
                KlantResultWindow krw = new(gevondenKlant, offertes);
                krw.Show();
            }
        }
    }
}
