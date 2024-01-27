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
    public class CA_Extensio
    {
        // Obtenim el nom del controlador des del fitxer App.config
        public static string controller = ConfigurationManager.AppSettings["controllerExtensio"];

        /// <summary>
        /// Fer una consulta a l'API que retorni totes les Extensions
        /// </summary>
        /// <returns>Un llistat de totes les Extensions</returns>
        public static async Task<List<Extensio>> GetExtensionsAsync()
        {
            string apiUrl = CA.baseApi + controller + "getExtensions";

            string result = await CA.GetDataFromApiAsync(apiUrl);
            var data = JsonConvert.DeserializeObject<List<Extensio>>(result);
            return data;
        }

        /// <summary>
        /// Fer una consulta l'API que retorni una Extensio en concret
        /// </summary>
        /// <param name="nom">Nom de l'Extensio a obtenir</param>
        /// <returns>L'objecte de l'Extensio a obtenir</returns>
        public static async Task<Extensio> GetExtensioAsync(string nom)
        {
            string apiUrl = CA.baseApi + controller + "getExtensio/" + nom;

            string result = await CA.GetDataFromApiAsync(apiUrl);
            var data = JsonConvert.DeserializeObject<Extensio>(result);
            return data;
        }

        /// <summary>
        /// Fer una consulta a l'API per inserir una Extensio
        /// </summary>
        /// <param name="e">L'objecte de l'Extensio a inserir</param>
        /// <returns>Verificació de que l'Extensio s'ha inserit correctament</returns>
        public static async Task PostExtensioAsync(Extensio e)
        {
            string jsonData = JsonConvert.SerializeObject(e);
            string apiUrl = CA.baseApi + controller + "postExtensio";

            await CA.PostDataAsync(apiUrl, jsonData);

        }

    }
}
