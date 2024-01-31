using ReproductorMusicaComponentLibrary.Classes;
using ReproductorMusicaComponentLibrary.ConnexioAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TaulerDeControlRM.CreaPages
{
    /// <summary>
    /// Lógica de interacción para PageCreaGrup.xaml
    /// </summary>
    public partial class PageCreaGrup : Page
    {
        private static readonly Regex _regex = new Regex("[^0-9.-]+"); //regex that matches disallowed text

        public PageCreaGrup()
        {
            InitializeComponent();
        }

        private void btOk_Click(object sender, RoutedEventArgs e)
        {

            if (this.txtNom.Text.ToString() == string.Empty
                || this.txtAny.Text.ToString() == string.Empty)
            {
                MessageBox.Show("ERROR! \n Emplena el formulari abans de pujar el Grup.");
            }
            else if (this.txtNom.Text.ToString().Length > 30)
            {
                MessageBox.Show("ERROR! \n El nom del grup és massa llarg.");
            }
            else if (_regex.IsMatch(this.txtAny.Text.ToString()))
            {
                MessageBox.Show("ERROR! \n Has de ficar l'Any del grup amb el format vàlid.");
            }
            else
            {
                Grup grup = new Grup();
                grup.Nom = this.txtNom.Text;
                grup.Any = int.Parse(this.txtAny.Text);
                grup.LMusics = null;
                grup.LTocar = null;
                CA_Grup.PostGrupAsync(grup);
                this.txtNom.Text = string.Empty;
                this.txtAny.Text = string.Empty;
                MessageBox.Show("Grup creat CORRECTAMENT!");
            }
        }
    }
}
