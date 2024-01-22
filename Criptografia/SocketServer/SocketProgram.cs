using iText.Kernel.Pdf;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Reflection.PortableExecutable;
using System.Security.Cryptography;
using System.Text;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using System.Text.RegularExpressions;
using CrearPDFSignat;
using System.Text.Json;
using System.Net.NetworkInformation;
using DAMSecurityLib.Crypto.Encryption;
using System.Security.Cryptography.X509Certificates;

namespace SocketServer
{
    public class SocketProgram
    {
        static int PORT = 999;
        static TcpClient ActualClient;
        static String PublicKeyString;
        static String ListaPedida;
        static String PDFEncriptado = "../../../../PDFCrypted.pdf";
        static String PDFSignat = "../../../../PDFsignat.pdf";
        static String RutaPublicKey = "../../../../PublicKeyFile.pem";
        static String RutaCertificado = "../../../../certMarc.pfx";
        static String CertPass = "1234";
        /// <summary>
        /// En aquest metodo s'haura de pasar el json
        /// </summary>
        /// <param name="json">objecte json per pasar y crear el document</param>
        static public void Main()
        {
            ActualClient = new TcpClient();
            var ipEndPoint = new IPEndPoint(IPAddress.Any, PORT);
             ActualClient.Connect("localhost", PORT);
            Console.WriteLine("Conexion correcta con el cliente");
            Listener();
            Console.WriteLine(ListaPedida, " ", PublicKeyString);
            //Fer consulta per generar pdf

            //Generar pdf
            var data = new
            {
                Nombre = "Ejemplo",
                Edad = 25,
                Ciudad = "Ciudad Ejemplo"
            };

            // Serializar el objeto a una cadena JSON
            string jsonString = JsonSerializer.Serialize(data);
            //ClassPDF.CrearPDFSignat(PDFSignat, jsonString, "1234", PDFSinEncriptar);
            //Encriptar pdf
            var AESKey = Encryption.EncryptPDF(PDFSignat, PDFEncriptado);
            //Retornar pdf amb consulta i encriptat amb clau publica
            Sender(PDFEncriptado, AESKey);
          
        }
        /// <summary>
        /// Funcio que envia l'archiu y la clau aes
        /// </summary>
        /// <param name="rutaArchivo"></param>
        /// <param name="aeskey"></param>
        static public void Sender(String rutaArchivo, byte[] aeskey)
        {
            try
            {
                
                // Obtener la referencia al flujo de red del cliente
                NetworkStream networkStream = ActualClient.GetStream();
                networkStream.Write(aeskey, 0, aeskey.Length);
                
                // Leer el archivo en bloques y enviarlo al cliente
                using (FileStream fileStream = File.OpenRead(rutaArchivo))
                {
                    int bufferSize = 1024;
                    byte[] buffer = new byte[bufferSize];
                    int bytesRead;

                    // Enviar el archivo al cliente en bloques
                    while ((bytesRead = fileStream.Read(buffer, 0, bufferSize)) > 0)
                    {
                        networkStream.Write(buffer, 0, bytesRead);
                    }
                }

                Console.WriteLine($"Archivo {rutaArchivo} encriptado enviado exitosamente.");
                
               
                
                // Cerrar la conexión
                networkStream.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al procesar cliente: {ex.Message}");
            }
        }
        /// <summary>
        /// Listener que escolta les petcions del client
        /// </summary>
        static public void Listener()
        {
            
                try
                {
                    // Obtener la referencia al flujo de red del cliente
                    NetworkStream networkStream = ActualClient.GetStream();

                    // Inicializar el tamaño del búfer
                    const int bufferSize = 1_024;
                    byte[] buffer = new byte[bufferSize];
                    int bytesRead;
                    bytesRead = networkStream.Read(buffer, 0, bufferSize);
                    if (bytesRead > 0)
                    {
                        string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                        string[] messageSplited = message.Split("|");
                        PublicKeyString = messageSplited[1];
                        ListaPedida = messageSplited[0];
                        Console.Write("Clave Recibida! La clave publica es: " + PublicKeyString);
                        Console.Write("La lista que se ha pedido es: " + ListaPedida);
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error en el listener del servidor: {ex.Message}");
                }
            
        }
       
    }

}
