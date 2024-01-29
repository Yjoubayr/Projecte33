using System;
using System.Collections.Generic;
using System.Data.Common;
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
using System.Xml.Linq;

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
            if (lastValuesNotNull())
            {
                gridConjuntValors.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
                int col = 0;
                foreach (ConjuntValors cv in Valors)
                {
                    ComboBox comboBox = new ComboBox();
                    if (cv.Cerca)
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

                    comboBox.Loaded += ComboBox_Loaded;

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

                Grid.SetRow(btEliminar, gridConjuntValors.RowDefinitions.Count - 1);
                Grid.SetColumn(btEliminar, gridConjuntValors.ColumnDefinitions.Count - 1);

            }
        }
        private void ComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;

            // Get the ComboBox template
            ControlTemplate template = comboBox.Template;
            if (template != null)
            {
                // Find the inner TextBox by its name
                TextBox textBox = template.FindName("PART_EditableTextBox", comboBox) as TextBox;
                if (textBox != null)
                {
                    // Add LostFocus and GotFocus event handlers to the TextBox
                    textBox.LostFocus += (s, e) => TextBox_LostFocus(s, e, comboBox);
                    textBox.GotFocus += (s, e) => TextBox_GotFocus(s, e, comboBox);
                    textBox.TextChanged += (s, e) => TextBox_TextChanged(s, e, comboBox);
                }
            }
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
            ConjuntValors cv = Valors.ElementAtOrDefault(columnIndex);
            if (sender is ComboBox myComboBox)
            {
                // Get the selected value
                if (myComboBox.SelectedItem != null)
                {
                    string selectedValue = myComboBox.SelectedItem.ToString();

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
                else
                {
                    //afegir label Nou
                    //newValueLabel(myComboBox,"nou");
                }
            }
            updateComboBoxElements(columnIndex);
            
        }
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e, ComboBox comboBox)
        {
            TextBox textBox = sender as TextBox;
            string enteredText = textBox.Text;

            int columnIndex = Grid.GetColumn((ComboBox)comboBox);
            ConjuntValors cv = Valors.ElementAtOrDefault(columnIndex);

            // Check if the entered text matches any existing item in the ComboBox
            bool matchesExistingItem = cv.Valors.Contains(enteredText);

            if (!matchesExistingItem && enteredText!=string.Empty)
            {
                newValueLabel(comboBox, "nou");
            }
            else
            {
                if(ExistsInGridCell(columnIndex, Grid.GetRow(comboBox), typeof(Label)))
                {
                    deleteLabelInGridCell(comboBox);
                }
            }
            
            if (!cv.EsPotRepetir)
            {
                if (cv.ValorsNous.Contains(enteredText))
                {
                    newValueLabel(comboBox, "repetit");
                }
            }
        }

        private void deleteLabelInGridCell(ComboBox comboBox)
        {
            int columnIndex = Grid.GetColumn((ComboBox)comboBox);
            int rowIndex = Grid.GetRow((ComboBox)comboBox);

            var childrenCopy = gridConjuntValors.Children.Cast<UIElement>().ToList();
            foreach (var child in childrenCopy)
            {
                if (child is UIElement ui)
                {
                    if (child.GetType() == typeof(Label))
                    {
                        if (Grid.GetColumn(ui) == columnIndex && Grid.GetRow(ui) == rowIndex)
                        {
                            // Element found in the specified cell
                            gridConjuntValors.Children.Remove(ui);
                        }
                    }
                }
            }

        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e, ComboBox comboBox)
        {
            int columnIndex = Grid.GetColumn((ComboBox)comboBox);
            int rowIndex = Grid.GetRow((ComboBox)comboBox);
            ConjuntValors cv = Valors.ElementAtOrDefault(columnIndex);
            bool matchesExistingItem = cv.ValorsNous.Contains(comboBox.Text);
            if (!matchesExistingItem)
            {
                cv.ValorsNous.Add(comboBox.Text);
            }
        }
        private void TextBox_GotFocus(object sender, RoutedEventArgs e, ComboBox comboBox) {
            if (sender is TextBox myTextBox)
            {
                if (myTextBox.Text != string.Empty)
                {
                    int columnIndex = Grid.GetColumn((ComboBox)comboBox);
                    int rowIndex = Grid.GetRow((ComboBox)comboBox);
                    ConjuntValors cv = Valors.ElementAtOrDefault(columnIndex);
                    cv.ValorsNous.Remove(comboBox.Text);

                }
            }
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
        public bool lastValuesNotNull()
        {
            int lastRowIndex = gridConjuntValors.RowDefinitions.Count - 1;
            if (lastRowIndex >= 1) {
                var comboBoxesInLastRow = gridConjuntValors.Children
               .OfType<ComboBox>()
               .Where(comboBox => Grid.GetRow(comboBox) == lastRowIndex)
               .ToList();

                foreach (var comboBox in comboBoxesInLastRow)
                {
                    try
                    {
                        string selectedValue = comboBox.SelectedItem?.ToString();
                        if (selectedValue == null)
                        {
                            selectedValue=comboBox.Text;
                            if(selectedValue == string.Empty)
                            {
                                MessageBox.Show("Error");
                                return false;
                            }
                        }
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Error");
                        return false;
                    }
                }
            }
            return true;
           
        }
        public void newValueLabel(ComboBox comboBox, string missatge)
        {
            // Get the current position of the ComboBox
            int columnIndex = Grid.GetColumn(comboBox);
            int rowIndex = Grid.GetRow(comboBox);

            if (!ExistsInGridCell(columnIndex, rowIndex, typeof(Label)))
            {
                //// Create a Label and add it to the StackPanel
                Label label = new Label();
                label.Content = "* "+missatge;
                label.Foreground = Brushes.Red;
                label.HorizontalAlignment = HorizontalAlignment.Right;
                gridConjuntValors.Children.Add(label);

                // Set the position of the StackPanel in the Grid
                Grid.SetColumn(label, columnIndex);
                Grid.SetRow(label, rowIndex);
            }
            else
            {
                EditLabelInGridCell(columnIndex, rowIndex, missatge);
            }
        }
        private bool ExistsInGridCell(int column, int row, Type element)
        {
            foreach (var child in gridConjuntValors.Children)
            {
                if(child is UIElement ui)
                {
                    if (child.GetType() == element)
                    {
                        if (Grid.GetColumn(ui) == column && Grid.GetRow(ui) == row) 
                        {
                            // Element found in the specified cell
                            return true;
                        }
                    }   
                }
            }
            // Element not found in the specified cell
            return false;
        }
        private void EditLabelInGridCell(int column, int row, string missatge)
        {
            foreach (var child in gridConjuntValors.Children)
            {
                if (child is Label lb)
                {
                    if (Grid.GetColumn(lb) == column && Grid.GetRow(lb) == row)
                    {
                        lb.Content = "* "+missatge;
                    }
                }
            }
        }


    }
}
