using ReproductorMusicaComponentLibrary.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using ReproductorMusicaComponentLibrary.ConnexioAPI;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TaulerDeControlRM.EditaPages
{
    /// <summary>
    /// Lógica de interacción para PageEditaGrup.xaml
    /// </summary>
    public partial class PageEditaGrup : Page
    {
        public Grup grup = new Grup();
        public List<string> nomsMusics = new List<string>();

        public PageEditaGrup(string NomGrup)
        {
            InitializeComponent();
            this.ObtenirGrup(NomGrup);
            this.LblNomGrup.Content = "Nom " + this.grup.Nom;
            this.LblAnyGrup.Content = "Any " + this.grup.Any;

            this.CrearLlistaConjuntValors();
        }

        private async void ObtenirNomsMusics()
        {
            for (int i = 0; i < this.grup.LMusics.Count; i++)
            {
                this.nomsMusics.Add(this.grup.LMusics[i].Nom);
            }
        }

        private async void ObtenirGrup(string NomGrup)
        {
            this.grup = await CA_Grup.GetGrupAsync(NomGrup);
            this.ObtenirNomsMusics();
        }

        private async void CrearLlistaConjuntValors()
        {
            ConjuntValors cvMusics= new ConjuntValors("Music", this.nomsMusics, true, true);
            List<ConjuntValors> llistaConjutValors = new List<ConjuntValors> { cvMusics };
            GridConjuntValors gcv = new GridConjuntValors(true, llistaConjutValors);
            spMusics.Children.Add(gcv);
        }

        private async void Save_Click(object sender, RoutedEventArgs e)
        {
            await CA_Music.UpdateGrupAsync(this.grup);
            MessageBox.Show("Grup modificat correctament");
        }
    }
}
