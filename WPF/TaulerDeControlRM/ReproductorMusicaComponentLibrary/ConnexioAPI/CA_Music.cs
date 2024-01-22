using Newtonsoft.Json;
using ReproductorMusicaComponentLibrary.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReproductorMusicaComponentLibrary.ConnexioAPI
{
    public class CA_Music
    {
        public static string controller = "Music/";
        //GETS
        //Tots
        public static async Task<List<Music>> GetMusicsAsync()
        {
            string apiUrl = CA.baseApi + controller + "getMusics";

            string result = await CA.GetDataFromApiAsync(apiUrl);
            var data = JsonConvert.DeserializeObject<List<Music>>(result);
            return data;
        }
        public static async Task<Music> GetMusicAsync(string nomMusic)
        {
            string apiUrl = CA.baseApi + controller + "getMusic/" + nomMusic;

            string result = await CA.GetDataFromApiAsync(apiUrl);
            var data = JsonConvert.DeserializeObject<Music>(result);
            return data;
        }

        //POST
        public static async Task PostMusicAsync(Music e)
        {
            string jsonData = JsonConvert.SerializeObject(e);
            string apiUrl = CA.baseApi + controller + "postMusic";

            await CA.PostDataAsync(apiUrl, jsonData);
        }
    }
}
