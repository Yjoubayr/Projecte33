using Newtonsoft.Json;
using ReproductorMusicaComponentLibrary.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReproductorMusicaComponentLibrary.ConnexioAPI
{
    public class CA_Grup
    {
        public static string controller = "Grup/";
        //GETS
        public static async Task<List<Grup>> GetCanconsAsync()
        {
            string apiUrl = CA.baseApi + controller + "getGrups";

            string result = await CA.GetDataFromApiAsync(apiUrl);
            var data = JsonConvert.DeserializeObject<List<Grup>>(result);
            return data;
        }
        public static async Task<Grup> GetCancoAsync(string Nom)
        {
            string apiUrl = CA.baseApi + controller + "getGrup/" + Nom;

            string result = await CA.GetDataFromApiAsync(apiUrl);
            var data = JsonConvert.DeserializeObject<Grup>(result);
            return data;
        }

        //POST
        public static async Task PostCancoAsync(Grup g)
        {
            string jsonData = JsonConvert.SerializeObject(g);
            string apiUrl = CA.baseApi + controller + "postGrup";

            await CA.PostDataAsync(apiUrl, jsonData);

        }
    }
}
