using Newtonsoft.Json;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReproductorMusicaComponentLibrary.Classes
{
    public class ConfigManager
    {
        private const string ConfigFilePath = "config.json";

        public static AppConfig ObtenirConfiguracio()
        {
            try
            {
                if (File.Exists(ConfigFilePath))
                {
                    string json = File.ReadAllText(ConfigFilePath);
                    return JsonConvert.DeserializeObject<AppConfig>(json);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al llegir la configuració: {ex.Message}");
            }

            return new AppConfig { PrimeraExecucio = true };
        }

        public static void ActualitzarConfiguracio(AppConfig config)
        {
            try
            {
                string json = JsonConvert.SerializeObject(config);
                File.WriteAllText(ConfigFilePath, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al actualitzar la configuració: {ex.Message}");
            }
        }
    }
}
