using System.ComponentModel.DataAnnotations;

namespace dymj.ReproductorMusica.API_SQL.Model {
    public partial class Grup {
        [MaxLength(30)]
        public string Nom {get; set;}
        public DateTime DataFundacio {get; set;}
        public int? Any { get; set; }
        public ICollection<Tocar>? LTocar { get; set; } = new List<Tocar>();
        public ICollection<Agrupa>? LAgrupes {get; set;} = new List<Agrupa>();

    }
}