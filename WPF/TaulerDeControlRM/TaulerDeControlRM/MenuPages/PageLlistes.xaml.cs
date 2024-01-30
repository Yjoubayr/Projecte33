using ReproductorMusicaComponentLibrary.Classes;
using ReproductorMusicaComponentLibrary.ConnexioAPI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace TaulerDeControlRM
{
    /// <summary>
    /// Interaction logic for PageLlistes.xaml
    /// </summary>
    public partial class PageLlistes : Page
    {
        public PageLlistes()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void BtMostrarCancons_Click(object sender, RoutedEventArgs e)
        {
            //var num = int.Parse(txtNum.Text);
            try
            {
                List<Canco> llista = await CA_Canco.GetCanconsAsync();
                foreach (Canco canco in llista)
                {
                    if (canco != null)
                    {
                        songListView.Items.Add(canco);


                    }
                    else
                    {
                        throw new Exception("No s'ha pogut carregar la llista de cançons");
                    }


                }
            }
            catch(Exception ex)
            {
                throw new Exception("No s'ha fet correctament la petico a l'api" + ex );
            }
            /*Canco canco = new Canco();
             canco.Nom = "Canço1";
             canco.Any = 2021;
             songListView.Items.Add(canco);*/
        }
    }
}
