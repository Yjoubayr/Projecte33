using CrearPDFSignat;
using SocketLibrary;
class Program
{
    static public void Main()
    {
        ProvaPDF();
        //CreateRSA.Main();
        String clauPublica = "../../../../clave_publica.pem";
        String PDFSignat = "../../../../PDFsignat.pdf";
       
    }
        static public void ProvaPDF()
    {
        string Provajson = "{\"nombre\": \"Juan\", \"edad\": 25, \"ciudad\": \"Barcelona\"}";
        string certRuta = "../../../../certMarc.pfx";
        string certPass = "1234";
        string outputRute = "../../../../PDFsignat.pdf";
        ClassPDF.CrearPDFSignat(outputRute, Provajson, certPass, certRuta);

    }
}