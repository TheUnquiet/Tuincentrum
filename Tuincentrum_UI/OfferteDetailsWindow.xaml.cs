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
    /// Interaction logic for OfferteDetailsWindow.xaml
    /// </summary>
    public partial class OfferteDetailsWindow : Window
    {
        private ITuincentrumRepository tuincentrumRepository;
        private TuincentrumManager tuincentrumManager;

        public OfferteDetailsWindow(Offerte offerte, Klant klant)
        {
            InitializeComponent();
            OfferteTextBlock.Text += offerte.Datum;
            OfferteDetailsListBox.ItemsSource = offerte.producten;
            KlantTextBlock.Text = klant.ToString();
            tuincentrumRepository = new TuincentrumRepository(ConfigurationManager.ConnectionStrings["TuincentrumDBConnectionLaptop"].ToString());
            tuincentrumManager = new TuincentrumManager(tuincentrumRepository);
        }

        private void NieuweOfferteButtonClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Mannetje!", "Mannetje");
        }

        private void CloseButtonClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
