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

namespace TaulerDeControlRM.MenuPages
{
    /// <summary>
    /// Interaction logic for PagePDF.xaml
    /// </summary>
    public partial class PagePDF : Page
    {
        private PdfDocument pdfDoc;
        private int currentPage = 1;

        public PagePDF()
        {
            InitializeComponent();
            Loaded += PagePDF_Loaded;
        }

        private void PagePDF_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            // Specify the path to the PDF file you want to read
            string filePath = "C:\\Users\\ASUS\\Downloads\\pdfExemple.pdf";

            // Open the PDF document
            pdfDoc = new PdfDocument(new PdfReader(filePath));

            // Display the text of the first page
            DisplayPageText(currentPage);
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
                }
            }

            private void btnNext_Click(object sender, EventArgs e)
            {
                // Navigate to the next page
                if (currentPage < pdfDoc.GetNumberOfPages())
                {
                    currentPage++;
                    DisplayPageText(currentPage);
                }
            }
        
    }
}
