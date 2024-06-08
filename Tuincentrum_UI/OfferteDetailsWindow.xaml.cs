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
        private Offerte offerte;
        public OfferteDetailsWindow(Offerte o)
        {
            InitializeComponent();
            offerte = o;
            OfferteTextBlock.Text += offerte.Datum.ToString("dd/MM/yyyy");
            KlantTextBlock.Text += offerte.Klant.ToString();
            offerte.RekenAf();
            PrijsTextBlock.Text += offerte.Prijs.ToString();

            foreach (var p in offerte.Producten)
            {
                OfferteDetailsListBox.Items.Add(new ListBoxItem
                {
                    Content = $"x{p.Value}, {p.Key}"
                });
            }
        }

        private void NieuweOfferteButtonClick(object sender, RoutedEventArgs e)
        {
            NieuweOfferteWindow no = new NieuweOfferteWindow(offerte.Klant);
            no.Show();
        }

        private void CloseButtonClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
