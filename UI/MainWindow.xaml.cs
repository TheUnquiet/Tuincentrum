using DL_DataUpload;
using BL.Managers;
using Microsoft.Win32;
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

namespace UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private OpenFileDialog fileDialog = new OpenFileDialog();
        private OpenFolderDialog folderDialog = new OpenFolderDialog();
        private FileManager fileManager = new FileManager(new FileProcessor());

        public MainWindow()
        {
            InitializeComponent();
            fileDialog.DefaultExt = ".zip";
            fileDialog.Filter = "Zip files (.zip)| *.zip";
            fileDialog.InitialDirectory = @"C:\data\tuinData";
            fileDialog.Multiselect = false;
            folderDialog.InitialDirectory = @"C:\data\tuinData";
        }

        private void OpenZipFileButton_Click(object sender, RoutedEventArgs e)
        {
            bool? res = fileDialog.ShowDialog();

            if (res == true && !string.IsNullOrEmpty(fileDialog.FileName))
            {
                SourceFileTextBox.Text = fileDialog.FileName;

                List<string> fileNames = fileManager.GetFilesFromZip(fileDialog.FileName);
                ZipFileListBox.ItemsSource = fileNames;
            }
        }

        private void OpenDestinationButton_Click(object sender, RoutedEventArgs e)
        {
            bool? res = fileDialog.ShowDialog();
            if (res == true && !string.IsNullOrEmpty(fileDialog.FileName))
            {
                //
            }
        }

        private void ExcecuteButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}