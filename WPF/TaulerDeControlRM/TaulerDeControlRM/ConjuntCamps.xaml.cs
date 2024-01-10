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
        private string[] campsDeCerca = { "Nom Cançó", "Artista", "Album", "Llista de reproducció", "Versió" };
        public ConjuntCamps()
        {
            InitializeComponent();
        }

        private void btAfegirCampClick(object sender, RoutedEventArgs e)
        {
            Camp newCamp = new Camp();
            string[] campsDeCercaRestants = campsDeCerca;

            foreach (UIElement child in gridCampsCerca.Children)
            {
                if (child is Camp)
                {
                    // Assuming GetSelectedValue is a method you've defined in your Camp class
                    string selectedValue = (child as Camp).GetSelectedValue();

                    // Check if the selected value is not null or empty before subtracting
                    if (!string.IsNullOrEmpty(selectedValue))
                    {
                        campsDeCercaRestants = campsDeCercaRestants.Except(new string[] { selectedValue }).ToArray();
                    }
                }
            }

            newCamp.SetPossibleValues(campsDeCercaRestants);

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
                UIElement campToRemove = gridCampsCerca.Children.Cast<UIElement>()
                    .FirstOrDefault(child => Grid.GetRow(child) == rowIndex && Grid.GetColumn(child) == 0);

                if (campToRemove != null)
                {
                    gridCampsCerca.Children.Remove(campToRemove);
                }

                // Remove the "x" button
                gridCampsCerca.Children.Remove(sender as UIElement);

                // Remove the row definition
                gridCampsCerca.RowDefinitions.RemoveAt(rowIndex);
            }
        }

    }
}
