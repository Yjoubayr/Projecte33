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

namespace TaulerDeControlRM
{
    /// <summary>
    /// Lógica de interacción para CreacioDockerWindow.xaml
    /// </summary>
    public partial class CreacioDockerWindow : Window
    {
        public CreacioDockerWindow()
        {
            InitializeComponent();
        }

        public void Button_Click(object sender, RoutedEventArgs e)
        {



            string selectedDatabase = (databaseListBox.SelectedItem as ListBoxItem)?.Content.ToString();

            if (selectedDatabase == null)
            {
                MessageBox.Show("Por favor, selecciona una base de datos");
                return;
            }
            if (selectedDatabase == "PostgreSQL")
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

            }

            // Ejemplo de acción: mostrar un mensaje con la configuración seleccionada
            MessageBox.Show($"Base de datos seleccionada: {selectedDatabase}");
        }
    }
}
