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
using TaulerDeControlRM.CreaPages;
using TaulerDeControlRM.EditaPages;

namespace TaulerDeControlRM
{
    /// <summary>
    /// Interaction logic for PageEdita.xaml
    /// </summary>
    public partial class PageEdita : Page
    {
        private List<Canco> llistaCancons = new List<Canco>();
        private List<Album> llistaAlbums = new List<Album>();
        private List<Music> llistaMusics = new List<Music>();
        private List<string> llistaPK = new List<string>();
        private List<List<string>> llistaPKComposta = new List<List<string>>();

        public PageEdita()
        {
            InitializeComponent();
            elementComboBox.ItemsSource = new List<string> { "Àlbum", "Cançó", "Grup", "Músic", "Llista de reproducció" };
        }
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            // Get the selected item from the ListBox
            //ListBoxItem selectedListBoxItem = (ListBoxItem)elementComboBox.SelectedItem;



            // Determine which item was selected and update the main content accordingly
            if (elementComboBox.SelectedItem != null)
            {
                switch (elementComboBox.SelectedItem.ToString())
                {
                    case "Àlbum":
                        Buscador.IsTextSearchEnabled = true;
                        Buscador.ItemsSource = this.llistaPK;
                        frameEditar.Navigate(new PageCreaAlbum());
                        break;
                    case "Cançó":
                        Buscador.IsTextSearchEnabled = true;
                        this.ObtenirCancons();
                        Buscador.ItemsSource = this.llistaPK;
                        break;
                    case "Grup":
                        Buscador.IsTextSearchEnabled = true;
                        break;
                    case "Músic":
                        Buscador.IsTextSearchEnabled = true;
                        this.ObtenirMusics();
                        Buscador.ItemsSource = this.llistaPK;
                        frameEditar.Navigate(new PageCreaMusic());
                        break;
                        // Add more cases as needed
                }
            }
        }

        /// <summary>
        /// Un cop l'usuari ha especificat l'objecte que vol editar 
        /// juntament amb la seva classe, el portarà a la pàgina 
        /// per editar-lo
        /// </summary>
        private void Cerca_Click(object sender, RoutedEventArgs e)
        {
            if (elementComboBox.SelectedItem != null && Buscador.SelectedItem != null)
            {
                switch (elementComboBox.SelectedItem.ToString())
                {
                    case "Àlbum":
                        frameEditar.Navigate(new PageCreaAlbum());
                        break;
                    case "Cançó":
                        frameEditar.Navigate(new PageEditaCanco(Buscador.Text));
                        break;
                    case "Grup":
                        Buscador.IsTextSearchEnabled = true;
                        break;
                    case "Músic":
                        frameEditar.Navigate(new PageCreaMusic());
                        break;
                        // Add more cases as needed
                }
            }
            else
            {
                Buscador.IsTextSearchEnabled = false;
                MessageBox.Show("Cal que especifiquis la classe i quin objecte vols editar");
            }

            songListView.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Obtenim tots els Albums fent una crida a l'API
        /// </summary>
        private async void ObtenirAlbums()
        {
            this.llistaAlbums = await CA_Album.GetAlbumsAsync();

            this.llistaPK = new List<string>();

            for (int i = 0; i < this.llistaCancons.Count; i++)
            {
                this.llistaPKComposta.Add(new List<string> { this.llistaAlbums[i].Titol, llistaAlbums[i].Any.ToString(), this.llistaAlbums[i].IDCanco });
            }
        }

        /// <summary>
        /// Obtenim totes les Cancons fent una crida a l'API
        /// </summary>
        private async void ObtenirCancons()
        {
            this.llistaCancons = await CA_Canco.GetCanconsAsync();

            this.llistaPK = new List<string>();

            for (int i = 0; i < this.llistaCancons.Count; i++)
            {
                this.llistaPK.Add(llistaCancons[i].IDCanco);
            }

            Buscador.ItemsSource = this.llistaPK;
        }

        /// <summary>
        /// Obtenim totes les Musics fent una crida a l'API
        /// </summary>
        private async void ObtenirMusics()
        {
            this.llistaMusics = await CA_Music.GetMusicsAsync();

            this.llistaPK = new List<string>();

            for (int i = 0; i < this.llistaMusics.Count; i++)
            {
                this.llistaPK.Add(llistaMusics[i].Nom);
            }
        }
    }
}
