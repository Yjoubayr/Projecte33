using CrearPDFSignat;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using SocketProvaClient;

namespace Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        /// <summary>
        /// Test per comprovar que es signi i estigui amb el json.
        /// </summary>
        [Test]
        public void CrearPDFSignatAmbJson()
        {
            string Provajson = "{\"nombre\": \"Juan\", \"edad\": 25, \"ciudad\": \"Barcelona\"}";
            string certRuta = "../../../../certMarc.pfx";
            string certPass = "1234";
            string outputRute = "../../../../PDFsignat.pdf";
            ClassPDF.CrearPDFSignat(outputRute, Provajson, certPass, certRuta);
            if (File.Exists(outputRute))
            {
                Assert.Pass();
            } else { Assert.Fail(); } 
        }
        /// <summary>
        /// Test per comprovar que aixo funcioni
        /// </summary>
        [Test]
        public void CrearPDFenBlanc()
        {
            string outputRoute = "../../../../PDFEnBlanc.pdf";
            DAMSecurityLib.Crypto.Sign.CreateBlancPDF(outputRoute);
            Assert.Pass();
        }
        /// <summary>
        /// Test per craer un PDF en memoria 
        /// </summary>
        [Test]
        public void CrearPDFenMemoria()
        {
            byte[] bytes;

            bytes = DAMSecurityLib.Crypto.Sign.CreatePDFinMemory();
            
            if(bytes.Length > 0)
            {
                Assert.Pass();
            } else
            {
                Assert.Fail();
            }
        }
        /// <summary>
        /// Test per Comprobar que s'envi un missatge correctament
        /// </summary>
        [Test]
        public void EnviarMissatgeSocket()
        {
            try
            {
                ClientSocket.SenderMessage("hola");
               
            }catch (Exception ex)
            {
                Assert.Fail(ex.ToString());
            }
            Assert.Pass();
        }

    }
}