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
    /// Interaction logic for Camp.xaml
    /// </summary>
    public partial class Camp : UserControl
    {
        private string previousSelectedValue;
        public event EventHandler ButtonClicked;
        public Camp()
        {
            InitializeComponent();

            cmbCamp.SelectionChanged += cmbCamp_SelectionChanged;
        }
        // Method to set the possible values for the ComboBox
        public void SetPossibleValues(List<String> values)
        {
            cmbCamp.ItemsSource = values;
        }

        // Method to get the selected value from the ComboBox
        public string GetSelectedValue()
        {
            return cmbCamp.SelectedItem?.ToString();
        }

        private void cmbCamp_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Check if the selected value can be changed
            if(cmbCamp.SelectedItem != null)
            {
                cmbCamp.IsEnabled = false;
                ConjuntCamps.campsDeCercaRestants.Remove(cmbCamp.SelectedItem.ToString());
            }
        }
    }
}
