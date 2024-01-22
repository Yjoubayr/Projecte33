using System.Net.Sockets;
using System.Net;
using System.IO;
using System;
using System.Text;
using System.Reflection.PortableExecutable;
using iText.Kernel.Pdf;
using iText.Kernel.Exceptions;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using DAMSecurityLib.Crypto;
using SocketLibrary;
using System.Security.Cryptography.X509Certificates;
using iText.StyledXmlParser.Jsoup.Parser;
using System.Runtime.ConstrainedExecution;
using DAMSecurityLib.Crypto.Encryption;
namespace SocketProvaClient
{
    public class ClientSocket
    {
        static TcpListener listener;
        static int PORT = 999;
        static TcpClient ActualClient;
        static String PDFRebut = "../../../../PDFRebut.pdf";
        static String PDFDesencriptat = "../../../../PDFRebutYDesencriptat.pdf";
        static String ClauPublicaRuta = "../../../../PublicKeyFile";
        static String RutaCertificado = "../../../../certMarc.pfx";
        static String CertPass = "1234";
        public static void Main()
        {
            var ipEndPoint = new IPEndPoint(IPAddress.Any, PORT);
            listener = new TcpListener(ipEndPoint);
            listener.Start();
            Console.WriteLine("Socket Iniciado y escuchando...");
            ActualClient = listener.AcceptTcpClient();
            Console.Write("Introduce una lista para hacer la consulta: ");
            String response = Console.ReadLine();
            RSACrypt.SavePublicKey(RutaCertificado, CertPass, ClauPublicaRuta);
            RSA rsa = RSACrypt.LoadPublicKey(ClauPublicaRuta);
            // Exportar clave pública a una cadena
            byte[] publicKeyBytes = rsa.ExportRSAPublicKey();
            String publicKeyString = Convert.ToBase64String(publicKeyBytes);
            
            // Ahora puedes usar publicKeyString como una cadena en tu respuesta.
            response = response + "|" + publicKeyString;
            SenderMessage(response);
            NetworkStream networkStream = ActualClient.GetStream();
            byte[] claveAesEncriptada = ReceiveMessage(networkStream);
            Receiver(PDFRebut, networkStream, claveAesEncriptada);

        }
        /// <summary>
        /// funcio que rep un missatge
        /// </summary>
        /// <param name="networkStream"></param>
        /// <returns></returns>
        static public byte[] ReceiveMessage(NetworkStream networkStream)
        {
            try { 
            
                MemoryStream memoryStream = new MemoryStream();
                byte[] buffer = new byte[256]; // Puedes ajustar el tamaño del búfer según tus necesidades

                int bytesRead;
                networkStream.Read(buffer, 0, buffer.Length);               
            
                return buffer;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al recibir datos: {ex.Message}");
                throw;
            }
        }
        /// <summary>
        /// Funcio que envia la clau aes per socket
        /// </summary>
        /// <param name="rutaDestino"></param>
        /// <param name="networkStream"></param>
        /// <param name="claveAesEncriptada"></param>
        static public void Receiver(string rutaDestino, NetworkStream networkStream, byte[] claveAesEncriptada)
        {
            try
            {
               

                int bufferSize = 1024;
                byte[] buffer = new byte[bufferSize];

               
                using (FileStream fileStream = File.Create(rutaDestino))
                {
                    int bytesRead;

                    // Recibir el archivo en bloques
                    while ((bytesRead = networkStream.Read(buffer, 0, bufferSize)) > 0)
                    {
                        fileStream.Write(buffer, 0, bytesRead);
                    }
                }
                //Desencriptar Archivo
                Encryption.DecryptPDF(PDFRebut, PDFDesencriptat, claveAesEncriptada, RutaCertificado, CertPass);
                // Cerrar la conexión
                networkStream.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al procesar la respuesta del servidor: {ex.Message}");
            }
        }
        /// <summary>
        /// Funcio que envia un missatge per socket
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        static async Task SenderMessage(string message)
        {
            try
            {
                // Obtener la referencia al flujo de red del cliente
                NetworkStream stream = ActualClient.GetStream();
                var EncMessage = Encoding.UTF8.GetBytes(message);
                StreamWriter writer = new StreamWriter(stream);
                await writer.WriteLineAsync(message);
                await writer.FlushAsync();
                Console.WriteLine("Enviat: " + message);
            }
            catch (SocketException ex)
            {
                throw ex;
            }
        }
        
    }
}
