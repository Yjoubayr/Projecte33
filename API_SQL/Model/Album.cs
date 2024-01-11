using System.ComponentModel.DataAnnotations;

namespace dymj.ReproductorMusica {
    public partial class Album {
        [MaxLength(20)]
        public string Titol { get; set; }
        public int Any { get; set; }
        public string NomLlista { get; set; }
        public string NomDispositiu { get; set; }

        public ICollection<Canco>? LCancons { get; set; }
    }
}