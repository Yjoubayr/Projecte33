using System.ComponentModel.DataAnnotations;

namespace dymj.ReproductorMusica.API_SQL.Model {
    public partial class Music {
        [Key]
        [MaxLength(20)]
        public string Nom {get; set;}
        public ICollection<Agrupa>? LAgrupa {get; set;} = new List<Agrupa>();
        public ICollection<Tocar>? LTocar { get; set; } = new List<Tocar>();
    }
}