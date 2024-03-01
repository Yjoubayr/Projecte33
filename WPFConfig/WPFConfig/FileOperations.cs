using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.IO;
using System.Windows;

namespace WPFConfig
{
    /// <summary>
    /// Esta clase contiene operaciones para trabajar con archivos
    /// </summary>
    public class FileOperations
    {
        /// <summary>
        /// Copia el contenido de un archivo a otro
        /// </summary>
        /// <param name="sourceFilePath"></param>
        /// <param name="destinationFilePath"></param>
        public void CopyFileContent(string sourceFilePath, string destinationFilePath)
        {
            MessageBox.Show("Copiando el contenido del archivo: " + sourceFilePath);
            MessageBox.Show("Al archivo: " + destinationFilePath);

            try
            {
                // Leer el contenido del archivo de origen
                string fileContent = File.ReadAllText(sourceFilePath);

                // Sobreescribir el contenido en el archivo de destino
                File.WriteAllText(destinationFilePath, fileContent);

                MessageBox.Show("El contenido del archivo se ha copiado correctamente");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al copiar el contenido del archivo: {ex.Message}");
            }
        }
    }

    
}
