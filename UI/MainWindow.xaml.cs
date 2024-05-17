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
using BL.Models;
using UI_DataUpload;
using BL.Interfaces;
using DL_Data;
using System.Configuration;

namespace UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private OpenFileDialog fileDialog = new OpenFileDialog();
        private OpenFolderDialog folderDialog = new OpenFolderDialog();
        private IFileProcessor fileProcessor;
        private ITuincentrumRepository tuincentrumRepository;
        private FileManager fileManager;
        private TuincentrumManager tuincentrumManager;
        
        public MainWindow()
        {
            InitializeComponent();
            fileDialog.DefaultExt = ".zip";
            fileDialog.Filter = "Zip files (.zip)| *.zip";
            fileDialog.InitialDirectory = @"C:\data\";
            fileDialog.Multiselect = false;
            folderDialog.InitialDirectory = @"C:\data";

            fileProcessor = new FileProcessor();
            tuincentrumRepository = new TuincentrumRepository(ConfigurationManager.ConnectionStrings["TuincentrumDBConnectionLaptop"].ToString());
            fileManager = new FileManager(fileProcessor);
            tuincentrumManager = new TuincentrumManager(tuincentrumRepository, fileProcessor);
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

        private void DestinationFolderButton_Click(object sender, RoutedEventArgs e)
        {
            bool? result = folderDialog.ShowDialog();
            if (result == true && !string.IsNullOrWhiteSpace(folderDialog.FolderName))
            {
                if (!fileManager.IsFolderEmpty(folderDialog.FolderName))
                {
                    if (MessageBox.Show($"Clean folder {folderDialog.FolderName}", "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        fileManager.CleanFolder(folderDialog.FolderName);
                        DestinationFolderTextBox.Text = folderDialog.FolderName;
                    }
                }
                else
                {
                    DestinationFolderTextBox.Text = folderDialog.FolderName;
                }
            }
        }

        private void ExcecuteButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                fileManager.ProcessZip(SourceFileTextBox.Text, DestinationFolderTextBox.Text);
                tuincentrumManager.UploadKlanten(DestinationFolderTextBox.Text + "\\" + ZipFileListBox.Items[0]);
                tuincentrumManager.UploadBestellingen(DestinationFolderTextBox.Text + "\\" + ZipFileListBox.Items[1]);
                tuincentrumManager.UploadOffertes(DestinationFolderTextBox.Text + "\\" + ZipFileListBox.Items[2]);
                tuincentrumManager.UploadProducten(DestinationFolderTextBox.Text + "\\" + ZipFileListBox.Items[3]);

                MessageBox.Show("Upload klaar!");

            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "FileManager", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}