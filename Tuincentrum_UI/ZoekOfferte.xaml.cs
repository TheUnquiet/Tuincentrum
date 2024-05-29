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
    /// Interaction logic for ZoekOfferte.xaml
    /// </summary>
    public partial class ZoekOfferte : Window
    {
        private TuincentrumManager tuincentrumManager;
        private ITuincentrumRepository tuincentrumRepository;
        public ZoekOfferte()
        {
            InitializeComponent();
            tuincentrumRepository = new TuincentrumRepository(ConfigurationManager.ConnectionStrings["TuincentrumDBConnectionLaptop"].ToString());
            tuincentrumManager = new TuincentrumManager(tuincentrumRepository);
        }

        private void ZoekOfferteButtonClick(object sender, RoutedEventArgs e)
        {
            Offerte o = tuincentrumManager.GeefOfferte(int.Parse(IdTextBox.Text));
            if (o != null)
            {
                OfferteDetailsWindow odw = new OfferteDetailsWindow(o, tuincentrumManager.GeefKlant(o.KlantNummer));
                odw.Show();
            }
            else
            {
                MessageBox.Show("Offerte niet gevonden", "Notification");
            }
        }
    }
}
