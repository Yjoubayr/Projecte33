using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SocketLibrary
{
    public class CreateRSA
    {
        public static void Main()
        {
            // Rutas de los archivos de claves
            string rutaArchivoClavePublica = "../../../../clave_publica.pem";
            string rutaArchivoClavePrivada = "../../../../clave_privada.pem";

            // Generar y guardar el par de claves RSA
            GenerarYGuardarClavesRSA(rutaArchivoClavePublica, rutaArchivoClavePrivada);

            Console.WriteLine("Par de claves RSA generado y guardado.");
        }

        static void GenerarYGuardarClavesRSA(string rutaArchivoClavePublica, string rutaArchivoClavePrivada)
        {
            try
            {
                // Crear un par de claves RSA
                using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(2048))
                {
                    // Exportar la clave pública a formato PEM y guardarla en un archivo
                    string clavePublicaPEM = ExportarClavePublicaPEM(rsa);
                    System.IO.File.WriteAllText(rutaArchivoClavePublica, clavePublicaPEM);

                    // Exportar la clave privada a formato PEM y guardarla en un archivo
                    string clavePrivadaPEM = ExportarClavePrivadaPEM(rsa);
                    System.IO.File.WriteAllText(rutaArchivoClavePrivada, clavePrivadaPEM);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al generar y guardar el par de claves RSA: {ex.Message}");
            }
        }

        static string ExportarClavePublicaPEM(RSACryptoServiceProvider rsa)
        {
            var parameters = rsa.ExportParameters(false);

            string pemString = $"{Convert.ToBase64String(parameters.Modulus)}";

            return pemString;
        }

        static string ExportarClavePrivadaPEM(RSACryptoServiceProvider rsa)
        {
            var parameters = rsa.ExportParameters(true);

            string pemString = $"{Convert.ToBase64String(parameters.Modulus)}";

            return pemString;
        }

        
        
    }
}
