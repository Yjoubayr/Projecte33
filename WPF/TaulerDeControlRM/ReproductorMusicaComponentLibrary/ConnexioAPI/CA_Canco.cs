using ReproductorMusicaComponentLibrary.ConnexioAPI;
using ReproductorMusicaComponentLibrary.Classes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace ReproductorMusicaComponentLibrary.ConnexioAPI
{
    public class CA_Canco
    {
        // Obtenim el nom del controlador des del fitxer App.config
        public static string controller = ConfigurationManager.AppSettings["controllerCanco"];

        /// <summary>
        /// Fer una consulta a l'API que retorni totes les Cancons
        /// </summary>
        /// <returns>Un llistat de totes les Cancons</returns>
        public static async Task<List<Canco>> GetCanconsAsync()
        {
            string apiUrl = CA.baseApi + controller + "getCancons";

            string result = await CA.GetDataFromApiAsync(apiUrl);
            var data = JsonConvert.DeserializeObject<List<Canco>>(result);
            return data;
        }

        /// <summary>
        /// Fer una consulta l'API que retorni una Canco en concret
        /// </summary>
        /// <param name="IDCanco">Identificador de la Canco a obtenir</param>
        /// <returns>L'objecte de la Canco a obtenir</returns>
        public static async Task<Canco> GetCancoAsync(string IDCanco)
        {
            string apiUrl = CA.baseApi + controller + "getCanco/" + IDCanco;

            string result = await CA.GetDataFromApiAsync(apiUrl);
            var data = JsonConvert.DeserializeObject<Canco>(result);
            return data;
        }

        /// <summary>
        /// Fer una consulta a l'API per inserir una Canco
        /// </summary>
        /// <param name="c">L'objecte de la Canco a inserir</param>
        /// <returns>Verificació de que la Canco s'ha inserit correctament</returns>
        public static async Task<string> PostCancoAsync(Canco c)
        {
            string jsonData = JsonConvert.SerializeObject(c);
            string apiUrl = CA.baseApi + controller + "postCanco";

            string response=await CA.PostDataAsync(apiUrl, jsonData);
            return response;
        }
    }
}
