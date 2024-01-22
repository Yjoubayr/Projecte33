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
    /// Interaction logic for Valor.xaml
    /// </summary>
    public partial class Valor : UserControl
    {
        //private string previousSelectedValue;
      
        public event EventHandler ButtonClicked;
        public Valor()
        {
            InitializeComponent();
            cmbValue.IsEditable = true;
            cmbValue.IsTextSearchEnabled = true;
        }
        // Method to set the possible values for the ComboBox
        public void SetPossibleValues(List<String> values)
        {
            cmbValue.ItemsSource = values;
        }


        
    }
}
