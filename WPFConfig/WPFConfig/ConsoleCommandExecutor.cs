using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WPFConfig
{
    public class ConsoleCommandExecutor
    {
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
                process.Start();

                // Leer la salida del proceso (opcional)
                string output = process.StandardOutput.ReadToEnd();
                Console.WriteLine(output);

                process.WaitForExit();
                process.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al ejecutar 'dotnet run': {ex.Message}");
            }
        }
    }
}
