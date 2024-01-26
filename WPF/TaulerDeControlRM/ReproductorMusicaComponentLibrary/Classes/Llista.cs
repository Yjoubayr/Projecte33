using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReproductorMusicaComponentLibrary.Classes
{
    public class Llista
    {
        public string MACAddress { get; set; }
        public string NomLlista { get; set; }
        public string? NomDispositiu { get; set; }
        public List<Canco> LCancons { get; set; }
    }
}
