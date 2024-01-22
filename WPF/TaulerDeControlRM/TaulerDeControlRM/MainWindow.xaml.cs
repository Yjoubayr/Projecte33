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
            //menuPujarCanco secondWindow = new menuPujarCanco();

            // Show the SecondWindow
            //secondWindow.ShowDialog();
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
                        mainFrame.Navigate(new PageEdita()); // Replace Page2 with your actual user control
                        break;
                    case "Historial":
                        mainFrame.Navigate(new PageHistorial()); // Replace Page3 with your actual user control
                        break;
                    case "Llistats":
                        mainFrame.Navigate(new PageLlistes()); // Replace Page4 with your actual user control
                        break;
                        // Add more cases as needed
                }
                //mainFrame.NavigationService.Navigate(selectedPage);
            }
        }
    }
}