// PdfViewerWindow.xaml.cs
using System;
using System.Windows;

namespace TaulerDeControlRM
{
    public partial class PdfViewerWindow : Window
    {
        private int currentPageIndex = 0;
        private string[] pages; // array to store PDF pages path or content

        public PdfViewerWindow(string[] pdfPages)
        {
            InitializeComponent();
            pages = pdfPages;
            DisplayCurrentPage();
        }

        private void DisplayCurrentPage()
        {
            try
            {
                if (currentPageIndex >= 0 && currentPageIndex < pages.Length)
                {
                    string page = pages[currentPageIndex];
                    Uri uri = new Uri(page, UriKind.Absolute); // Specify UriKind.Absolute
                    pdfFrame.Navigate(uri);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load PDF file: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
