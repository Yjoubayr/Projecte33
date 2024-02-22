using System.ComponentModel.DataAnnotations;

namespace dymj.ReproductorMusica.API_SQL.Model {
    public partial class Agrupa {
        public DateTime DataIncorporacio {get; set;}
        public ICollection<Grup>? LGrups {get; set;} = new List<Grup>();
        public ICollection<Music>? LMusics {get; set;} = new List<Music>();
    }
}