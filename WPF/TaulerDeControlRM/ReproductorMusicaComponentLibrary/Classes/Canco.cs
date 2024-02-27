using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReproductorMusicaComponentLibrary.Classes
{
    public class Canco
    {
        public string IDCanco { get; set; }
        public string Nom { get; set; }
        public int? Any { get; set; }
        public string? Estat { get; set; }
        public List<Llista> Llistes { get; set; }
        public List<Canco> LVersions { get; set; }
        public List<Album> LAlbums { get; set; }
        public List<Tocar> LTocar { get; set; }
        public List<Extensio> LExtensions { get; set; }

    }
}
