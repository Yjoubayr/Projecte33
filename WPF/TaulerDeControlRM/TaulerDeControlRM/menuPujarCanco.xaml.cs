using Microsoft.Win32;
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


namespace TaulerDeControlRM
{
    /// <summary>
    /// Interaction logic for menuPujarCanco.xaml
    /// </summary>
    public partial class menuPujarCanco : Window
    {
        public menuPujarCanco()
        {
            InitializeComponent();
            List<string> musicGroupsList = GetMusicGroups(); // You should replace this with your logic to get the actual list
            cvArtistes.LblValue = "Artista";
            cvArtistes.lblValue.Background = Brushes.LightBlue;
            cvArtistes.Valors = new List<string> { "Joan", "Pep", "Francesc" };

            // Set the ComboBox items source
            cmbGrup.ItemsSource = musicGroupsList;


        }
        private void BrowseButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            if (openFileDialog.ShowDialog() == true)
            {
                // Set the selected file path to the TextBox
                FilePathTextBox.Text = openFileDialog.FileName;
            }
        }
        private void Upload_Click(object sender, RoutedEventArgs e)
        {

        }
        private List<string> GetMusicGroups()
        {
            List<string> groups = new List<string>
            {
                "Crea nou grup",
                "Group 1",
                "Group 2",
                "Group 3"
                // Add more group names as needed
            };

            return groups;
        }
    }
}
