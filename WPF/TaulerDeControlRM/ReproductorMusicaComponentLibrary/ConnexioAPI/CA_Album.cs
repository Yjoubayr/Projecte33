using Newtonsoft.Json;
using ReproductorMusicaComponentLibrary.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReproductorMusicaComponentLibrary.ConnexioAPI
{
    public class CA_Album
    {
        public static string controller = "Album/";
        //GETS
        public static async Task<List<Album>> GetAlbumsAsync()
        {
            string apiUrl = CA.baseApi + controller + "getAlbums";

            string result = await CA.GetDataFromApiAsync(apiUrl);
            var data = JsonConvert.DeserializeObject<List<Album>>(result);
            return data;
        }

        public static async Task<Album> GetAlbumAsync(string Titol, string Any)
        {
            string apiUrl = CA.baseApi + controller + "getAlbum/" + Titol + "/" + Any;

            string result = await CA.GetDataFromApiAsync(apiUrl);
            var data = JsonConvert.DeserializeObject<Album>(result);
            return data;
        }

        //POST
        public static async Task PostAlbumAsync(Album a)
        {
            string jsonData = JsonConvert.SerializeObject(a);
            string apiUrl = CA.baseApi + controller + "postAlbum";

            await CA.PostDataAsync(apiUrl, jsonData);
        }
    }
}
