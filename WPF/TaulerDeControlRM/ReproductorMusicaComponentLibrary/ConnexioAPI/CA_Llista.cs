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
    public class CA_Llista
    {
        // Obtenim el nom del controlador des del fitxer App.config
        public static string controller = ConfigurationManager.AppSettings["controllerLlista"];

        /// <summary>
        /// Fer una consulta a l'API que retorni totes les Llistes
        /// </summary>
        /// <returns>Un llistat de totes les Cancons</returns>
        public static async Task<List<Llista>> GetLlistesAsync()
        {
            string apiUrl = CA.baseApi + controller + "getLlistes";

            string result = await CA.GetDataFromApiAsync(apiUrl);
            var data = JsonConvert.DeserializeObject<List<Llista>>(result);
            return data;
        }

        /// <summary>
        /// Fer una consulta l'API que retorni una Llista en concret
        /// </summary>
        /// <param name="NomLlista">Identificador de la Llista a obtenir</param>
        /// <param name="MACAddress">Identificador de la Llista a obtenir</param>
        /// <returns>L'objecte de la Llista a obtenir</returns>
        public static async Task<Llista> GetLlistaAsync(string NomLlista, string MACAddress)
        {
            string apiUrl = CA.baseApi + controller + "getLlista/" + MACAddress+"/"+NomLlista;

            string result = await CA.GetDataFromApiAsync(apiUrl);
            var data = JsonConvert.DeserializeObject<Llista>(result);
            return data;
        }
    }
}
