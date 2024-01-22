using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using DAMSecurityLib;
using DAMSecurityLib.Crypto;

namespace CrearPDFSignat
{
    public class ClassPDF
    {
        /// <summary>
        /// Classe que signa un pdf
        /// </summary>
        /// <param name="rutaOutputPDFSignado"></param>
        /// <param name="jsonList"></param>
        /// <param name="Certpass"></param>
        /// <param name="CertificateRoute"></param>
        public static void CrearPDFSignat(string rutaOutputPDFSignado, string jsonList, string Certpass,string CertificateRoute)
        {
            try
            {
                Sign SecurityLib = new Sign();
                String rutaPDF = "../../../../PDFAmbJson.pdf";
                SecurityLib.CreatePDFWithJsonList(rutaPDF, jsonList);
                SecurityLib.createPDFsignedMarc(certPath: CertificateRoute, certPass: Certpass, rutaOutputPDFSignado, rutaPDF);
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
    }
}
