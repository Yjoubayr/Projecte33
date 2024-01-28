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
        private List<Grup> llistaGrups = new List<Grup>();
        private List<string> nomsGrups = new List<string>();

        public PageEditaGrup()
        {
            InitializeComponent();
            ConjuntValors cvMusics = new ConjuntValors("Músic", new List<string> { "Joan", "Josep", "Ferran", "Maria", "Miquel" }, false, true);
            List<ConjuntValors> llistaConjutValors = new List<ConjuntValors> { cvMusics };
            GridConjuntValors gcv = new GridConjuntValors(false, llistaConjutValors);
            spMusics.Children.Add(gcv);
        }

        private async void CrearLlistaConjuntValors()
        {
            ConjuntValors cvInstrument = new ConjuntValors("Instrument", new List<string> { "Flauta", "Guitarra", "Trompeta" }, true, true);
            ConjuntValors cvGrups = new ConjuntValors("Grup", this.nomsGrups, true, true);
            List<ConjuntValors> llistaConjutValors = new List<ConjuntValors> { cvGrups, cvInstrument };

            GridConjuntValors gcv = new GridConjuntValors(true, llistaConjutValors);
            spMusics.Children.Add(gcv);

        }

        private async void ObtenirGrups()
        {
            this.llistaGrups = await CA_Grup.GetGrupsAsync();

            this.nomsGrups = new List<string>();

            for (int i = 0; i < this.llistaGrups.Count; i++)
            {
                this.nomsGrups.Add(llistaGrups[i].Nom);
            }

            CrearLlistaConjuntValors();
        }

        private void Upload_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
