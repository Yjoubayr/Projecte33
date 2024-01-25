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
using System.Windows.Shapes;

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
