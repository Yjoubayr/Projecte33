using System.ComponentModel.DataAnnotations;

namespace dymj.ReproductorMusica.API_SQL.Model {
    public partial class Album {
        [MaxLength(20)]
        public string Titol { get; set; }
        [NotRequired]
        public int Any { get; set; }

        //public ICollection<Canco>? LCancons { get; set; }
    }
}