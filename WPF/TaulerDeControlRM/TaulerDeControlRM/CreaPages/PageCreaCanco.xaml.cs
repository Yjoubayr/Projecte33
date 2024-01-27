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
using System.Text.RegularExpressions;
using static System.Net.Mime.MediaTypeNames;
using Newtonsoft.Json;

namespace TaulerDeControlRM
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class PageCreaCanco : Page
    {
        public ObservableCollection<string> YourStringList { get; set; }

        //Creem l'objecte de la Cancçó que volem afegir, en les funcions que veuràs a continuació, serà quan se l'hi assignin els valors
        private Canco canco = new Canco();
        private Extensio e = new Extensio();
        private static readonly Regex _regex = new Regex("[^0-9.-]+"); //regex that matches disallowed text
        private List<Music> llistaMusics = new List<Music>();
        private List<string> nomsMusics = new List<string>();

        public  PageCreaCanco()
        {
            InitializeComponent();

            //2 coses:
            //contemplar músic 2 o més instruments
            //contemplar 2 o més registres nous iguals

            this.ObtenirMusics();
        }

        private async void ObtenirMusics()
        {
            this.llistaMusics = await CA_Music.GetMusicsAsync();

            this.nomsMusics = new List<string>();

            for (int i = 0; i < this.llistaMusics.Count; i++)
            {
                this.nomsMusics.Add(llistaMusics[i].Nom);
            }

            CrearLlistaConjuntValors();
        }

        private async void CrearLlistaConjuntValors()
        {
            //ConjuntValors cvMusics = new ConjuntValors("Músic",new List<string>{"Joan","Josep","Ferran","Maria","Miquel"},true,true);

            ConjuntValors cvInstrument = new ConjuntValors("Instrument", new List<string> { "Flauta", "Guitarra", "Trompeta" }, true, true);
            ConjuntValors cvMusics = new ConjuntValors("Músic", this.nomsMusics, true, true);
            List<ConjuntValors> llistaConjutValors = new List<ConjuntValors> { cvMusics, cvInstrument };

            GridConjuntValors gcv = new GridConjuntValors(true, llistaConjutValors);
            spMusics.Children.Add(gcv);

        }

        private void BrowseButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            if (openFileDialog.ShowDialog() == true)
            {
                // Set the selected file path to the TextBox
                FilePathTextBox.Text = openFileDialog.FileName;
                string[] cancoSeparada = FilePathTextBox.Text.Split('.');
                string nomExtensio = cancoSeparada[cancoSeparada.Length - 1];
                this.e.Nom = nomExtensio;
            }
        }
        private void Upload_Click(object sender, RoutedEventArgs e)
        {
            if (this.txtNom.Text.ToString() != string.Empty && this.txtAny.Text.ToString() != string.Empty && !_regex.IsMatch(this.txtAny.Text.ToString()))
            {
                this.canco.Nom = this.txtNom.Text.ToString();
                this.canco.Any = int.Parse(this.txtAny.Text);

                if (this.e != new Extensio())
                {
                    this.canco.LExtensions.Add(this.e);
                }

                MessageBox.Show("Nom: " + this.canco.Nom + " Any: " + this.canco.Any + " LExtensions: " + JsonConvert.SerializeObject(this.canco.LExtensions));
                //await CA_Canco.PostCancoAsync(this.canco);

            } else
            {
                MessageBox.Show("ERROR! \n Emplena els camps abans de pujar la cançó i que estiguin en el format correcte.");
            }

        }
    }
}
