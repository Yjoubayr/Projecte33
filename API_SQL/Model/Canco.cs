using System.ComponentModel.DataAnnotations;

namespace dymj.ReproductorMusica.API_SQL.Model {
    public partial class Canco {
        [Key]
        public string IDCanco { get; set; }
        [MaxLength(20)]
        public string? Nom { get; set; }
        public int? Any { get; set; }

        public ICollection<Llista>? LListes { get; set; }
        public ICollection<Canco>? LVersions { get; set; } = new List<Canco>();
        public ICollection<Album>? LAlbums { get; set; }
        public ICollection<Tocar>? LTocar { get; set; }
        public ICollection<Extensio>? LExtensions { get; set; }
    }
}