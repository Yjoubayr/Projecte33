using Newtonsoft.Json;
using Newtonsoft.Json.Bson;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Printing;
using System.Security.Policy;
using System.Text;
using System.Text.Json.Nodes;
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
        private string url = "http://172.23.2.141:5050/api/v1/Canco/";

        public class Item
        {
            public string? MAC { get; set; }
            public string? Name { get; set; }
            public string? Data { get; set; }
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

            if (InputID.Text != null /*&& comprovacio.Length == 3*/)
            {
                try
                {
                    List<Item> items = await PeticioGET(InputID.Text);
                    foreach (Item item in items)
                    {
                        FicarItem(item.MAC, item.Name, item.Data);
                    }
                    songListView.ItemsSource = items;

                }
                catch (Exception ex)
                {
                    throw new Exception("Error en la peticio GET: " + ex.Message);
                }


            }
        }
        private void FicarItem(string? MAC, string? name, string? data)
        {
            if (MAC != null && name != null && data != null)
            {
                items.Add(new Item { MAC = MAC, Name = name, Data = data });
            }
        }


        public async Task<List<Item>> PeticioGET(String ID)
        {
            url = url + ID;
            List<Item> items = new List<Item>();
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    HttpResponseMessage response = await httpClient.GetAsync(url);
                    if (response.IsSuccessStatusCode)
                    {
                        string jsonContent = await response.Content.ReadAsStringAsync();
                        List<Item> itemList = JsonConvert.DeserializeObject<List<Item>>(jsonContent);
                        items.AddRange(itemList);
                        return items;
                    }
                    else
                    {
                        MessageBox.Show(response.ToString());
                    }
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Error en la peticio GET: " + ex.Message);
            }
            return items;
        }
    }
}
