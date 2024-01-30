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

namespace TaulerDeControlRM
{
    /// <summary>
    /// Interaction logic for WindowBasica.xaml
    /// </summary>
    public partial class WindowBasica : Window
    {
        public WindowBasica()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Cançó pujada correctament");
            string[] pdfPages = { "C:/Users/ASUS/Desktop/asdfgh.pdf" }; // Array of PDF page paths
            var pdfViewerWindow = new PdfViewerWindow(pdfPages);
            pdfViewerWindow.ShowDialog();
        }
    }
}
