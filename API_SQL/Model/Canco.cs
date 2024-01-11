using System.ComponentModel.DataAnnotations;

namespace dymj.ReproductorMusica.API_SQL.Model {
    public partial class Canco {
        [Key]
        public string ID { get; set; }
        [MaxLength(20)]
        public string Nom { get; set; }
        public string NomLlista { get; set; }
        public string NomDispositiu { get; set; }

        public ICollection<Llista>? LListes { get; set; }
        public ICollection<Album>? LAlbums { get; set; }
        public ICollection<Music>? LMusics { get; set; }
        public ICollection<Grup>? LGrups { get; set; }
        public ICollection<Extensio>? LExtensions { get; set; }
    }
}