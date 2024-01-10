using DAMSecurityLib.Certificates;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Pkcs;
using Org.BouncyCastle.Pkcs;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.X509;
using iText.Kernel.Pdf;
using iText.Signatures;
using iText.Bouncycastle.Crypto;
using iText.Bouncycastle.X509;
using iText.Commons.Bouncycastle.Cert;
using Org.BouncyCastle.Crypto;
using System.Security.Cryptography;
using System.Text;
using iText.Kernel.Geom;
using iText.Forms.Fields.Properties;
using iText.Forms.Form.Element;
using System.Runtime.CompilerServices;
using iText.IO.Source;
using DAMSecurityLib.Crypto;
using iText.Kernel.Pdf;
using Org.BouncyCastle.Asn1;
using System.Text;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.IO.Source;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.Asn1.IsisMtt.Ocsp;
using Org.BouncyCastle.Pkix;

namespace DAMSecurityLib.Crypto
{
    /// <summary>
    /// This class is used to sign documents 
    /// </summary>
    public class Sign
    {

        #region Private attributes

        private X509Certificate2? certificate;
        private Certificates.CertificateInfo? certificateInfo;
        private Pkcs12Store pkcs12Store = new Pkcs12StoreBuilder().Build();
        private string storeAlias = "";

        #endregion

        /// <summary>
        /// Init class certificate attributes with the disk certificate
        /// </summary>
        /// <param name="pfxFileName">Certificate file disk path</param>
        /// <param name="pfxPassword">Certificate password</param>
        public void InitCertificate(string pfxFileName, string pfxPassword)
        {
            certificate = new X509Certificate2(pfxFileName, pfxPassword);

            pkcs12Store.Load(new FileStream(pfxFileName, FileMode.Open, FileAccess.Read), pfxPassword.ToCharArray());
            foreach (string currentAlias in pkcs12Store.Aliases)
            {
                if (pkcs12Store.IsKeyEntry(currentAlias))
                {
                    storeAlias = currentAlias;
                    break;
                }
            }
            certificateInfo = Certificates.CertificateInfo.FromCertificate(pfxFileName,pfxPassword);
        }

        /// <summary>
        /// Sign pdf document and save result to disk.
        /// This method puts digital signature inside pdf document
        /// </summary>
        /// <param name="inputFileName">Input pdf file path to sign</param>
        /// <param name="outputFileName">Ouput pdf file path to save the result file</param>
        /// <param name="showSignature">If signatature is visible in pdf document</param>
        public void SignPdf(string inputFileName, string outputFileName, bool showSignature)
        {
            AsymmetricKeyParameter key = pkcs12Store.GetKey(storeAlias).Key;

            X509CertificateEntry[] chainEntries = pkcs12Store.GetCertificateChain(storeAlias);
            IX509Certificate[] chain = new IX509Certificate[chainEntries.Length];
            for (int i = 0; i < chainEntries.Length; i++)
                chain[i] = new X509CertificateBC(chainEntries[i].Certificate);
            PrivateKeySignature signature = new PrivateKeySignature(new PrivateKeyBC(key), "SHA256");

            using (PdfReader pdfReader = new PdfReader(inputFileName))
            using (FileStream result = File.Create(outputFileName))
            {
                PdfSigner pdfSigner = new PdfSigner(pdfReader, result, new StampingProperties().UseAppendMode());

                if (showSignature)
                {
                    CreateSignatureApperanceField(pdfSigner);
                }

                pdfSigner.SignDetached(signature, chain, null, null, null, 0, PdfSigner.CryptoStandard.CMS);
            }
        }

        /// <summary>
        /// Sign filedisk file with the global class certificate
        /// </summary>
        /// <param name="inputFileName">Filedisk input file path to sign</param>
        /// <param name="outputFileName">Filedisk output file path to save the result</param>
        public void SignFile(string inputFileName, string outputFileName)
        {
            if (certificate != null)
            {
                byte[] inputBytes = File.ReadAllBytes(inputFileName);
                byte[] outputBytes = SignDocument(certificate, inputBytes);

                File.WriteAllBytes(outputFileName, outputBytes);
            }
        }

        /// <summary>
        /// Returns SHA-256 HASH from input byte array
        /// </summary>
        /// <param name="input">Input byte array to obtain SHA-256 HASH</param>
        /// <returns>SHA-256 HASH</returns>
        public string SHA256Hash(string input)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = sha256.ComputeHash(inputBytes);

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    builder.Append(hashBytes[i].ToString("x2"));
                }

                return builder.ToString();
            }
        }
        /// <summary>
        /// Sign byte array document with the certificate
        /// </summary>
        /// <param name="certificate">Certificated used to sign the document</param>
        /// <param name="document">Document byte array to sign</param>
        /// <returns>Byte array with the signed document</returns>
        internal static byte[] SignDocument(X509Certificate2 certificate, byte[] document)
        {
            ContentInfo contentInfo = new ContentInfo(document);
            SignedCms signedCms = new SignedCms(contentInfo, false);
            CmsSigner signer = new CmsSigner(SubjectIdentifierType.Unknown, certificate);
            signedCms.ComputeSignature(signer);

            return signedCms.Encode();
        }

        /// <summary>
        /// Adds signature field rectangle inside pdf document
        /// </summary>
        /// <param name="pdfSigner">PdfSigner used to sign document</param>
        internal void CreateSignatureApperanceField(PdfSigner pdfSigner)
        {
            var pdfDocument = pdfSigner.GetDocument();
            var pageRect = pdfDocument.GetPage(1).GetPageSize();
            var size = new PageSize(pageRect);
            pdfDocument.AddNewPage(size);
            var totalPages = pdfDocument.GetNumberOfPages();
            float yPos = pdfDocument.GetPage(totalPages).GetPageSize().GetHeight() - 100;
            float xPos = 0;
            Rectangle rect = new Rectangle(xPos, yPos, 200, 100);

            pdfSigner.SetFieldName("signature");

            SignatureFieldAppearance appearance = new SignatureFieldAppearance(pdfSigner.GetFieldName())
                    .SetContent(new SignedAppearanceText()
                        .SetSignedBy(certificateInfo?.Organization)
                        .SetReasonLine("" + " - " + "")
                        .SetLocationLine("Location: " + certificateInfo?.Locality)
                        .SetSignDate(pdfSigner.GetSignDate()));

            pdfSigner.SetPageNumber(totalPages).SetPageRect(rect)
                    .SetSignatureAppearance(appearance);

        }
        /// <summary>
        /// A function to create a blanc PDF
        /// </summary>
        /// <param name="rutaPdf">param of the file path</param>
        public void CreateBlancPDF(String rutaPdf)
        {
            try
            {
                using (PdfWriter writer = new PdfWriter(rutaPdf))
                {
                    using (PdfDocument pdfDocument = new PdfDocument(writer))
                    {
                        using (Document document = new Document(pdfDocument))
                        {
                            Console.WriteLine("PDF en blanco creado correctamente.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al crear el PDF en blanco: " + ex.Message);
            }

        }
        private static byte[] CreatePDFinMemory()
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (PdfWriter writer = new PdfWriter(memoryStream))
                {
                    using (PdfDocument pdfDocument = new PdfDocument(writer))
                    {
                        using (Document document = new Document(pdfDocument))
                        {
                            // CONTENT OF THE DOCUMENT
                        }
                    }
                }

                return memoryStream.ToArray();
            }
        }
        public void createBlancPDFsignedMarc(String certPath,String certPass)
        {
            using (X509Certificate2 cert = new X509Certificate2(certPath, certPass))
            {
                byte[] bytes = CreatePDFinMemory();
                InitCertificate(certPath, certPass);
                byte[] outputBytes = SignDocument(cert, bytes);
                File.WriteAllBytes("C:\\Users\\marcg\\Documents\\Programacio\\C.G.S_DAM2\\CsharpEncriptacio\\DAMSecurity\\signedpdf.pdf", outputBytes);
            }
        }
        public void ValidateSignature(string pdfFileName)
        {
            using (PdfReader pdfReader = new PdfReader(pdfFileName))
            {
                using (PdfDocument pdfDocument = new PdfDocument(pdfReader))
                {
                    SignatureUtil signatureUtil = new SignatureUtil(pdfDocument);
                    var signatureNames = signatureUtil.GetSignatureNames();
                    foreach (var signatureName in signatureNames)
                    {
                        var signatureDictionary = signatureUtil.ReadSignatureData(signatureName);
                        var certificates = signatureDictionary.GetCertificates();
                        foreach (var certificate in certificates)
                        {
                            certificate.CheckValidity(certificate.GetNotBefore());
                        }
                    }
                }
            }
        }
        public void pdfCreateTables()
        {
            string pdfFilePath = "C:\\Users\\marcg\\Documents\\Programacio\\C.G.S_DAM2\\CsharpEncriptacio\\DAMSecurity\\Taules.pdf";

            using (var writer = new PdfWriter(pdfFilePath))
            {
                using (var pdf = new PdfDocument(writer))
                {
                    var document = new Document(pdf);

                    // Capçalera de pàgina
                    document.Add(new Paragraph("Capçalera de la pàgina"));

                    // Crear taula amb 3 columnes
                    Table table = new Table(3);

                    // Afegir capçaleres de columna
                    table.AddHeaderCell("Capçalera 1");
                    table.AddHeaderCell("Capçalera 2");
                    table.AddHeaderCell("Capçalera 3");

                    // Afegir dades a la taula
                    for (int i = 0; i < 10; i++)
                    {
                        table.AddCell($"Dada {i + 1},1");
                        table.AddCell($"Dada {i + 1},2");
                        table.AddCell($"Dada {i + 1},3");
                    }

                    // Afegir la taula al document
                    document.Add(table);

                    // Peu de pàgina
                    document.Add(new Paragraph("Peu de pàgina"));
                }
            }

            Console.WriteLine($"PDF creat a: {pdfFilePath}");
        }
        public void EncriptacionPDFAes(string rutaPDF, string clave)
        {
            using (AesManaged aesAlg = new AesManaged())
            {
                // Ajustar la longitud de la clave a 256 bits
                aesAlg.Key = SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(clave));
                aesAlg.IV = new byte[aesAlg.BlockSize / 8];
                string outputPath = "C:\\Users\\marcg\\Documents\\Programacio\\C.G.S_DAM2\\CsharpEncriptacio\\DAMSecurity\\pdfEncriptadoAEScbc.pdf";

                using (FileStream fsEntrada = new FileStream(rutaPDF, FileMode.Open, FileAccess.Read))
                using (FileStream fsSalida = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                using (ICryptoTransform encryptor = aesAlg.CreateEncryptor())
                using (CryptoStream cryptoStream = new CryptoStream(fsSalida, encryptor, CryptoStreamMode.Write))
                {
                    int bytesRead;
                    byte[] buffer = new byte[4096];

                    while ((bytesRead = fsEntrada.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        cryptoStream.Write(buffer, 0, bytesRead);
                    }
                }
            }
        }
        public void AddPasswordToPdf(string inputPdfPath, string outputPdfPath, string userPassword)
        {
            using (PdfReader pdfReader = new PdfReader(inputPdfPath))
            {
                using (PdfWriter pdfWriter = new PdfWriter(outputPdfPath, new WriterProperties().SetStandardEncryption(
                    Encoding.Default.GetBytes(userPassword),
                    Encoding.Default.GetBytes(""),
                    EncryptionConstants.ALLOW_PRINTING,
                    EncryptionConstants.ENCRYPTION_AES_256)))
                {
                }
            }
        }

    }
}
