using BL.Interfaces;
using BL.Managers;
using BL.Models;
using DL_Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Printing;
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
    /// Interaction logic for NieuweOfferte.xaml
    /// </summary>
    public partial class NieuweOfferteWindow : Window
    {
        private ObservableCollection<Product> AlleProducten;
        private ObservableCollection<Product> GeselecteerdeProducten;
        private TuincentrumManager tuincentrumManager;
        private ITuincentrumRepository tuincentrumRepository;
        private Offerte offerte;

        public NieuweOfferteWindow(Klant k)
        {
            InitializeComponent();

            tuincentrumRepository = new TuincentrumRepository(ConfigurationManager.ConnectionStrings["TuincentrumDBConnectionLaptop"].ToString());
            tuincentrumManager = new TuincentrumManager(tuincentrumRepository);

            DatumTextBlock.Text = DateTime.Now.ToString("dd/MM/yyyy");
            KlantIdTextBlock.Text = k.Id.ToString();
            KlantNaamTextBlock.Text = k.Naam;
            KlantAdresTextBlock.Text = k.Adres;
            AlleProducten = new ObservableCollection<Product>(tuincentrumManager.GeefAlleProducten());

            GeselecteerdeProducten = new ObservableCollection<Product>();
            GeselecteerdeProducten.CollectionChanged += GeselecteerdeProducten_CollectionChanged;

            AlleProductenListBox.ItemsSource = AlleProducten;
            GeselecteerdeProductenListBox.ItemsSource = GeselecteerdeProducten;

            offerte = new(DateTime.Parse(DatumTextBlock.Text), false, false, 0, k);
        }

        private void GeselecteerdeProducten_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            UpdatePrijsText();
        }

        private void UpdatePrijsText()
        {
            offerte.RekenAf();
            PrijsTextBlock.Text = $"Prijs: €{offerte.Prijs}";
        }

        private void OpslaanEnSluitenButtonClick(object sender, RoutedEventArgs e)
        {
            offerte.AantalProducten = offerte.BerekenTotaleAantalProducten();
            offerte.RekenAf(); // to be sure

            tuincentrumManager.UploadOfferteEnProducten(offerte);

            MessageBox.Show("Offerte opgeslagen en gesloten", "Succes");

            Close();
        }

        private void VerwijderAlleProductenButtonClick(object sender, RoutedEventArgs e)
        {
            GeselecteerdeProducten.Clear();
            foreach (var p in offerte.Producten)
            {
                offerte.VerwijderProduct(p.Key, p.Value);
            }
            UpdatePrijsText();
        }

        private void VerwijderProductenButtonClick(object sender, RoutedEventArgs e)
        {
            List<Product> producten = new();
            foreach (Product v in GeselecteerdeProductenListBox.SelectedItems) producten.Add(v);
            foreach (Product v in producten)
            {
                AantalWindow aw = new AantalWindow();

                if (aw.ShowDialog() == true)
                {
                    int aantal = aw.Aantal;
                    offerte.VerwijderProduct(v, aantal);

                    if (offerte.Producten.ContainsKey(v))
                    {
                        MessageBox.Show($"{aantal} verwijderd, U heeft nog {offerte.Producten[v]} over", "Alert", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        GeselecteerdeProducten.Remove(v);
                        MessageBox.Show($"Alles verwijderd", "Alert", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }
        }

        private void VoegProductButtonClick(object sender, RoutedEventArgs e)
        {
            foreach (Product p in AlleProductenListBox.SelectedItems)
            {
                int aantal = 0;
                AantalWindow aw = new AantalWindow();

                if (aw.ShowDialog() == true)
                {
                    aantal = aw.Aantal;
                    offerte.VoegProduct(p, aantal);
                }

                GeselecteerdeProducten.Add(p); // just for show
            }
        }

        private void ZoekProductButtonClick(object sender, RoutedEventArgs e)
        {
            AlleProductenListBox.ItemsSource = tuincentrumManager.GeefProduct(ZoekProductTextBox.Text);
        }

        private void FilterWissenButtonClick(object sender, RoutedEventArgs e)
        {
            ZoekProductTextBox.Text = "";
            AlleProductenListBox.ItemsSource = AlleProducten;
        }

        private void AanlegJaRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            if (offerte != null)
            {
                offerte.Aanleg = true;
                UpdatePrijsText();
            }
        }

        private void AanlegNeeRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            if (offerte != null)
            {
                offerte.Aanleg = false;
                UpdatePrijsText();
            }
        }

        private void AfhaalJaRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            if (offerte != null)
            {
                offerte.Afhaal = true;
                UpdatePrijsText();
            }
        }

        private void AfhaalNeeRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            if (offerte != null)
            {
                offerte.Afhaal = false;
                UpdatePrijsText();
            }
        }
    }
}
