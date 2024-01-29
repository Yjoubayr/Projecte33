using ReproductorMusicaComponentLibrary.Classes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using Microsoft.Win32;

namespace TaulerDeControlRM.EditaPages
{
    /// <summary>
    /// Lógica de interacción para PageEditaCanco.xaml
    /// </summary>
    public partial class PageEditaCanco : Page
    {
        public ObservableCollection<string> YourStringList { get; set; }

        //Creem l'objecte de la Cancçó que volem editar, en les funcions que veuràs a continuació, serà quan se l'hi assignin els valors
        private Canco canco = new Canco();
        private Extensio e = new Extensio();
        private static readonly Regex _regex = new Regex("[^0-9.-]+"); //regex that matches disallowed text
        private List<Music> llistaMusics = new List<Music>();
        private List<Instrument> llistaInstruments = new List<Instrument>();
        private List<Extensio> llistaExtensions = new List<Extensio>();
        private List<string> nomsMusics = new List<string>();
        private List<string> nomsInstruments = new List<string>();
        private List<string> nomsExtensions = new List<string>();
        private string IDCanco = string.Empty;

        public PageEditaCanco(string IDCanco)
        {
            InitializeComponent();
            this.IDCanco = IDCanco;
            ObtenirCanco();
            CrearLlistaConjuntValors();
        }

        /// <summary>
        /// Obtenim l'objecte de la Canco a Editar
        /// </summary>
        private async void ObtenirCanco()
        {
            this.canco = await CA_Canco.GetCancoAsync(this.IDCanco);
        }

        /// <summary>
        /// Obtenim tots els musics fent una crida a l'API
        /// </summary>
        private async void ObtenirMusics()
        {
            this.llistaMusics = await CA_Music.GetMusicsAsync();

            this.nomsMusics = new List<string>();

            for (int i = 0; i < this.llistaMusics.Count; i++)
            {
                this.nomsMusics.Add(llistaMusics[i].Nom);
            }

        }

        /// <summary>
        /// Obtenim totes les Extensions fent una crida a l'API
        /// </summary>
        private async void ObtenirExtensions()
        {
            this.llistaExtensions = await CA_Extensio.GetExtensionsAsync();

            this.nomsExtensions = new List<string>();

            for (int i = 0; i < this.llistaExtensions.Count; i++)
            {
                this.nomsMusics.Add(llistaExtensions[i].Nom);
            }

        }

        private async void CrearLlistaConjuntValors()
        {

            ConjuntValors cvInstrument = new ConjuntValors("Instrument", new List<string> { "Flauta", "Guitarra", "Trompeta" }, true, true);
            ConjuntValors cvMusics = new ConjuntValors("Músic", this.nomsMusics, true, true);
            ConjuntValors cvExtensions = new ConjuntValors("Extensió", this.nomsExtensions, true, true);
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
            if (txtNom.Text.ToString() != string.Empty && this.txtAny.Text.ToString() != string.Empty && !_regex.IsMatch(this.txtAny.Text.ToString()))
            {
                this.canco.Nom = this.txtNom.Text.ToString();
                this.canco.Any = int.Parse(this.txtAny.Text);

                if (this.e != new Extensio())
                {
                    this.canco.LExtensions.Add(this.e);
                }

                MessageBox.Show("Nom: " + this.canco.Nom + " Any: " + this.canco.Any + " LExtensions: " + JsonConvert.SerializeObject(this.canco.LExtensions));
                //await CA_Canco.PostCancoAsync(this.canco);

            }
            else
            {
                MessageBox.Show("ERROR! \n Emplena els camps abans de pujar la cançó i que estiguin en el format correcte.");
            }

        }
    }
}
