using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReproductorMusicaComponentLibrary.Classes
{
    public class Instrument
    {
        public string Nom { get; set; }
        public string? Tipus { get; set; }
        public List<Tocar> LTocar { get; set; } = new List<Tocar>();
    }
}
