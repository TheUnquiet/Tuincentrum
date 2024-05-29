using BL.Interfaces;
using BL.Managers;
using BL.Models;
using BL.Models.DTOS;
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
        private ObservableCollection<Product> AlleProducten {  get; set; }
        private ObservableCollection<Product> GeselecteerdeProducten { get; set; }
        private TuincentrumManager tuincentrumManager;
        private ITuincentrumRepository tuincentrumRepository;

        public NieuweOfferteWindow(Klant k)
        {
            InitializeComponent();
            DataContext = this;

            tuincentrumRepository = new TuincentrumRepository(ConfigurationManager.ConnectionStrings["TuincentrumDBConnectionLaptop"].ToString());
            tuincentrumManager = new TuincentrumManager(tuincentrumRepository);

            DatumTextBlock.Text = DateTime.Now.ToString("dd/MM/yyyy");
            KlantIdTextBlock.Text = k.Id.ToString();
            KlantNaamTextBlock.Text = k.Naam;
            KlantAdresTextBlock.Text = k.Adres;
            AlleProducten = new ObservableCollection<Product>(tuincentrumManager.GeefProducten());

            GeselecteerdeProducten = new ObservableCollection<Product>();
            GeselecteerdeProducten.CollectionChanged += GeselecteerdeProducten_CollectionChanged;

            AlleProductenListBox.ItemsSource = AlleProducten;
            GeselecteerdeProductenListBox.ItemsSource = GeselecteerdeProducten;
            UpdateTotalePrijs();
        }
        private void GeselecteerdeProducten_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            UpdateTotalePrijs();
        }

        private void UpdateTotalePrijs()
        {
            double prijs = tuincentrumManager.GeefPrijsOfferte(GeselecteerdeProducten.ToList());
            PrijsTextBlock.Text = $"Prijs : €{prijs}";

        }

        private void OpslaanEnSluitenButtonClick(object sender, RoutedEventArgs e)
        {
            bool aanleg, afhaal;
            if ((bool)AanlegRadioButton.IsChecked) aanleg = true; else aanleg = false;
            if ((bool)AfhaalRadioButton.IsChecked) afhaal = true; else afhaal = false;
            
            Offerte offerte = new(DateTime.Now, int.Parse(KlantIdTextBlock.Text), afhaal, aanleg, GeselecteerdeProducten.Count);
            tuincentrumManager.UploadOfferte(offerte);
            offerte = tuincentrumManager.GeefLaatsteOfferte();

            var aantal_producten = GeselecteerdeProducten
                .GroupBy(product => product.Id)
                .Select(group => new
                {
                    productId = group.Key,
                    Count = group.Count()
                });

            foreach (var group in aantal_producten)
            {
                Bestelling b = new Bestelling(offerte.Id.Value, group.productId.Value, group.Count);
                tuincentrumManager.UploadBestelling(b);
            }


            Close();
        }

        private void VerwijderAlleProductenButtonClick(object sender, RoutedEventArgs e)
        {
            GeselecteerdeProducten.Clear();
        }

        private void VerwijderProductenButtonClick(object sender, RoutedEventArgs e)
        {
            List<Product> producten = new();
            foreach (Product v in GeselecteerdeProductenListBox.SelectedItems) producten.Add(v);
            foreach (Product v in producten)
            {
                GeselecteerdeProducten.Remove(v);
            }
        }

        private void VoegAlleProductenButtonClick(object sender, RoutedEventArgs e)
        {
            foreach (Product v in AlleProducten)
            {
                GeselecteerdeProducten.Add(v);
            }
        }

        private void VoegProductButtonClick(object sender, RoutedEventArgs e)
        {
            List<Product> producten = new();
            foreach (Product p in AlleProductenListBox.SelectedItems) producten.Add(p);
            foreach (Product p in producten)
            {
                GeselecteerdeProducten.Add(p);
            }
        }
    }
}
