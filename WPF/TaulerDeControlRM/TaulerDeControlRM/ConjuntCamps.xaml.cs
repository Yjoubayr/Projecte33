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
        private static string[] campsDeCerca = { "Nom Cançó", "Artista", "Album", "Llista de reproducció", "Versió" };
        public static List<string> campsDeCercaRestants = ConjuntCamps.campsDeCerca.ToList();
        public ConjuntCamps()
        {
            InitializeComponent();
        }

        private void btAfegirCampClick(object sender, RoutedEventArgs e)
        {
            Camp newCamp = new Camp();

            newCamp.SetPossibleValues(ConjuntCamps.campsDeCercaRestants);

            Button btEliminar = new Button();
            btEliminar.Content = "x";
            btEliminar.Height = 20;
            btEliminar.Width = 20;
            btEliminar.Margin = new Thickness(15,15, 0, 15);
            btEliminar.Click += btEliminarClick;

            // Add a new RowDefinition to the Grid
            gridCampsCerca.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

            // Set the content of the new row to the new Camp UserControl
            gridCampsCerca.Children.Add(newCamp);

            // Set the Grid.Row property for the new Camp UserControl
            Grid.SetRow(newCamp, gridCampsCerca.RowDefinitions.Count - 1);
            Grid.SetColumn(newCamp, 0);

            gridCampsCerca.Children.Add(btEliminar);

            Grid.SetRow(btEliminar, gridCampsCerca.RowDefinitions.Count - 1);
            Grid.SetColumn(btEliminar, 1);
        }
        private void btEliminarClick(object sender, RoutedEventArgs e)
        {
            // Find the corresponding row index
            int rowIndex = Grid.GetRow(sender as UIElement);

            if (rowIndex >= 0 && rowIndex < gridCampsCerca.RowDefinitions.Count)
            {
                // Remove the Camp UserControl
                Camp campToRemove = gridCampsCerca.Children
                    .OfType<Camp>()  // Filter to include only elements of type Camp
                    .FirstOrDefault(child => Grid.GetRow(child) == rowIndex && Grid.GetColumn(child) == 0);

                if (campToRemove != null)
                {
                    if (campToRemove.cmbCamp.SelectedItem != null)
                    {
                        ConjuntCamps.campsDeCercaRestants.Add(campToRemove.cmbCamp.SelectedItem.ToString());
                    }
                    gridCampsCerca.Children.Remove(campToRemove);
                }

                // Remove the "x" button
                gridCampsCerca.Children.Remove(sender as UIElement);

                // Move up any Camp UserControls that are below the one that was removed
                foreach (UIElement element in gridCampsCerca.Children)
                {
                    if (Grid.GetRow(element) > rowIndex)
                    {
                        Grid.SetRow(element, Grid.GetRow(element) - 1);
                    }

                }

                // Remove last row
                gridCampsCerca.RowDefinitions.RemoveAt(gridCampsCerca.RowDefinitions.Count - 1);
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
