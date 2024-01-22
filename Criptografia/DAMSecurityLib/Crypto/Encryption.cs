using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Tls;
using System.Security.Cryptography.X509Certificates;
using Org.BouncyCastle.Asn1.IsisMtt.Ocsp;

namespace DAMSecurityLib.Crypto.Encryption
{
    public class Encryption
    {
        static public string certRute = "../../../../certMarc.pfx";
        static public string certPass = "1234";
        static public string PublicKeyRoute = "../../../../PublicKeyFile";
        /// <summary>
        /// Funcio que encrypta la clau y el pdf
        /// </summary>
        /// <param name="rutaPDF"></param>
        /// <param name="PDFEncriptado"></param>
        /// <returns></returns>
        public static byte[] EncryptPDF(string rutaPDF, string PDFEncriptado)
        {
            try
            {
                // Obtener la clave pública RSA desde el certificado
                var cert = new X509Certificate2(certRute, certPass);
                var pk = RSACrypt.LoadPublicKey(PublicKeyRoute).ExportParameters(false);
                // Encriptar la clave AES con la clave pública RSA
                X509Certificate2 certificado = new X509Certificate2(certRute, certPass);
                // Leer el contenido del archivo PDF
                byte[] contenidoPDF = File.ReadAllBytes(rutaPDF);
                byte[] claveAesEncriptada;
                // Encriptar el contenido del PDF con AES
                using (FileStream fsInput = new FileStream(rutaPDF, FileMode.Open))
                {
                    using (FileStream fsOutput = new FileStream(PDFEncriptado, FileMode.Create))
                    {
                        using (AesManaged aes = new AesManaged())
                        {
                            aes.GenerateKey();
                            byte[] aesIV = new byte[16];
                            aes.IV = aesIV;
                            claveAesEncriptada = RSACrypt.EncryptAESKey(aes.Key, pk);
                            // Perform encryption
                            ICryptoTransform encryptor = aes.CreateEncryptor();
                            using (CryptoStream cs = new CryptoStream(fsOutput, encryptor, CryptoStreamMode.Write))
                            {
                                fsInput.CopyTo(cs);
                            }
                        }
                    }
                }

                Console.WriteLine("Documento encriptado con éxito.");

                // Devolver las claves generadas
                return claveAesEncriptada;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al encriptar el PDF: {ex.Message}");
                throw;
            }
        }
        /// <summary>
        /// Funcio que desencripta la clau y el pdf
        /// </summary>
        /// <param name="rutaPDF"></param>
        /// <param name="PDFDesencriptado"></param>
        /// <param name="claveAesEncriptada"></param>
        /// <param name="certRuta"></param>
        /// <param name="certPass"></param>
        public static void DecryptPDF(string rutaPDF, string PDFDesencriptado, byte[] claveAesEncriptada, string certRuta, string certPass)
        {
            try
            {
                X509Certificate2 certificado = new X509Certificate2(certRuta, certPass);
                byte[] claveAesDesencriptada = RSACrypt.DecryptAESKeyWithPrivateKey(claveAesEncriptada, certificado);
                byte[] contenidoEncriptadoAES = File.ReadAllBytes(rutaPDF);

                using (FileStream fsInput = new FileStream(rutaPDF, FileMode.Open))
                {
                    using (FileStream fsOutput = new FileStream(PDFDesencriptado, FileMode.Create))
                    {
                        using (AesManaged aes = new AesManaged())
                        {
                            
                            aes.Key = claveAesDesencriptada;
                            byte[] aesIV = new byte[16];
                            aes.IV = aesIV;


                            ICryptoTransform decryptor = aes.CreateDecryptor();
                            using (CryptoStream cs = new CryptoStream(fsOutput, decryptor, CryptoStreamMode.Write))
                            {
                                fsInput.CopyTo(cs);
                            }
                        }
                    }
                }

                Console.WriteLine("Documento desencriptado con éxito.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al desencriptar el PDF: {ex.Message}");
                throw;
            }
        }
    }
}
