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

namespace TaulerDeControlRM
{
    public partial class ConfiguracioWindow : Window
    {
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



            // Ejemplo de acción: mostrar un mensaje con la configuración seleccionada
            MessageBox.Show($"Base de datos seleccionada: {selectedDatabase}");
        }


    }
}

