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
    /// Interaction logic for GridConjuntValors.xaml
    /// </summary>
    public partial class GridConjuntValors : UserControl
    { 
        public string BtText { get; set; } = "Afegir valor";
        public List<ConjuntValors> valors { get; set; } = new List<ConjuntValors>();


        //definir columnes per múliples valors amb botó d'afegir i d'eliminar
        public GridConjuntValors()
        {
            InitializeComponent();
            foreach (ConjuntValors cv in valors)
            {
                ColumnDefinition colDef1 = new ColumnDefinition();
                colDef1.Width = new GridLength(1, GridUnitType.Star);
                gridValors.ColumnDefinitions.Add(colDef1);
                Grid.SetColumn(cv, gridValors.ColumnDefinitions.Count - 1);
                Grid.SetRow(cv, 0);
                gridValors.Children.Add(cv);
            }
            ColumnDefinition colBtEliminar = new ColumnDefinition();
            gridValors.ColumnDefinitions.Add(colBtEliminar);
            btAfegir.Content = BtText;
        }

        private void btAfegir_Click(object sender, RoutedEventArgs e)
        {
            int col = 0;
            gridValors.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

            foreach (ConjuntValors cv in valors)
            {
                Valor newValue = new Valor();
                if (cv.esPotRepetir)
                {
                    newValue.SetPossibleValues(cv.valors);
                }
                else
                {
                    newValue.SetPossibleValues(cv.valorsRestants);
                    newValue.cmbValue.SelectionChanged += cv.ComboBox_SelectionChanged;
                }

                // Set the content of the new row to the new Valor UserControl
                gridValors.Children.Add(newValue);

                // Set the Grid.Row property for the new Valor UserControl
                Grid.SetRow(newValue, gridValors.RowDefinitions.Count - 1);
                Grid.SetColumn(newValue, col);
                col++;

            }
            

            Button btEliminar = new Button();
            btEliminar.Content = "x";
            btEliminar.Height = 20;
            btEliminar.Width = 20;
            btEliminar.Margin = new Thickness(15, 15, 0, 15);
            btEliminar.Click += btEliminarClick;

            gridValors.Children.Add(btEliminar);

            Grid.SetRow(btEliminar, gridValors.RowDefinitions.Count - 1);
            Grid.SetColumn(btEliminar, gridValors.ColumnDefinitions.Count -1);
        }
        private void btEliminarClick(object sender, RoutedEventArgs e)
        {
            //gridValues.Children.RemoveAt(gridValues.Children.Count - 1);
            //gridValues.Children.RemoveAt(gridValues.Children.Count - 1);
            //gridValues.RowDefinitions.RemoveAt(gridValues.RowDefinitions.Count - 1);

            //// Find the corresponding row index
            //int rowIndex = Grid.GetRow(sender as UIElement);

            //if (rowIndex >= 0 && rowIndex < gridValors.RowDefinitions.Count)
            //{
            //    // Remove the Valor UserControl
            //    Valor ValorToRemove = gridValors.Children
            //        .OfType<Valor>()  // Filter to include only elements of type Valor
            //        .FirstOrDefault(child => Grid.GetRow(child) == rowIndex && Grid.GetColumn(child) == 0);

            //    if (ValorToRemove != null)
            //    {
            //        if (ValorToRemove.cmbValue.SelectedItem != null)
            //        {
            //            valorsRestants.Add(ValorToRemove.cmbValue.SelectedItem.ToString());
            //        }
            //        gridValues.Children.Remove(ValorToRemove);
            //    }
            //    UpdateComboBoxItemsSource();
            //    // Remove the "x" button
            //    gridValues.Children.Remove(sender as UIElement);

            //    // Move up any Valor UserControls that are below the one that was removed
            //    foreach (UIElement element in gridValues.Children)
            //    {
            //        if (Grid.GetRow(element) > rowIndex)
            //        {
            //            Grid.SetRow(element, Grid.GetRow(element) - 1);
            //        }

            //    }

            //    // Remove last row
            //    gridValues.RowDefinitions.RemoveAt(gridValues.RowDefinitions.Count - 1);
            //}
        }
    }
}
