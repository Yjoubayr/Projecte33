using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows;
using System.IO;
using System.Diagnostics;


namespace WPFConfig
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }


        private void instalarLocalbtn_Click(object sender, RoutedEventArgs e)
        {
            // Lógica para instalar la base de datos seleccionada localmente
            string selectedDatabase = (databaseListBox.SelectedItem as ListBoxItem)?.Content.ToString();

            if (selectedDatabase == null)
            {
                MessageBox.Show("Por favor seleccione una base de datos");
                return;
            }
            // C:\Users\yosse\Dropbox\PC\Desktop\PROGRAMACIO\PROGRAMES\2nDAM\PROJECTEv3\Projecte33\WPFConfig\WPFConfig\composes\mssql-compose.yaml
            // 
            string baseDirectory = @"..\..\..\composes";

            string composeFolder = System.IO.Path.GetFullPath(baseDirectory);
            string composeFile = "";

            MessageBox.Show(composeFile);

            if (selectedDatabase == "PostgreSQL")
            {
                composeFile = System.IO.Path.Combine(composeFolder, "postgres-compose.yaml");
                // Comprueba si el archivo de configuración existe
                if (!File.Exists(composeFile))
                {
                    MessageBox.Show("El archivo de configuración no existe");
                    return;
                }
                else
                {
                    MessageBox.Show($"Instalando {selectedDatabase} en modo local...");
                    executeInShell("docker-compose", $"-f {composeFile} up -d");
                    // install dependecias de postgres

                }
            }
            else if (selectedDatabase == "MySQL")
            {
                composeFile = System.IO.Path.Combine(composeFolder, "mysql-compose.yaml");
                // Comprueba si el archivo de configuración existe
                if (!File.Exists(composeFile))
                {
                    MessageBox.Show("El archivo de configuración no existe");
                    return;
                }
                else
                {
                    MessageBox.Show($"Instalando {selectedDatabase} en modo local...");
                    executeInShell("docker-compose", $"-f {composeFile} up -d");

                }
            }
            else if (selectedDatabase == "MicrosoftSQLServer")
            {
                composeFile = System.IO.Path.Combine(composeFolder, "mssql-compose.yaml");
                // Comprueba si el archivo de configuración existe
                if (!File.Exists(composeFile))
                {
                    MessageBox.Show("El archivo de configuración no existe");
                    return;
                }
                else
                {
                    MessageBox.Show($"Instalando {selectedDatabase} en modo local...");
                    executeInShell("docker-compose", $"-f {composeFile} up -d");

                }
            }
            else
            {
                MessageBox.Show("Base de datos seleccionada no compatible");
                return;
            }

            
        }


    private void executeInShell(string command, string arguments)
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


    private void conectarBdbtn_Click(object sender, RoutedEventArgs e)
        {
        // Lógica para conectarse a la base de datos remota
        string ipAddress = ipTextBox.Text;
        string port = portTextBox.Text;

        MessageBox.Show($"Conectando en {ipAddress}:{port}...");
        }
    }
}
