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
        private List<string> llistaPKComposta = new List<string>();

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
                        Buscador.SelectedItem = null;
                        this.ObtenirAlbums();
                        ComboBoxAlbumYear.Visibility = Visibility.Visible;
                        break;
                    case "Cançó":
                        Buscador.IsTextSearchEnabled = true;
                        Buscador.SelectedItem = null;
                        this.ObtenirCancons();
                        Buscador.ItemsSource = this.llistaPK;
                        break;
                    case "Grup":
                        Buscador.IsTextSearchEnabled = true;
                        Buscador.SelectedItem = null;
                        break;
                    case "Músic":
                        Buscador.IsTextSearchEnabled = true;
                        Buscador.SelectedItem = null;
                        this.ObtenirMusics();
                        Buscador.ItemsSource = this.llistaPK;
                        frameEditar.Navigate(new PageEditaMusic());
                        break;
                        // Add more cases as needed
                }
            }
        }

        /// <summary>
        /// Obtenir tots els anys d'un Album segons el seu Titol
        /// </summary>
        private async void MostrarClauComposta(object sender, SelectionChangedEventArgs e)
        {
            switch (elementComboBox.SelectedItem.ToString()) {
                case "Àlbum":
                    this.llistaPKComposta = await CA_Album.GetAnysAlbumAsync(Buscador.SelectedItem.ToString());
                    ComboBoxAlbumYear.ItemsSource = this.llistaPKComposta;
                break;
            }
        }

        /// <summary>
        /// Obtenim tots els Albums fent una crida a l'API
        /// </summary>
        private async void ObtenirAlbums()
        {
            this.llistaAlbums = await CA_Album.GetAlbumsAsync();

            this.llistaPK = new List<string>();

            for (int i = 0; i < this.llistaAlbums.Count; i++)
            {
                this.llistaPK.Add(this.llistaAlbums[i].Titol);
            }

            Buscador.ItemsSource = this.llistaPK;
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

        /// <summary>
        /// Un cop l'usuari ha especificat l'objecte que vol editar 
        /// juntament amb la seva classe, el portarà a la pàgina 
        /// per editar-lo
        /// </summary>
        private void Cerca_Click(object sender, RoutedEventArgs e)
        {
            if (elementComboBox.SelectedItem != null && Buscador.SelectedItem != null)
            {

                if (elementComboBox.SelectedItem.ToString() == "Àlbum" && ComboBoxAlbumYear.SelectedItem != null)
                {
                    frameEditar.Navigate(new PageEditaAlbum(Buscador.Text, ComboBoxAlbumYear.Text));

                } else if (elementComboBox.SelectedItem.ToString() == "Àlbum" 
                    && ComboBoxAlbumYear.SelectedItem == null)
                {
                    MessageBox.Show("ERROR! \n Especifica de quin Any és l'Album que busques");
                }


                if (elementComboBox.SelectedItem.ToString() == "Cançó")
                {
                    frameEditar.Navigate(new PageEditaCanco(Buscador.Text));
                }

                if (elementComboBox.SelectedItem.ToString() == "Grup")
                {

                }
                
                if (elementComboBox.SelectedItem.ToString() == "Músic")
                {
                    frameEditar.Navigate(new PageCreaMusic());
                } 
            }
            else
            {
                Buscador.IsTextSearchEnabled = false;
                MessageBox.Show("ERROR! \n Cal que especifiquis la classe i quin objecte vols editar");
            }

            songListView.Visibility = Visibility.Visible;
        }

    }
}
