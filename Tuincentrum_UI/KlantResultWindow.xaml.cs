using BL.Models;
using System;
using System.Collections.Generic;
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
        public KlantResultWindow(Klant k, List<Offerte> offertesKlant)
        {
            InitializeComponent();
            IdTextBox.Text = k.Id.ToString();
            NaamTextBox.Text = k.Naam;
            AdresTextBox.Text = k.Adres;
            OffertesListBox.ItemsSource = offertesKlant;
        }

        private void ButtonClickClose(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
