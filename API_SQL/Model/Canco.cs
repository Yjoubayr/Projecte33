using System.ComponentModel.DataAnnotations;

namespace dymj.ReproductorMusica.API_SQL.Model {
    public partial class Canco {
        [Key]
        public string? IDCanco { get; set; }
        [MaxLength(20)]
        public string? Nom { get; set; }
        public int? Any { get; set; }
        [MaxLength(10)]
        public string? Estat { get; set; } = "Incompleta";

        public ICollection<Llista>? LListes { get; set; } = new List<Llista>();
        public ICollection<Canco>? LVersions { get; set; } = new List<Canco>();
        public ICollection<Album>? LAlbums { get; set; } = new List<Album>();
        public ICollection<Tocar>? LTocar { get; set; } = new List<Tocar>();
        public ICollection<Extensio>? LExtensions { get; set; } = new List<Extensio>();
    }
}