using System.ComponentModel.DataAnnotations;

namespace dymj.ReproductorMusica.API_SQL.Model {
    public partial class Grup {
        [Key]
        public string Nom {get; set;}
        [NotRequired]
        public int Any { get; set; }
        public ICollection<Music>? LMusics {get; set;}
        public ICollection<Tocar>? LTocar { get; set; }
    }
}