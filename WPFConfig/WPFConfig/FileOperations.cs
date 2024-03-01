using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.IO;

namespace WPFConfig
{
    public class FileOperations
    {
        // Copiar el contenido de un archivo a otro
        public void CopyFileContent(string sourceFilePath, string destinationFilePath)
        {
            try
            {
                // Leer el contenido del archivo de origen
                string fileContent = File.ReadAllText(sourceFilePath);

                // Sobreescribir el contenido en el archivo de destino (esto borrará el contenido existente)
                File.WriteAllText(destinationFilePath, fileContent);

                Console.WriteLine("El contenido del archivo se ha copiado correctamente.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al copiar el contenido del archivo: {ex.Message}");
            }
        }
    }

    
}
