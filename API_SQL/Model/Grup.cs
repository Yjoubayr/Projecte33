using System.ComponentModel.DataAnnotations;

namespace dymj.ReproductorMusica.API_SQL.Model {
    public partial class Grup {
        [MaxLength(30)]
        public string Nom {get; set;}
        public string DataFundacio {get; set;}
        public int? Any { get; set; }
        public ICollection<Music>? LMusics {get; set;} = new List<Music>();
        public ICollection<Tocar>? LTocar { get; set; } = new List<Tocar>();
        public ICollection<Agrupa>? LAgrupa {get; set;} = new List<Agrupa>();

    }
}