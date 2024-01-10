using System.ComponentModel.DataAnnotations;

namespace dymj.ReproductorMusica {
    public partial class Canco {
        [Key]
        public string ID { get; set; }
        [MaxLength(20)]
        public string Nom { get; set; }
        public string NomLlista { get; set; }
        public string NomDispositiu { get; set; }

        public Album? AlbumObj { get; set; }      

    }
}