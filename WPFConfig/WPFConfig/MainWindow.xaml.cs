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
        // Instancia de las clases necesarias
        FileOperations fileOperations = new FileOperations();
        ConsoleCommandExecutor executor = new ConsoleCommandExecutor();

        /// <summary>
        /// Este es el constructor de la clase MainWindow
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Este método se ejecuta cuando el usuario hace clic en el botón "Instalar base de datos localmente"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void instalarLocalbtn_Click(object sender, RoutedEventArgs e)
        {
            // Lógica para instalar la base de datos seleccionada localmente
            string selectedDatabase = (databaseListBox.SelectedItem as ListBoxItem)?.Content.ToString();

            if (selectedDatabase == null)
            {
                MessageBox.Show("Por favor seleccione una base de datos");
                return;
            }
            
            string baseDirectory = @"..\..\..\composes";

            string composeFolder = System.IO.Path.GetFullPath(baseDirectory);
            string composeFile = "";


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
                    executor.executeInShell("docker-compose", $"-f {composeFile} up -d");

                    // file to be copied
                    string sourceFilePath = System.IO.Path.GetFullPath(@"..\..\..\programs\programPostgres.skip");

                    // file where the content will be written
                    string destinationFilePath = System.IO.Path.GetFullPath(@"..\..\..\..\API_SQL\Program.cs");
                    fileOperations.CopyFileContent(sourceFilePath, destinationFilePath);

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
                    executor.executeInShell("docker-compose", $"-f {composeFile} up -d");

                    // file to be copied
                    string sourceFilePath = System.IO.Path.GetFullPath(@"..\..\..\programs\programMYSQL.skip");
                    // file where the content will be written
                    string destinationFilePath = System.IO.Path.GetFullPath(@"..\..\..\..\API_SQL\Program.cs");
                    fileOperations.CopyFileContent(sourceFilePath, destinationFilePath);

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
                    executor.executeInShell("docker-compose", $"-f {composeFile} up -d");

                    // file to be copied
                    string sourceFilePath = System.IO.Path.GetFullPath(@"..\..\..\programs\programMSSQL.skip");
                    // file where the content will be written
                    string destinationFilePath = System.IO.Path.GetFullPath(@"..\..\..\..\API_SQL\Program.cs");
                    fileOperations.CopyFileContent(sourceFilePath, destinationFilePath);

                }
            }
            else
            {
                MessageBox.Show("Base de datos seleccionada no compatible");
                return;
            }

            // la ruta real del directorio de trabajo
            string workingDirectory = System.IO.Path.GetFullPath(@"..\..\..\..\API_SQL");
            MessageBox.Show($"Ejecutando dotnet run en {workingDirectory}...");
            executor.ExecuteDotnetRun(workingDirectory);
        }

    /// <summary>
    /// Este método se ejecuta cuando el usuario hace clic en el botón "Conectar a la base de datos remota"
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void conectarBdbtn_Click(object sender, RoutedEventArgs e)
        {
        // Lógica para conectarse a la base de datos remota
        string ipAddress = ipTextBox.Text;
        string port = portTextBox.Text;

        MessageBox.Show($"Conectando en {ipAddress}:{port}...");
        }
    }
}
