using ReproductorMusicaComponentLibrary.Classes;
using ReproductorMusicaComponentLibrary.ConnexioAPI;
using System.Windows;
using System.Windows.Controls;

namespace TaulerDeControlRM.CreaPages
{
    /// <summary>
    /// Lógica de interacción para PageCreaAlbum.xaml
    /// </summary>
    public partial class PageCreaAlbum : Page
    {
        public PageCreaAlbum()
        {
            InitializeComponent();
            getIDsCancons();
        }

        /// <summary>
        /// 
        /// </summary>
        private async void getIDsCancons()
        {
            List<Canco> cancons = await CA_Canco.GetCanconsAsync();
            List<string> idsCancons = new List<string>();

            for (int i = 0; i < cancons.Count; i++)
            {
                idsCancons.Add(cancons[i].IDCanco);
            }

            elementComboBox.ItemsSource = idsCancons;
        }
        /// <summary>
        /// EventListener boto
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btOk_Click(object sender, RoutedEventArgs e)
        {
            if (elementComboBox.SelectedItem != null)
            {
                Album album = new Album();
                album.Titol = this.txtAlbumTitle.Text.ToString();
                album.Any = int.Parse(this.txtAlbumYear.Text);
                album.IDCanco = elementComboBox.SelectedItem.ToString();
                await CA_Album.PostAlbumAsync(album);
            }
        }
    }
}
