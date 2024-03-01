using System.ComponentModel.DataAnnotations;

namespace dymj.ReproductorMusica.API_SQL.Model {
    public partial class Extensio {
        [Key]
        [MaxLength(5)]
        public string? Nom {get; set;}
        public ICollection<Canco>? LCancons { get; set; } = new List<Canco>();
    }
}