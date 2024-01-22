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

namespace TaulerDeControlRM
{
    /// <summary>
    /// Interaction logic for PageEdita.xaml
    /// </summary>
    public partial class PageEdita : Page
    {
        public PageEdita()
        {
            InitializeComponent();
            elementComboBox.ItemsSource = new List<string> { "Cançó", "Artista", "Grup", "Àlbum", "Llista de reproducció" };
        }

        private void Cerca_Click(object sender, RoutedEventArgs e)
        {
            songListView.Visibility = Visibility.Visible;   
        }
    }
}
