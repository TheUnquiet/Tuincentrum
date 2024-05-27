using BL.Interfaces;
using BL.Managers;
using BL.Models;
using BL.Models.DTOS;
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
    /// Interaction logic for KlantResultWindow.xaml
    /// </summary>
    public partial class KlantResultWindow : Window
    {   
        private Klant klant;
        private ITuincentrumRepository tuincentrumRepository;
        private TuincentrumManager tuincentrumManager;
        public KlantResultWindow(Klant k, List<OfferteInfo> offertesKlant)
        {
            InitializeComponent();
            IdTextBox.Text += k.Id.ToString();
            NaamTextBox.Text += k.Naam;
            AdresTextBox.Text += k.Adres;
            OffertesListBox.ItemsSource = offertesKlant;
            klant = k;
            tuincentrumRepository = new TuincentrumRepository(ConfigurationManager.ConnectionStrings["TuincentrumDBConnectionLaptop"].ToString());
            tuincentrumManager = new TuincentrumManager(tuincentrumRepository);
        }

        private void ButtonClickClose(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void OfferteDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (OffertesListBox.SelectedItem != null)
            {
                OfferteInfo offerteInfo = (OfferteInfo)OffertesListBox.SelectedItem;
                OfferteDetailsWindow ofdw = new OfferteDetailsWindow(tuincentrumManager.GeefOfferte(offerteInfo.Id), klant);
                ofdw.ShowDialog();
            }
        }
    }
}
