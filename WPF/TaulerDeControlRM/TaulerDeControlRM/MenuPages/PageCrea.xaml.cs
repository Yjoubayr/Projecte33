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
            elementComboBox.ItemsSource = new List<string> { "Cançó", "Músic", "Grup", "Àlbum", "Llista de reproducció" };
        }
        public void ChangeMenu(object sender, SelectionChangedEventArgs e)
        { 
            string selectedcmbItem = elementComboBox.SelectedItem.ToString();

            // Determine which item was selected and update the main content accordingly
            if (selectedcmbItem != null)
            {
                switch (selectedcmbItem.ToString())
                {
                    case "Cançó":
                        frameCrear.Navigate(new PageCreaCanco());
                        break;
                    //case "Músic":
                    //    frameCrear.Navigate(new PageCreaMusic());
        
                        // Add more cases as needed
                }
                //mainFrame.NavigationService.Navigate(selectedPage);
            }
    }
    }
}
