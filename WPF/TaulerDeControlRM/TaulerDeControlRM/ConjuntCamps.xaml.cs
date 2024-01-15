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
using Camp = TaulerDeControlRM.Camp;

namespace TaulerDeControlRM
{
    /// <summary>
    /// Interaction logic for ConjuntCamps.xaml
    /// </summary>
    public partial class ConjuntCamps : UserControl
    {
        private static List<string> valors;
        public static List<string> valorsRestants;
        public static readonly DependencyProperty BtTextProperty =
            DependencyProperty.Register("BtText", typeof(string), typeof(ConjuntCamps));
        public static List<string> Valors
        {
            get { return valors; }
            set
            {
                valors = value;
                valorsRestants = valors;
            }
        }
        public string BtText
        {
            get { return (string)GetValue(BtTextProperty); }
            set { SetValue(BtTextProperty, value); }
        }
        public ConjuntCamps()
        {
            InitializeComponent();
        }

        private void btAfegirCampClick(object sender, RoutedEventArgs e)
        {
            Camp newValue = new Camp();

            newValue.SetPossibleValues(ConjuntCamps.valorsRestants);

            Button btEliminar = new Button();
            btEliminar.Content = "x";
            btEliminar.Height = 20;
            btEliminar.Width = 20;
            btEliminar.Margin = new Thickness(15,15, 0, 15);
            btEliminar.Click += btEliminarClick;

            // Add a new RowDefinition to the Grid
            gridValues.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

            // Set the content of the new row to the new Camp UserControl
            gridValues.Children.Add(newValue);

            // Set the Grid.Row property for the new Camp UserControl
            Grid.SetRow(newValue, gridValues.RowDefinitions.Count - 1);
            Grid.SetColumn(newValue, 0);

            gridValues.Children.Add(btEliminar);

            Grid.SetRow(btEliminar, gridValues.RowDefinitions.Count - 1);
            Grid.SetColumn(btEliminar, 1);
        }
        private void btEliminarClick(object sender, RoutedEventArgs e)
        {
            // Find the corresponding row index
            int rowIndex = Grid.GetRow(sender as UIElement);

            if (rowIndex >= 0 && rowIndex < gridValues.RowDefinitions.Count)
            {
                // Remove the Camp UserControl
                Camp campToRemove = gridValues.Children
                    .OfType<Camp>()  // Filter to include only elements of type Camp
                    .FirstOrDefault(child => Grid.GetRow(child) == rowIndex && Grid.GetColumn(child) == 0);

                if (campToRemove != null)
                {
                    if (campToRemove.cmbValue.SelectedItem != null)
                    {
                        ConjuntCamps.valorsRestants.Add(campToRemove.cmbValue.SelectedItem.ToString());
                    }
                    gridValues.Children.Remove(campToRemove);
                }

                // Remove the "x" button
                gridValues.Children.Remove(sender as UIElement);

                // Move up any Camp UserControls that are below the one that was removed
                foreach (UIElement element in gridValues.Children)
                {
                    if (Grid.GetRow(element) > rowIndex)
                    {
                        Grid.SetRow(element, Grid.GetRow(element) - 1);
                    }

                }

                // Remove last row
                gridValues.RowDefinitions.RemoveAt(gridValues.RowDefinitions.Count - 1);
            }
        }


        public static void GetElementsAtGridPosition(Grid grid, int row, int column)
        {
            List<UIElement> elements = new List<UIElement>();

            foreach (UIElement element in grid.Children)
            {
                int elementRow = Grid.GetRow(element);
                int elementColumn = Grid.GetColumn(element);

                if (elementRow == row && elementColumn == column)
                {
                    MessageBox.Show(element.ToString());
                }
            }
        }

}
}
