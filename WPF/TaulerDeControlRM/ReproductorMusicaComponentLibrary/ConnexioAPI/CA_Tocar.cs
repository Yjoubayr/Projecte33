using Newtonsoft.Json;
using ReproductorMusicaComponentLibrary.Classes;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReproductorMusicaComponentLibrary.ConnexioAPI
{
    public class CA_Tocar
    {
        // Obtenim el nom del controlador des del fitxer App.config
        public static string controller = ConfigurationManager.AppSettings["controllerTocar"];

        /// <summary>
        /// Fer una consulta a l'API que retorni tots els registres
        /// de Tocar
        /// </summary>
        /// <returns>Un llistat de tots els registres de Tocar</returns>
        public static async Task<List<Tocar>> GetAllTocarAsync()
        {
            string apiUrl = CA.baseApi + controller + "getAllTocar";

            string result = await CA.GetDataFromApiAsync(apiUrl);
            var data = JsonConvert.DeserializeObject<List<Tocar>>(result);
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
        /// Fer una consulta a l'API per inserir un objecte de la classe Tocar
        /// </summary>
        /// <param name="t">L'objecte de la classe Tocar a inserir</param>
        /// <returns>Verificació de que l'objecte de la classe Tocar s'ha inserit correctament</returns>
        public static async Task PostTocarAsync(Tocar t)
        {
            string jsonData = JsonConvert.SerializeObject(t);
            string apiUrl = CA.baseApi + controller + "postTocar";

            await CA.PostDataAsync(apiUrl, jsonData);
        }
    }
}
