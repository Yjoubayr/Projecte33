using CrearPDFSignat;
class Program
{
    static public void Main()
    {
        string Provajson = "{\"nombre\": \"Juan\", \"edad\": 25, \"ciudad\": \"Barcelona\"}";
        string certRuta = "../../../../certMarc.pfx";
        string certPass = "1234";
        string outputRute = "../../../../PDFsignat.pdf";
        ClassPDF.CrearPDFSignat(outputRute , Provajson, certPass, certRuta);
        

    }
}