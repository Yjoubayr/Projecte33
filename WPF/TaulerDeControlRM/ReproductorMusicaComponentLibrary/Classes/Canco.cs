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
        public int Any { get; set; }
        public List<Llista> LLlistes { get; set; } = new List<Llista>();
        public List<Canco> LVersions { get; set; } = new List<Canco>();
        public List<Album> LAlbums { get; set; } = new List<Album>();
        public List<Tocar> LTocar { get; set; } = new List<Tocar>();
        public List<Extensio> LExtensions { get; set; } = new List<Extensio>();
    }
}
