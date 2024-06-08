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
    /// Interaction logic for AantalWindow.xaml
    /// </summary>
    public partial class AantalWindow : Window
    {
        public int Aantal {  get; private set; }

        public AantalWindow()
        {
            InitializeComponent();
        }

        public void ZetAantalButtonClick(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(AantalTextBox.Text, out int aantal))
            {
                Aantal = aantal;
                DialogResult = true;
            }
            else
            {
                MessageBox.Show("Alleen nummers zijn toegestaan.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
