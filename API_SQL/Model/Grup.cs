using System.ComponentModel.DataAnnotations;

namespace dymj.ReproductorMusica.API_SQL.Model {
    public partial class Grup {
        [Key]
        [MaxLength(30)]
        public string Nom {get; set;}
        public int? Any { get; set; }
        public ICollection<Music>? LMusics {get; set;} = new List<Music>();
        public ICollection<Tocar>? LTocar { get; set; } = new List<Tocar>();
    }
}