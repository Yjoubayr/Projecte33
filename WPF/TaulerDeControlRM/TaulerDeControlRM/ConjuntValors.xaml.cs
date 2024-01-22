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
using Valor = TaulerDeControlRM.Valor;

namespace TaulerDeControlRM
{
    /// <summary>
    /// Interaction logic for ConjuntValors.xaml
    /// </summary>
    public partial class ConjuntValors : UserControl
    {
        public List<string> valors;
        public List<string> valorsRestants;
        public Boolean esPotRepetir;
        public static readonly DependencyProperty LblValueProperty =
            DependencyProperty.Register("LblValue", typeof(string), typeof(ConjuntValors));

        public List<string> Valors
        {
            get { return valors; }
            set
            {
                valors = value;
                valorsRestants = valors;
            }
        }
        public string LblValue
        {
            get { return (string)GetValue(LblValueProperty); }
            set { SetValue(LblValueProperty, value); }
        }

        public ConjuntValors()
        {
            InitializeComponent();
        }

        private void btAfegirValork(object sender, RoutedEventArgs e)
        {
            Valor newValue = new Valor();
            if (esPotRepetir)
            {
                newValue.SetPossibleValues(valors);
            }
            else
            {
                newValue.SetPossibleValues(valorsRestants);
                newValue.cmbValue.SelectionChanged += ComboBox_SelectionChanged;
            }
            

            Button btEliminar = new Button();
            btEliminar.Content = "x";
            btEliminar.Height = 20;
            btEliminar.Width = 20;
            btEliminar.Margin = new Thickness(15,15, 0, 15);
            btEliminar.Click += btEliminarClick;

            // Add a new RowDefinition to the Grid
            gridValues.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

            // Set the content of the new row to the new Valor UserControl
            gridValues.Children.Add(newValue);

            // Set the Grid.Row property for the new Valor UserControl
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
                // Remove the Valor UserControl
                Valor ValorToRemove = gridValues.Children
                    .OfType<Valor>()  // Filter to include only elements of type Valor
                    .FirstOrDefault(child => Grid.GetRow(child) == rowIndex && Grid.GetColumn(child) == 0);

                if (ValorToRemove != null)
                {
                    if (ValorToRemove.cmbValue.SelectedItem != null)
                    {
                        valorsRestants.Add(ValorToRemove.cmbValue.SelectedItem.ToString());
                    }
                    gridValues.Children.Remove(ValorToRemove);
                }
                UpdateComboBoxItemsSource();
                // Remove the "x" button
                gridValues.Children.Remove(sender as UIElement);

                // Move up any Valor UserControls that are below the one that was removed
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
        public void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Get the selected value from the ComboBox that triggered the event
            ComboBox selectedComboBox = (ComboBox)sender;
            string selectedValue = selectedComboBox.SelectedItem as string;

            if (selectedValue != null)
            {
                // Remove the selected value from the available values
                valorsRestants.Remove(selectedValue);

                // Update the items source for all ComboBoxes
                UpdateComboBoxItemsSource();
            }
        }
        private void UpdateComboBoxItemsSource()
        {
            // Update the items source for all ComboBoxes
            foreach (ComboBox comboBox in FindVisualChildren<ComboBox>(this))
            {
                comboBox.ItemsSource = valorsRestants;
            }
        }
        private static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);

                    if (child is T obj)
                    {
                        yield return obj;
                    }

                    foreach (T childOfChild in FindVisualChildren<T>(child))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }

    }
}
