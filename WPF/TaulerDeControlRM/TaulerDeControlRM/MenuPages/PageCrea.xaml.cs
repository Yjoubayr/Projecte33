using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
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
            elementComboBox.ItemsSource = new List<string> { "Àlbum", "Cançó", "Grup", "Músic" };
            
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
                        frameCrear.Navigate(new PageCreaAlbum());
                        break;
                    case "Cançó":
                        frameCrear.Navigate(new PageCreaCanco());
                        break;
                    case "Grup":
                        frameCrear.Navigate(new PageCreaGrup());
                        break;
                    case "Músic":
                        frameCrear.Navigate(new PageCreaMusic());
                        break;
                        // Add more cases as needed
                }
            }
        }

    }
}
