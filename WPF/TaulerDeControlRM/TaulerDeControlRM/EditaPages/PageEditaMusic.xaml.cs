using ReproductorMusicaComponentLibrary.Classes;
using ReproductorMusicaComponentLibrary.ConnexioAPI;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TaulerDeControlRM.EditaPages
{
    /// <summary>
    /// Lógica de interacción para PageEditaMusic.xaml
    /// </summary>
    public partial class PageEditaMusic : Page
    {
        private Music music = new Music();
        private List<string> nomsGrups = new List<string>();

        public PageEditaMusic(string NomMusic)
        {
            InitializeComponent();
            this.ObtenirMusic(NomMusic);
            LblNomMusic.Content = "Músic: " + music.Nom;
        }

        private async void ObtenirNomsMusics()
        {
            for (int i = 0; i < this.music.LGrups.Count; i++)
            {
                this.nomsGrups.Add(this.music.LGrups[i].Nom);
            }
        }

        private async void ObtenirMusic(string NomMusic)
        {
            this.music = await CA_Music.GetMusicAsync(NomMusic);
            this.ObtenirNomsMusics();
        }

        private async void Save_Click(object sender, RoutedEventArgs e)
        {
            await CA_Grup.UpdateMusicAsync(this.music);
            MessageBox.Show("Grup modificat correctament");
        }
    }
}
