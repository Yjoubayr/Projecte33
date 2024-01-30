using System.ComponentModel.DataAnnotations;

namespace dymj.ReproductorMusica.API_SQL.Model {
    public partial class Album {
        [MaxLength(20)]
        public string Titol { get; set; }
        public int Any { get; set; }
        public string IDCanco { get; set; }
        public Canco? CancoObj {get; set;}
    }
}