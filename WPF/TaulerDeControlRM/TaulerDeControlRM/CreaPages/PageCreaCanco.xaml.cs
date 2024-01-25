using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using ReproductorMusicaComponentLibrary.Classes;
using ReproductorMusicaComponentLibrary.ConnexioAPI;

namespace TaulerDeControlRM
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class PageCreaCanco : Page
    {
        public ObservableCollection<string> YourStringList { get; set; }
        public PageCreaCanco()
        {
            InitializeComponent();


            ConjuntValors cvMusics = new ConjuntValors("Músic",new List<string>{"Joan","Josep","Ferran","Maria","Miquel"},false,true);

            ConjuntValors cvInstrument = new ConjuntValors("Instrument", new List<string> { "Flauta", "Guitarra", "Trompeta" }, true, true);

            List<ConjuntValors> llistaConjutValors = new List<ConjuntValors> { cvMusics, cvInstrument};

            GridConjuntValors gcv=new GridConjuntValors(true, llistaConjutValors);
            spMusics.Children.Add(gcv);
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


        private async void btOk_Click(object sender, RoutedEventArgs e)
        {
            string cancoName = this.txtCancoName.Text.ToString();
            string cancoYear = this.txtCancoYear.Text.ToString();
            
            Canco canco = new Canco();
            canco.Nom = cancoName;
            canco.Any = int.Parse(cancoYear);
            await CA_Canco.PostCancoAsync(canco);
        }
    }
}
