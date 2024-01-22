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
    /// Interaction logic for PageCrea.xaml
    /// </summary>
    public partial class PageCrea : Page
    {
        public PageCrea()
        {
            InitializeComponent();
            elementComboBox.ItemsSource = new List<string> { "Cançó", "Artista", "Grup", "Àlbum", "Llista de reproducció" };
            
            // Get the selected item from the ListBox
            ListBoxItem selectedListBoxItem = (ListBoxItem)elementComboBox.SelectedItem;

            // Determine which item was selected and update the main content accordingly
            if (selectedListBoxItem != null)
            {
                switch (selectedListBoxItem.Content.ToString())
                {
                    case "Canco":
                        frameCrear.Navigate(new PageCreaCanco());
                        break;
                    case "Artista":
                        frameCrear.Navigate(new PageEdita()); // Replace Page2 with your actual user control
                        break;
                    case "Historial":
                        frameCrear.Navigate(new PageHistorial()); // Replace Page3 with your actual user control
                        break;
                    case "Llistats":
                        frameCrear.Navigate(new PageLlistes()); // Replace Page4 with your actual user control
                        break;
                        // Add more cases as needed
                }
            }
        }
    }
}
