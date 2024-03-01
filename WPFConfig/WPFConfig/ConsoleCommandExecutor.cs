using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WPFConfig
{

    /// <summary>
    /// Ejecuta comandos en la consola de Windows
    /// </summary>
    public class ConsoleCommandExecutor
    {

        /// <summary>
        /// Ejecuta un comando en la consola de Windows
        /// </summary>
        /// <param name="command"></param>
        /// <param name="arguments"></param>
        public void executeInShell(string command, string arguments)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = command,
                Arguments = arguments,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            Process process = new Process
            {
                StartInfo = startInfo
            };

            process.OutputDataReceived += (sender, e) =>
            {
                // Muestra la salida en tiempo real en un MessageBox
                MessageBox.Show(e.Data);
            };

            process.Start();
            MessageBox.Show("Proceso iniciado");
            process.WaitForExit();
            MessageBox.Show("Proceso finalizado");

            string output = process.StandardOutput.ReadToEnd();
            string error = process.StandardError.ReadToEnd();

            if (!string.IsNullOrEmpty(output))
            {
                MessageBox.Show($"Proceso finalizado con Output: {output}");
            }

            if (!string.IsNullOrEmpty(error))
            {
                MessageBox.Show($"Proceso finalizado con Error: {error}");
            }
        }


        /// <summary>
        /// Ejecuta el comando "dotnet run" en el directorio de trabajo especificado
        /// </summary>
        /// <param name="workingDirectory"></param>
        public void ExecuteDotnetRun(string workingDirectory)
        {
            try
            {
                // Crear un proceso para ejecutar el comando "dotnet run"
                Process process = new Process();
                process.StartInfo.FileName = "dotnet";
                process.StartInfo.Arguments = "run";
                process.StartInfo.WorkingDirectory = workingDirectory; // Establecer el directorio de trabajo
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.CreateNoWindow = true;

                process.Start();
                
                process.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al ejecutar 'dotnet run': {ex.Message}");
            }
        }
    }
}
