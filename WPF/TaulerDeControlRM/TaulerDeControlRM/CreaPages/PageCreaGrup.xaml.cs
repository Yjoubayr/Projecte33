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

namespace TaulerDeControlRM.CreaPages
{
    /// <summary>
    /// Lógica de interacción para PageCreaGrup.xaml
    /// </summary>
    public partial class PageCreaGrup : Page
    {
        public PageCreaGrup()
        {
            InitializeComponent();
            ConjuntValors cvMusics = new ConjuntValors("Músic", new List<string> { "Joan", "Josep", "Ferran", "Maria", "Miquel" }, false, true);
            List<ConjuntValors> llistaConjutValors = new List<ConjuntValors> { cvMusics };
            GridConjuntValors gcv = new GridConjuntValors(false, llistaConjutValors);
            spMusics.Children.Add(gcv);
        }
    }
}
