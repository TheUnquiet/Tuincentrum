using BL.Interfaces;
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

namespace UI_DataUpload
{
    /// <summary>
    /// Interaction logic for ResultWindow.xaml
    /// </summary>
    public partial class ResultWindow : Window
    {
        private string connectionstring = @"Data Source=LAPTOP-RQN2J66V\\SQLEXPRESS;Initial Catalog=Tuincentrum;Integrated Security=True;Encrypt=True;Trust Server Certificate=True";
        private ITuincentrumRepository tuincentrumRepository;

        public ResultWindow()
        {
            InitializeComponent();
            FileNameTextBox.Text = "Hello";
            ResultsListBox.ItemsSource = "Hello";

            
        }

        public void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void UploadButton_Click(object sender, RoutedEventArgs e)
        {
            foreach (var item in ResultsListBox.ItemsSource)
            {
                tuincentrumRepository.SchrijfKlant((Klant)item);
            }
        }
    }
}
