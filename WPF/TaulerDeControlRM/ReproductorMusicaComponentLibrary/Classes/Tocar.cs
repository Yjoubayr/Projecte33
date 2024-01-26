using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReproductorMusicaComponentLibrary.Classes
{
    public class Tocar
    {
        public string IDCanco { get; set; }
        public Canco CancoObj { get; set; }
        public string NomMusic {  get; set; }
        public Music MusicObj { get; set; }
        public string NomGrup { get; set; }
        public Grup GrupObj { get; set; }
        public string NomInstrument { get; set; }
        public Instrument InstrumentObj { get; set; }
    }
}
