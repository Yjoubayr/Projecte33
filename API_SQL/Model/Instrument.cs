using System.ComponentModel.DataAnnotations;

namespace dymj.ReproductorMusica.API_SQL.Model {
    public partial class Instrument {
        [Key]
        public string Nom {get; set;}
        public string? Tipus {get; set;}
        public ICollection<Tocar>? LTocar { get; set; } = new List<Tocar>();
    }
}