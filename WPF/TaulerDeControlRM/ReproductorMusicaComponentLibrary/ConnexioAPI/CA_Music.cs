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
    public class CA_Music
    {
        // Obtenim el nom del controlador des del fitxer App.config
        public static string controller = ConfigurationManager.AppSettings["controllerMusic"];

        /// <summary>
        /// Fer una consulta a l'API que retorni tots les Musics
        /// </summary>
        /// <returns>Un llistat de tots els Musics</returns>
        public static async Task<List<Music>> GetMusicsAsync()
        {
            string apiUrl = CA.baseApi + controller + "getMusics";

            string result = await CA.GetDataFromApiAsync(apiUrl);
            var data = JsonConvert.DeserializeObject<List<Music>>(result);
            return data;
        }

        /// <summary>
        /// Fer una consulta l'API que retorni un Music en concret
        /// </summary>
        /// <param name="nomMusic">Nom del Music a obtenir</param>
        /// <returns>L'objecte del Music a obtenir</returns>
        public static async Task<Music> GetMusicAsync(string nomMusic)
        {
            string apiUrl = CA.baseApi + controller + "getMusic/" + nomMusic;

            string result = await CA.GetDataFromApiAsync(apiUrl);
            var data = JsonConvert.DeserializeObject<Music>(result);
            return data;
        }

        /// <summary>
        /// Fer una consulta a l'API per inserir un Music
        /// </summary>
        /// <param name="m">L'objecte del Music a inserir</param>
        /// <returns>Verificació de que el Music s'ha inserit correctament</returns>
        public static async Task PostMusicAsync(Music m)
        {
            string jsonData = JsonConvert.SerializeObject(m);
            string apiUrl = CA.baseApi + controller + "postMusic";

            await CA.PostDataAsync(apiUrl, jsonData);
        }
    }
}
