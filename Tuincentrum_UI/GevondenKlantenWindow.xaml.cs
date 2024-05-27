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
    /// Interaction logic for GevondenKlantenWindow.xaml
    /// </summary>
    public partial class GevondenKlantenWindow : Window
    {
        public GevondenKlantenWindow(List<Klant> klanten)
        {
            InitializeComponent();
            KlantenListbox.Items.Clear();
            KlantenListbox.ItemsSource = klanten;
        }

        private void ButtonClickClose(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void KlantenListboxDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (KlantenListbox.SelectedItem != null)
            {
                Klant selectedKlant = (Klant)KlantenListbox.SelectedItem;
                KlantResultWindow krw = new(selectedKlant, selectedKlant.Offertes);
                krw.Show();
            }
        }
    }
}
