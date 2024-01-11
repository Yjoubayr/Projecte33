using System.ComponentModel.DataAnnotations;

namespace dymj.ReproductorMusica {
    public partial class Extensio {
        [Key]
        [MaxLength(5)]
        public string Nom {get; set;}
        public ICollection<Canco>? LCancons { get; set; }
    }
}