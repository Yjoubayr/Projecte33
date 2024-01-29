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
    public class CA_Instrument
    {
        // Obtenim el nom del controlador des del fitxer App.config
        public static string controller = ConfigurationManager.AppSettings["controllerInstrument"];

        /// <summary>
        /// Fer una consulta a l'API que retorni tots les Instruments
        /// </summary>
        /// <returns>Un llistat de totes les Instruments</returns>
        public static async Task<List<Instrument>> GetInstrumentsAsync()
        {
            string apiUrl = CA.baseApi + controller + "getInstruments";

            string result = await CA.GetDataFromApiAsync(apiUrl);
            var data = JsonConvert.DeserializeObject<List<Instrument>>(result);
            return data;
        }
    }
}
