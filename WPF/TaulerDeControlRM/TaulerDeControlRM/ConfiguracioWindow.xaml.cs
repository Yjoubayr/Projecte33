using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows;
using System.Configuration;
using System.Net.Http;
using ReproductorMusicaComponentLibrary.Classes;
using ReproductorMusicaComponentLibrary.ConnexioAPI;

namespace TaulerDeControlRM
{
    public partial class ConfiguracioWindow : Window
    {

        public static string baseApi = ConfigurationManager.AppSettings["baseApi"];
        private static readonly HttpClient _httpClient = new HttpClient();

        public ConfiguracioWindow()
        {
            InitializeComponent();

            // Suscribirse al evento Click del botón guardarButton
            guardarButton.Click += guardarButton_Click;
            
        }

        private void guardarButton_Click(object sender, RoutedEventArgs e)
        {
            // Aquí puedes escribir la lógica para guardar la configuración
            // string ip = ipTextBox.Text;
            // string port = portTextBox.Text;
            string selectedDatabase = (databaseListBox.SelectedItem as ListBoxItem)?.Content.ToString();
            var ipDocker = ipTextBox.ToString();
            var portDocker = portTextBox.ToString();
            var nomDocker = nomTextBox.ToString();
            var passwdDocker = passwdTextBox.ToString();

            try
            {
                if (!string.IsNullOrEmpty(nomDocker) && !string.IsNullOrEmpty(passwdDocker))
                {
                    if (passwdDocker.Length < 10)
                    {
                        MessageBox.Show($"És obligat posar 10 caracters o més a la contrasenya");
                        return;
                    }
                    else
                    {
                        if (selectedDatabase == null)
                        {
                            MessageBox.Show("Siusplau, escull una base de dades");
                            return;
                        }
                        else if (selectedDatabase == "PostgreSQL")
                        {
                            changeDockerApi("PostgreSQL");
                        }
                        else if (selectedDatabase == "MySQL")
                        {
                            changeDockerApi("MySQL");

                        }
                        else if (selectedDatabase == "Microsoft SQL Server")
                        {
                            changeDockerApi("MicrosoftSQLServer");

                            DockerManager.DescarregarMicrosoftSqlServer();

                            DockerManager.ExecutarContenidorMicrosoftSqlServer(nomDocker, passwdDocker);
                        }
                    }
                }
                else
                {
                    MessageBox.Show($"És obligat posar un nom i una contrasenya");
                }
            }
            catch(Exception ex)
            {
                
            }
        }

        private async Task<string?> changeDockerApi(string database)
        {
            HttpResponseMessage response = await _httpClient.GetAsync(baseApi+"/"+database);

            if (response.IsSuccessStatusCode)
            {
                // Read the response content
                return await response.Content.ReadAsStringAsync();
            }
            else
            {
                // Handle the error
                // MessageBox.Show($"Error: " + response.ReasonPhrase);
                return null;
            }
        }
    }
}

