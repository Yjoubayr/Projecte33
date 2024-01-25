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

namespace TaulerDeControlRM
{
    /// <summary>
    /// Interaction logic for Cercador.xaml
    /// </summary>
    public partial class Cercador : UserControl
    {
        public Cercador()
        {
            InitializeComponent();
            ConjuntValors cvCampsCerca = new ConjuntValors("Camp", new List<string> { "Nom Cançó", "Artista", "Album", "Llista de reproducció", "Versió" }, false, true);

            GridConjuntValors gcvCampsCerca = new GridConjuntValors(false, new List<ConjuntValors> { cvCampsCerca});
            gcvCampsCerca.Valors = new List<ConjuntValors> { cvCampsCerca };
            
            

            

            List<string> cmbOrdreValues = new List<string>
            { "Reproduccions", "Data Creació" , "Data Reproducció" };

            // Assign the collection to the ComboBox's ItemsSource
            cmbOrdre.ItemsSource = cmbOrdreValues;

            List<string> cmbSentitValues = new List<string>
            { "Ascendent", "Descendent" };
            cmbSentit.ItemsSource = cmbSentitValues;

            List<string> cmbTipusResultatValues = new List<string>
            { "Cançons", "Artistes", "Albums", "Llistes de reproducció" };
            cmbTipusResultat.ItemsSource = cmbTipusResultatValues;
            cmbTipusResultat.IsTextSearchEnabled = false;

        }
    }
}
