using BL.Interfaces;
using BL.Managers;
using BL.Models;
using DL_Data;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Tuincentrum_UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ITuincentrumRepository tuincentrumRepository;
        private TuincentrumManager tuincentrumManager;
        private ObservableCollection<Klant> klanten;
        public MainWindow()
        {
            InitializeComponent();
            tuincentrumRepository = new TuincentrumRepository(ConfigurationManager.ConnectionStrings["TuincentrumDBConnectionLaptop"].ToString());
            tuincentrumManager = new TuincentrumManager(tuincentrumRepository);
            klanten = new ObservableCollection<Klant>();
        }

        private void ZoekKlantButtonClick(object sender, RoutedEventArgs e)
        {
            ZoekKlantWindow zk = new ZoekKlantWindow();
            zk.Show();
        }

        private void ZoekOfferteButtonClick(object sender, RoutedEventArgs e)
        {
            ZoekOfferte zoek = new ZoekOfferte();
            zoek.Show();
        }
    }
}