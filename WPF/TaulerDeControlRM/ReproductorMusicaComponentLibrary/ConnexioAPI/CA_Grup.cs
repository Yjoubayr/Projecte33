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
    public class CA_Grup
    {
        // Obtenim el nom del controlador des del fitxer App.config
        public static string controller = ConfigurationManager.AppSettings["controllerGrup"];

        /// <summary>
        /// Fer una consulta a l'API que retorni tots els Grups
        /// </summary>
        /// <returns>Un llistat de tots els Grups</returns>
        public static async Task<List<Grup>> GetGrupsAsync()
        {
            string apiUrl = CA.baseApi + controller + "getGrups";

            string result = await CA.GetDataFromApiAsync(apiUrl);
            var data = JsonConvert.DeserializeObject<List<Grup>>(result);
            return data;
        }

        /// <summary>
        /// Fer una consulta l'API que retorni un Grup en concret
        /// </summary>
        /// <param name="nom">El Nom del Grup a obtenir</param>
        /// <returns></returns>
        public static async Task<Grup> GetGrupAsync(string nom)
        {
            string apiUrl = CA.baseApi + controller + "getGrup/" + nom;

            string result = await CA.GetDataFromApiAsync(apiUrl);
            var data = JsonConvert.DeserializeObject<Grup>(result);
            return data;
        }

        /// <summary>
        /// Fer una consulta a l'API per inserir un Grup
        /// </summary>
        /// <param name="g">L'objecte del Grup a inserir</param>
        /// <returns>L'objecte del Grup a obtenir</returns>
        public static async Task PostGrupAsync(Grup g)
        {
            string jsonData = JsonConvert.SerializeObject(g);
            string apiUrl = CA.baseApi + controller + "postGrup";

            await CA.PostDataAsync(apiUrl, jsonData);

        }
    }
}
