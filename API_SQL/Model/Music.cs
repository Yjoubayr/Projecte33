using System.ComponentModel.DataAnnotations;

namespace dymj.ReproductorMusica.API_SQL.Model {
    public partial class Music {
        [Key]
        public string Nom {get; set;}
        public ICollection<Grup>? LGrups{get; set;} = new List<Grup>();
        public ICollection<Tocar>? LTocar { get; set; } = new List<Tocar>();
    }
}