using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TaulerDeControlRM.MenuPages;

namespace TaulerDeControlRM
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void OnSidebarItemSelected(object sender, SelectionChangedEventArgs e)
        {
            // Get the selected item from the ListBox
            ListBoxItem selectedListBoxItem = (ListBoxItem)sidebarList.SelectedItem;

            // Determine which item was selected and update the main content accordingly
            if (selectedListBoxItem != null)
            {
                Page selectedPage = null;
                switch (selectedListBoxItem.Content.ToString())
                {
                    case "Nou":
                        mainFrame.Navigate(new PageCrea()); 
                        break;
                    case "Edita":
                        mainFrame.Navigate(new PageEdita());
                        break;
                    case "Historial":
                        mainFrame.Navigate(new PageHistorial());
                        break;
                    case "Llistats":
                        mainFrame.Navigate(new PageLlistes()); 
                        break;
                    case "Pdf Viewer":
                        mainFrame.Navigate(new PagePDF()); 
                        break;
                    case "Pdf Viewer 2":
                        mainFrame.Navigate(new PagePDF2());
                        break;
                }
                //mainFrame.NavigationService.Navigate(selectedPage);
            }
        }

        private void Config_Click(object sender, RoutedEventArgs e)
        {
            // show dialog configuration
            ConfiguracioWindow config = new ConfiguracioWindow();
            config.ShowDialog();

        }
    }
}