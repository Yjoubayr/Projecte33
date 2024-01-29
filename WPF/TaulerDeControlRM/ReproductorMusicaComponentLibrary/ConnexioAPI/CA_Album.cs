using Newtonsoft.Json;
using ReproductorMusicaComponentLibrary.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace ReproductorMusicaComponentLibrary.ConnexioAPI
{
    public class CA_Album
    {
        // Obtenim el nom del controlador des del fitxer App.config
        public static string controller = ConfigurationManager.AppSettings["controllerAlbum"];

        /// <summary>
        /// Fer una consulta a l'API que retorni tots els Albums
        /// </summary>
        /// <returns>Un llistat de tots els Albums</returns>
        public static async Task<List<Album>> GetAlbumsAsync()
        {
            string apiUrl = CA.baseApi + controller + "getAlbums";

            string result = await CA.GetDataFromApiAsync(apiUrl);
            var data = JsonConvert.DeserializeObject<List<Album>>(result);
            return data;
        }

        /// <summary>
        /// Fer una consulta l'API que retorni un Album en concret
        /// </summary>
        /// <param name="Titol">Titol de l'Album a obtenir</param>
        /// <param name="Any">Any de l'Album a obtenir</param>
        /// <returns>L'objecte de l'Album a obtenir</returns>
        public static async Task<Album> GetAlbumAsync(string Titol, string Any)
        {
            string apiUrl = CA.baseApi + controller + "getAlbum/" + Titol + "/" + Any;

            string result = await CA.GetDataFromApiAsync(apiUrl);
            var data = JsonConvert.DeserializeObject<Album>(result);
            return data;
        }

        /// <summary>
        /// Fer una consulta a l'API per inserir un Album
        /// </summary>
        /// <param name="a">L'objecte de l'Album a inserir</param>
        /// <returns>Verificació de que l'Album s'ha inserit correctament</returns>
        public static async Task PostAlbumAsync(Album a)
        {
            string jsonData = JsonConvert.SerializeObject(a);
            string apiUrl = CA.baseApi + controller + "postAlbum";

            await CA.PostDataAsync(apiUrl, jsonData);
        }
    }
}
