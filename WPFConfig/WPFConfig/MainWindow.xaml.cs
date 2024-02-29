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
            }
            else if (selectedDatabase == "MySQL")
            {
                composeFile = System.IO.Path.Combine(composeFolder, "mysql-compose.yaml");
            }
            else if (selectedDatabase == "Microsoft SQL Server")
            {
                composeFile = System.IO.Path.Combine(composeFolder, "mssql-compose.yaml");
            }
            else
            {
                MessageBox.Show("Base de datos seleccionada no compatible");
                return;
            }

            // Comprueba si el archivo de configuración existe
            if (!File.Exists(composeFile))
            {
                MessageBox.Show("El archivo de configuración no existe");
                return;
            }
            else
            {
                MessageBox.Show($"Instalando {selectedDatabase} en modo local...");
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
