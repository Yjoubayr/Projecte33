using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReproductorMusicaComponentLibrary.Classes
{
    public class Music
    {
        public string Nom { get; set; }
        public List<Grup> LGrups { get; set; }
        public List<Tocar> LTocar { get; set; }
    }
}
