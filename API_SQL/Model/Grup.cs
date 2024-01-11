using System.ComponentModel.DataAnnotations;

namespace dymj.ReproductorMusica.API_SQL.Model {
    public partial class Grup {
        [Key]
        public string Nom {get; set;}
        public ICollection<Music>? LMusics {get; set;}
        public ICollection<Canco>? LCancons { get; set; }
    }
}