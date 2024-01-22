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
using System.Windows.Navigation;
using System.Windows.Shapes;
using ReproductorMusicaComponentLibrary.Classes;

namespace TaulerDeControlRM
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class PageCreaCanco : Page
    {
        public PageCreaCanco()
        {
            InitializeComponent();
        }


        private void btOk_Click(object sender, RoutedEventArgs e)
        {
            string cancoName = this.txtCancoName.Text.ToString();
            string cancoYear = this.txtCancoYear.Text.ToString();
            
            Canco canco = new Canco();
            await CA_Canco.PostCancoAsync(canco);
        }
    }
}
