using System.ComponentModel.DataAnnotations;

namespace dymj.ReproductorMusica.API_SQL.Model {
    public partial class Instrument {
        [Key]
        public string Nom {get; set;}
        [Required]
        public string Tipus {get; set;}
        public ICollection<Tocar>? LTocar { get; set; }
    }
}