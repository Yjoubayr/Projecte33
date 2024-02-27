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
        /// Fer una consulta a l'API que retorni el llistat de tots els
        /// Titols de tots els Albums
        /// </summary>
        /// <returns>El llistat de tots els Titols dels Albums</returns>
        public static async Task<List<string>> GetTitlesAlbumsAync()
        {
            string apiUrl = CA.baseApi + controller + "getTitlesAlbums";

            string result = await CA.GetDataFromApiAsync(apiUrl);
            var data = JsonConvert.DeserializeObject<List<string>>(result);
            return data;
        }

        /// <summary>
        /// Fer una consulta a l'API que retorni el llistat d'Anys d'un Album
        /// segons el seu Titol
        /// </summary>
        /// <param name="Titol">Titol de l'Album del qual obtenir els anys</param>
        /// <returns>El llistat de tots els anys d'un Album</returns>
        public static async Task<List<string>> GetAnysAlbumAsync(string Titol)
        {
            string apiUrl = CA.baseApi + controller + "getAnysAlbum/" + Titol;

            string result = await CA.GetDataFromApiAsync(apiUrl);
            var data = JsonConvert.DeserializeObject<List<string>>(result);
            return data;
        }


        /// <summary>
        /// Fer una consulta l'API que retorni un llistat d'Albums
        /// amb un Titol i Any en concret
        /// </summary>
        /// <param name="Titol">Titol de l'Album o Albums a obtenir</param>
        /// <param name="Any">Any de l'Album o Albums a obtenir</param>
        /// <returns>Llista dels Albums obtingus</returns>
        public static async Task<List<Album>> GetAlbumsByTitolAndAnyAsync(string Titol, string Any)
        {
            string apiUrl = CA.baseApi + controller + "getAlbumsByTitolAndAny/" + Titol + "/" + Any;

            string result = await CA.GetDataFromApiAsync(apiUrl);
            var data = JsonConvert.DeserializeObject<List<Album>>(result);
            return data;
        }

        /// <summary>
        /// Get album by titol and any as an asynchronous operation.
        /// </summary>
        /// <param name="Titol">The titol.</param>
        /// <param name="Any">Any.</param>
        /// <returns>A Task&lt;Album&gt; representing the asynchronous operation.</returns>
        public static async Task<Album> GetAlbumByTitolAndAnyAsync(string Titol, string Any)
        {
            string apiUrl = CA.baseApi + controller + "getAlbumByTitolAndAny/" + Titol + "/" + Any;

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

        /// <summary>
        /// Fer una consulta a l'API per eliminar un Album
        /// </summary>
        /// <param name="Titol">Titol de l'Album a eliminar</param>
        /// <param name="Any">Any de l'Album a eliminar</param>
        /// <param name="IDCanco">ID de la Canco de l'Album a eliminar</param>
        /// <returns>Verificació de que l'Album s'ha eliminat correctament</returns>
        public static async Task DeleteAlbumAsync(string Titol, int? Any, string IDCanco)
        {
            string apiUrl = CA.baseApi + controller + "deleteAlbum/" + Titol + "/" + Any + "/" + IDCanco;

            await CA.DeleteDataFromApiAsync(apiUrl);
        }
    }
}
