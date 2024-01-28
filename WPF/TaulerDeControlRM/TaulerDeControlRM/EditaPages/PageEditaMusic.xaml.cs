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
        public PageEditaMusic()
        {
            InitializeComponent();
        }
        private void Upload_Click(object sender, RoutedEventArgs e)
        {
            Music music = new Music();
            music.Nom = txtNom.Text;
            music.LGrups = null;

            CA_Music.PostMusicAsync(music);
        }
    }
}
