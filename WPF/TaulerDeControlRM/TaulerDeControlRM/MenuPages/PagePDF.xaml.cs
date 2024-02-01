using iText.Kernel.Pdf.Canvas.Parser.Listener;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf;
using System;
using System.Collections.Generic;
using System.IO;
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
using Microsoft.Win32;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TaulerDeControlRM.MenuPages
{
    /// <summary>
    /// Interaction logic for PagePDF.xaml
    /// </summary>
    public partial class PagePDF : Page
    {
        private PdfDocument pdfDoc;
        private int currentPage = 1;
        private string _selectedPDFPath;

        public string SelectedPDFPath
        {
            get { return _selectedPDFPath; }
            set
            {
                _selectedPDFPath = value;
                OnPropertyChanged();
            }
        }

        public PagePDF()
        {
            InitializeComponent();
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            // Open the PDF document
            pdfDoc = new PdfDocument(new PdfReader(SelectedPDFPath));
            path.Text = SelectedPDFPath;

            // Display the text of the first page
            DisplayPageText(currentPage);
            txtBPageNumber.Text = currentPage.ToString();
        }


        private void DisplayPageText(int pageNum)
            {
                // Get the text extraction strategy
                ITextExtractionStrategy strategy = new SimpleTextExtractionStrategy();

                // Extract text from the specified page
                string pageText = PdfTextExtractor.GetTextFromPage(pdfDoc.GetPage(pageNum), strategy);

                // Display the extracted text in the TextBox
                txtBoxPdf.Text = pageText;
            }

            private void btnPrevious_Click(object sender, EventArgs e)
            {
                // Navigate to the previous page
                if (currentPage > 1)
                {
                    currentPage--;
                    DisplayPageText(currentPage);
                    txtBPageNumber.Text = currentPage.ToString();
                }
            }

            private void btnNext_Click(object sender, EventArgs e)
            {
                // Navigate to the next page
                if (currentPage < pdfDoc.GetNumberOfPages())
                {
                    currentPage++;
                    DisplayPageText(currentPage);
                    txtBPageNumber.Text = currentPage.ToString();
                }
            }

        private void BrowseButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "PDF Files (*.pdf)|*.pdf";
            if (openFileDialog.ShowDialog() == true)
            {
                SelectedPDFPath = openFileDialog.FileName;
            }
        }
    }
}
