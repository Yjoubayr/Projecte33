using Newtonsoft.Json;
using ReproductorMusicaComponentLibrary.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReproductorMusicaComponentLibrary.ConnexioAPI
{
    public class CA_Extensio
    {
        public static string controller = "Extensio/";
        //GETS
        public static async Task<List<Canco>> GetCanconsAsync()
        {
            string apiUrl = CA.baseApi + controller + "getExtensions";

            string result = await CA.GetDataFromApiAsync(apiUrl);
            var data = JsonConvert.DeserializeObject<List<Canco>>(result);
            return data;
        }
        public static async Task<Canco> GetCancoAsync(string IDCanco)
        {
            string apiUrl = CA.baseApi + controller + "getExtensio/" + IDCanco;

            string result = await CA.GetDataFromApiAsync(apiUrl);
            var data = JsonConvert.DeserializeObject<Canco>(result);
            return data;
        }

        //POST
        public static async Task PostCancoAsync(Canco c)
        {
            string jsonData = JsonConvert.SerializeObject(c);
            string apiUrl = CA.baseApi + controller + "postExtensio";

            await CA.PostDataAsync(apiUrl, jsonData);

        }

    }
}
