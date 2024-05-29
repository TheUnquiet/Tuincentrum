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
        private Klant Klant;

        public OfferteDetailsWindow(Offerte offerte, Klant klant)
        {
            InitializeComponent();
            OfferteTextBlock.Text += offerte.Datum.ToString("dd/MM/yyyy");
            OfferteDetailsListBox.ItemsSource = offerte.producten.Select(p => p.ToonProductEnAantal());
            KlantTextBlock.Text += klant.ToString();
            
            Klant = klant;
        }

        private void NieuweOfferteButtonClick(object sender, RoutedEventArgs e)
        {
            NieuweOfferteWindow no = new NieuweOfferteWindow(Klant);
            no.Show();
        }

        private void CloseButtonClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
