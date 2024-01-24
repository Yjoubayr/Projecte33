﻿using System;
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
    /// GridConjuntValors es tracta d'un control d'usuari que a partir d'un o més ConjuntValors
    /// permet tenir una graella amb ComboBoxs amb els valors definits al seu ConjuntValors.
    /// Si el ConjuntValors té el paràmetre EsPotRepetir a false, no podrà haver-hi ComboBoxs amb el mateix valor.
    /// Els botons Afegir i Eliminar permeten afegir i eliminar files de ComboBoxs.
    /// ShowNames permet mostrar o no el nom del ConjuntValors a la primera fila de la graella.
    /// </summary>
    public partial class GridConjuntValors : UserControl
    {
        private Dictionary<ComboBox, string> comboBoxValues = new Dictionary<ComboBox, string>();

        public static readonly DependencyProperty ValorsProperty =
            DependencyProperty.Register(
                "Valors",
                typeof(List<ConjuntValors>),
                typeof(GridConjuntValors),
                new PropertyMetadata(new List<ConjuntValors>()));

        public List<ConjuntValors> Valors
        {
            get { return (List<ConjuntValors>)GetValue(ValorsProperty); }
            set { SetValue(ValorsProperty, value); }
        }

        public static readonly DependencyProperty ShowNamesProperty =
            DependencyProperty.Register(
                "ShowNames",
                typeof(bool),
                typeof(GridConjuntValors),
                new PropertyMetadata(false));

        public bool ShowNames
        {
            get { return (bool)GetValue(ShowNamesProperty); }
            set { SetValue(ShowNamesProperty, value); }
        }
        

        /// <summary>
        /// Constructor de GridConjuntValors
        /// </summary>
        /// <param name="showNames"></param>
        /// <param name="llistaConjuntValors"></param>
        public GridConjuntValors(bool showNames, List<ConjuntValors> llistaConjuntValors)
        {
            InitializeComponent();
            Valors= llistaConjuntValors;
            ShowNames = showNames;
            //Row pels Headers
            gridConjuntValors.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            int col = 0;
            foreach (ConjuntValors cv in llistaConjuntValors)
            {
                //Afegir columna si cal
                if (col > gridConjuntValors.ColumnDefinitions.Count - 1)
                {
                    ColumnDefinition colDef1 = new ColumnDefinition();
                    colDef1.Width = new GridLength(1, GridUnitType.Star);
                    gridConjuntValors.ColumnDefinitions.Add(colDef1);
                }
                if (showNames)
                {
                    Label nom = new Label();
                    nom.Content = cv.Nom;
                    gridConjuntValors.Children.Add(nom);
                    Grid.SetColumn(nom, col);
                    Grid.SetRow(nom, 0);
                    col++;
                }
            }

            ColumnDefinition colBtEliminar = new ColumnDefinition();
            colBtEliminar.Width = GridLength.Auto;
            gridConjuntValors.ColumnDefinitions.Add(colBtEliminar);

            btAfegir.Content = "Afegir";
        }
        /// <summary>
        /// Mètode que s'executa quan es fa click al botó Afegir.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btAfegir_Click(object sender, RoutedEventArgs e)
        {
            gridConjuntValors.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            int col = 0;
            foreach (ConjuntValors cv in Valors)
            {
                ComboBox comboBox = new ComboBox();
                if(cv.Cerca)
                {
                    comboBox.IsEditable = true;
                    comboBox.IsTextSearchEnabled = true;
                    comboBox.IsTextSearchCaseSensitive = false;
                }
                if (cv.EsPotRepetir)
                {
                    comboBox.ItemsSource = cv.Valors;
                }
                else
                {
                    comboBox.ItemsSource = cv.ValorsRestants;
                    comboBoxValues.Add(comboBox, null);
                    comboBox.SelectionChanged += comboBox_SelectionChanged;
                }
                gridConjuntValors.Children.Add(comboBox);
                Grid.SetRow(comboBox, gridConjuntValors.RowDefinitions.Count - 1);
                Grid.SetColumn(comboBox, col);
                col++;
            }
            //Crear botó eliminar a la nova fila i última columna
            Button btEliminar = new Button();
            btEliminar.Content = "x";
            btEliminar.Height = 20;
            btEliminar.Width = 20;
            btEliminar.Margin = new Thickness(15, 15, 0, 15);
            btEliminar.Click += btEliminarClick;

            gridConjuntValors.Children.Add(btEliminar);

            Grid.SetRow(btEliminar, gridConjuntValors.RowDefinitions.Count-1);
            Grid.SetColumn(btEliminar, gridConjuntValors.ColumnDefinitions.Count -1);
        }
        private void btEliminarClick(object sender, RoutedEventArgs e)
        {
            Button deleteButton = (Button)sender;
            int rowIndex = Grid.GetRow(deleteButton);

            if (rowIndex >= 0 && rowIndex < gridConjuntValors.RowDefinitions.Count)
            {
                gridConjuntValors.RowDefinitions.RemoveAt(rowIndex);

                // Remove child controls in the deleted row
                foreach (UIElement child in gridConjuntValors.Children.Cast<UIElement>().ToList())
                {
                    int row = Grid.GetRow(child);
                    int col = Grid.GetColumn(child);
                    if (child is ComboBox)
                    {
                        ComboBox cb = (ComboBox)child;
                        if (row == rowIndex)
                        {
                            gridConjuntValors.Children.Remove(child);
                            ConjuntValors cv = Valors.ElementAtOrDefault(col);
                            if (!cv.EsPotRepetir && cb.SelectedItem!=null)
                            {
                                cv.ValorsRestants.Add(cb.SelectedItem.ToString());
                            }
                        }
                        else if (row > rowIndex)
                        {
                            // Shift rows below up
                            Grid.SetRow(child, row - 1);
                        }
                    }
                    else
                    {
                        if (row == rowIndex)
                        {
                            gridConjuntValors.Children.Remove(child);
                        }
                        else if (row > rowIndex)
                        {
                            // Shift rows below up
                            Grid.SetRow(child, row - 1);
                        }
                    }
                    
                }
                
            }
            updateComboBoxElements(-1);
        }
        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Get the column index of the ComboBox
            int columnIndex = Grid.GetColumn((ComboBox)sender);
            if (sender is ComboBox myComboBox)
            {
                // Get the selected value
                if (myComboBox.SelectedItem != null)
                {
                    string selectedValue = myComboBox.SelectedItem.ToString();

                    ConjuntValors cv = Valors.ElementAtOrDefault(columnIndex);

                    if (!cv.EsPotRepetir)
                    {
                        if (comboBoxValues[myComboBox] != null)
                        {
                            cv.ValorsRestants.Add(comboBoxValues[myComboBox]);
                        }
                        comboBoxValues[myComboBox]= selectedValue;
                        cv.ValorsRestants.Remove(selectedValue);
                    }
                }
            }
            updateComboBoxElements(columnIndex);
        }
        private void updateComboBoxElements(int param)
        {
            List<ConjuntValors> ValorsChange;
            //Si el param és -1 mira tots els conjunts de valors, si no només toca el que està a la posició param
            int cvCol;
            if (param == -1)
            {
                ValorsChange = Valors;
                cvCol = 0;
            }
            else
            {
                ValorsChange = new List<ConjuntValors> { Valors.ElementAtOrDefault(param)};
                cvCol = param;
            }
            
            foreach (ConjuntValors cv in ValorsChange)
            {
                if (!cv.EsPotRepetir)
                {
                    foreach (UIElement child in gridConjuntValors.Children.Cast<UIElement>().ToList())
                    {
                        int row = Grid.GetRow(child);
                        int col = Grid.GetColumn(child);
                        if (child is ComboBox)
                        {
                            ComboBox cb = (ComboBox)child;
                            if (col == cvCol)
                            {
                                List<string> newList = cv.ValorsRestants.ToList(); // Create a new list based on cv.ValorsRestants
                                if (cb.SelectedValue != null)
                                {
                                    newList.Add(cb.SelectedValue.ToString()); // Add the new item to the new lis
                                }
                                // Now 'newList' contains the items from 'cv.ValorsRestants' plus the new item
                                cb.ItemsSource = newList;                                
                            }
                        }
                    }
                }
                cvCol++;
            }
        }
    }
}
