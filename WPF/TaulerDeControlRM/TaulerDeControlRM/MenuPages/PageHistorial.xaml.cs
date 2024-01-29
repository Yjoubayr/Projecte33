using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Printing;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static TaulerDeControlRM.PageHistorial;

namespace TaulerDeControlRM
{
    /// <summary>
    /// Interaction logic for PageHistorial.xaml
    /// </summary>
    public partial class PageHistorial : Page
    {
        public class Item
        {
            public string Nom { get; set; }
            public string Grup { get; set; }
            public string Data { get; set; }
        }

        private ObservableCollection<Item> items;
        public PageHistorial()
        {
            InitializeComponent();
            items = new ObservableCollection<Item>();
            songListView.ItemsSource = items;
            
        }
        
        public async void clickHandler(object sender, RoutedEventArgs e)
        {
            var itemsGrid = new ObservableCollection<Item>();

            songListView.ItemsSource = itemsGrid;
            String[] comprovacio = InputID.Text.Split("-");
            if (InputID.Text != null /*&& comprovacio.Length == 3*/)
            {
               try
                {
                    List<Item> items = await PeticioGET(InputID.Text);
                    foreach (Item item in items) { 
                        FicarItem(item.Nom, item.Grup, item.Data);
                    }

                }catch (Exception ex)
                {
                    throw new Exception("Error en la peticio GET 1" + ex);
                }
                
               
            }
        }
        private void FicarItem(string nom, string grup, string data)
        {
            items.Add(new Item { Nom = nom, Grup = grup, Data = data });
        }


        public async Task<List<Item>> PeticioGET(String ID)
        {
            List<Item> items = new List<Item>();
            String url = "http://localhost:5050/api/v1/Historial/" + ID;
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    HttpResponseMessage response = await httpClient.GetAsync(url);
                    if (response.IsSuccessStatusCode)
                    {
                        
                        string jsonContent = await response.Content.ReadAsStringAsync();

                        
                        items = JsonConvert.DeserializeObject<List<Item>>(jsonContent);
                        return items;
                    }
                }
                
            }catch(Exception ex)
            {
                throw new Exception("Error en la peticio GET 2" + ex.Message);
            }
            return items;
        }
    }
}
