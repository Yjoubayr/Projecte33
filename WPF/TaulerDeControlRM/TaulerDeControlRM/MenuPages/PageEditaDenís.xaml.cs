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
    /// Interaction logic for PageEdita.xaml
    /// </summary>
    public partial class PageEditaDenís : Page
    {
        public PageEditaDenís()
        {
            InitializeComponent();
            elementComboBox.ItemsSource = new List<string> { "Cançó", "Grup", "Àlbum", "Llista de reproducció" };
            elementComboBox.SelectionChanged += ComboBox_SelectionChanged;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox elementComboBox = sender as ComboBox;
            string selectedItem = elementComboBox.SelectedValue.ToString();

            //FALTA ELIMINAR L'EXISTENT

            if (selectedItem != null)
            {
                // Perform different actions based on the selected value
                switch (selectedItem)
                {
                    case "Cançó":
                        //Camps de cerca de cançó
                        Camp cancoNom = new Camp("Nom Cançó");
                        cancoNom.MyList = new List<string> { "Cançó1", "Cançó2", "Cançó3" };
                        Camp cancoId = new Camp("Id");
                        cancoId.MyList = new List<string> { "Id1", "Id2", "Id3" };
                        List<Camp> campsCanco = new List<Camp> { cancoNom, cancoId };
                        ConjuntValors cvCanco = new ConjuntValors("Cançó", campsCanco);
                        GridConjuntValors gcvCanco = new GridConjuntValors(true, new List<ConjuntValors> { cvCanco });
                        spCerca.Children.Insert(1, gcvCanco);
                        break;
                    case "Grup":
                        //Camps de cerca de grup
                        Camp grupNom = new Camp("Nom Grup");
                        grupNom.MyList = new List<string> { "Grup1", "Grup2", "Grup3" };
                        List<Camp> campsGrup = new List<Camp> { grupNom };
                        ConjuntValors cvGrup = new ConjuntValors("Grup", campsGrup);
                        GridConjuntValors gcvGrup = new GridConjuntValors(true, new List<ConjuntValors> { cvGrup });
                        spCerca.Children.Insert(1, gcvGrup);
                        break;
                    case "Àlbum":
                        //Camps de cerca d'àlbum
                        Camp albumNom = new Camp("Títol Àlbum");
                        albumNom.MyList = new List<string> { "Àlbum1", "Àlbum2", "Àlbum3" };
                        Camp albumAny = new Camp("Any");
                        albumAny.MyList = new List<string> { "Any1", "Any2", "Any3" };
                        List<Camp> campsAlbum = new List<Camp> { albumNom, albumAny };
                        ConjuntValors cvAlbum = new ConjuntValors("Àlbum", campsAlbum);
                        GridConjuntValors gcvAlbum = new GridConjuntValors(true, new List<ConjuntValors> { cvAlbum });
                        spCerca.Children.Insert(1, gcvAlbum);
                        break;
                    case "Llista de reproducció":
                        //Camps de cerca de llista de reproducció
                        Camp llistaNom = new Camp("Nom Llista");
                        llistaNom.MyList = new List<string> { "Llista1", "Llista2", "Llista3" };
                        Camp MACDispositiu = new Camp("MAC Dispositiu");
                        MACDispositiu.MyList = new List<string> { "MAC1", "MAC2", "MAC3" };
                        List<Camp> campsLlista = new List<Camp> { llistaNom, MACDispositiu };
                        ConjuntValors cvLlista = new ConjuntValors("Llista de reproducció", campsLlista);
                        GridConjuntValors gcvLlista = new GridConjuntValors(true, new List<ConjuntValors> { cvLlista });
                        spCerca.Children.Insert(1, gcvLlista);
                        break;
                    default:
                        // Handle other cases
                        break;
                }

            }












        }

        private void Cerca_Click(object sender, RoutedEventArgs e)
        {
            songListView.Visibility = Visibility.Visible;
        }
    }
}
