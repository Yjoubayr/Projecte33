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
        public List<Music> musics = new List<Music>();
        public List<string> nomsMusics = new List<string>();

        public PageEditaGrup(string NomGrup)
        {
            InitializeComponent();
            this.ObtenirGrup(NomGrup);
            this.ObtenirMusics();
        }

        private async void btEliminar_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;

            if (btn != null && btn.DataContext is Music music)
            {
                foreach (Music musicLlista in this.grup.LMusics)
                {
                    if (musicLlista.Nom == music.Nom)
                    {
                        this.grup.LMusics.Remove(musicLlista);
                        await CA_Music.UpdateGrupAsync(this.grup);
                        this.ObtenirGrup(this.grup.Nom);
                        MessageBox.Show("Grup modificat correctament");
                    }
                }
            }
        }

        private async void ObtenirNomsMusics(Grup grup)
        {
            MusicsGrup.ItemsSource = grup.LMusics;
            MusicsGrup.AutoGenerateColumns = false;

            DataGridTextColumn nomMusicColumn = new DataGridTextColumn();
            nomMusicColumn.Header = "NomMusic";
            nomMusicColumn.Binding = new System.Windows.Data.Binding("Nom");
            MusicsGrup.Columns.Add(nomMusicColumn);

            DataGridTemplateColumn eliminarCancoColumn = new DataGridTemplateColumn();
            eliminarCancoColumn.Header = "Eliminar Músic";
            FrameworkElementFactory btnEliminar = new FrameworkElementFactory(typeof(Button));
            btnEliminar.SetValue(Button.ContentProperty, "Eliminar");
            btnEliminar.AddHandler(Button.ClickEvent, new RoutedEventHandler(btEliminar_Click));
            eliminarCancoColumn.CellTemplate = new DataTemplate() { VisualTree = btnEliminar };
            MusicsGrup.Columns.Add(eliminarCancoColumn);
        }

        private async void ObtenirMusics()
        {
            this.musics = await CA_Music.GetMusicsAsync();
            this.nomsMusics = new List<string>();

            for (int i = 0; i < musics.Count; i++)
            {
                nomsMusics.Add(musics[i].Nom);
            }

            comboBoxMusics.ItemsSource = nomsMusics;
        }

        private async void ObtenirGrup(string NomGrup)
        {
            this.grup = await CA_Grup.GetGrupAsync(NomGrup);
            this.LblNomGrup.Content = "Nom: " + this.grup.Nom;
            this.LblAnyGrup.Content = "Any: " + this.grup.Any;
            this.ObtenirNomsMusics(this.grup);
        }

        private async void btOk_Click(object sender, RoutedEventArgs e)
        {
            if (comboBoxMusics.SelectedItem != null)
            {
                Music music = await CA_Music.GetMusicAsync(comboBoxMusics.SelectedItem.ToString());
                this.grup.LMusics.Add(music);
                await CA_Music.UpdateGrupAsync(this.grup);
                this.ObtenirGrup(this.grup.Nom);
                MessageBox.Show("Músic afegit al Grup CORRECTAMENT!");
            }
            else
            {
                MessageBox.Show("ERROR! \n Cal que especifiquis quin Músic vols afegir.");
            }
        }
    }
}
