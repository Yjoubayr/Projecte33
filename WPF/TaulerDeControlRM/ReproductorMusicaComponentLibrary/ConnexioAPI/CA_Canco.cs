using ReproductorMusicaComponentLibrary.ConnexioAPI;
using ReproductorMusicaComponentLibrary.Classes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReproductorMusicaComponentLibrary.ConnexioAPI
{
    public class CA_Canco
    {
        public static string controller = "Canco/";
        //GETS
        public static async Task<List<Canco>> GetCanconsAsync()
        {
            string apiUrl = CA.baseApi + controller + "getCancons";

            string result = await CA.GetDataFromApiAsync(apiUrl);
            var data = JsonConvert.DeserializeObject<List<Canco>>(result);
            return data;
        }
        public static async Task<Canco> GetCancoAsync(string IDCanco)
        {
            string apiUrl = CA.baseApi + controller + "getCanco/" + IDCanco;

            string result = await CA.GetDataFromApiAsync(apiUrl);
            var data = JsonConvert.DeserializeObject<Canco>(result);
            return data;
        }

        //POST
        public static async Task PostCancoAsync(Canco c)
        {
            string jsonData = JsonConvert.SerializeObject(c);
            string apiUrl = CA.baseApi + controller + "postCanco";

            await CA.PostDataAsync(apiUrl, jsonData);
            
        }
    }
}
