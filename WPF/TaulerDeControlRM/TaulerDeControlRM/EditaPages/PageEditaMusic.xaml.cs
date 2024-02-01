using ReproductorMusicaComponentLibrary.Classes;
using ReproductorMusicaComponentLibrary.ConnexioAPI;
using System;
using System.Collections.Generic;
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

namespace TaulerDeControlRM.EditaPages
{
    /// <summary>
    /// Lógica de interacción para PageEditaMusic.xaml
    /// </summary>
    public partial class PageEditaMusic : Page
    {
        private Music music = new Music();
        private List<Grup> grups = new List<Grup>();
        private List<string> nomsGrups = new List<string>();

        public PageEditaMusic(string NomMusic)
        {
            InitializeComponent();
            this.ObtenirMusic(NomMusic);
            this.ObtenirGrups();
            LblNomMusic.Content = "Músic: " + music.Nom;
        }

        private async void btEliminar_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;

            if (btn != null && btn.DataContext is Grup grup)
            {
                this.music.LGrups.Remove(grup);
                await CA_Grup.UpdateMusicAsync(this.music);
                this.ObtenirMusic(this.music.Nom);
                MessageBox.Show("Músic modificat correctament");
            }
        }

        private async void ObtenirNomsGrups(Music music)
        {
            for (int i = 0; i < this.music.LGrups.Count; i++)
            {
                this.nomsGrups.Add(this.music.LGrups[i].Nom);
            }

            GrupsMusic.ItemsSource = music.LGrups;
            GrupsMusic.AutoGenerateColumns = false;

            DataGridTextColumn nomGrupColumn = new DataGridTextColumn();
            nomGrupColumn.Header = "NomGrup";
            nomGrupColumn.Binding = new System.Windows.Data.Binding("Nom");
            GrupsMusic.Columns.Add(nomGrupColumn);

            DataGridTemplateColumn eliminarGrupColumn = new DataGridTemplateColumn();
            eliminarGrupColumn.Header = "Eliminar Grup";
            FrameworkElementFactory btnEliminar = new FrameworkElementFactory(typeof(Button));
            btnEliminar.SetValue(Button.ContentProperty, "Eliminar");
            btnEliminar.AddHandler(Button.ClickEvent, new RoutedEventHandler(btEliminar_Click));
            eliminarGrupColumn.CellTemplate = new DataTemplate() { VisualTree = btnEliminar };
            GrupsMusic.Columns.Add(eliminarGrupColumn);
        }

        private async void ObtenirGrups()
        {
            this.grups = await CA_Grup.GetGrupsAsync();
            this.nomsGrups = new List<string>();

            for (int i = 0; i < grups.Count; i++)
            {
                this.nomsGrups.Add(this.grups[i].Nom);
            }

            comboBoxGrups.ItemsSource = this.nomsGrups;
        }

        private async void ObtenirMusic(string NomMusic)
        {
            this.music = await CA_Music.GetMusicAsync(NomMusic);
            this.LblNomMusic.Content = "Musc: " + this.music.Nom;
            this.ObtenirNomsGrups(music);
        }

        private async void btOk_Click(object sender, RoutedEventArgs e)
        {
            if (comboBoxGrups.SelectedItem != null)
            {
                Grup grup = await CA_Grup.GetGrupAsync(comboBoxGrups.SelectedItem.ToString());
                this.music.LGrups.Add(grup);
                await CA_Grup.UpdateMusicAsync(this.music);
                this.ObtenirMusic(this.music.Nom);
                MessageBox.Show("Grup afegit al Músic CORRECTAMENT!");
            }
            else
            {
                MessageBox.Show("ERROR! \n Cal que especifiquis quin Grup vols afegir.");
            }
        }
    }
}
