using System.ComponentModel.DataAnnotations;

namespace dymj.ReproductorMusica {
    public partial class Music {
        [Key]
        public string Nom{get; set;}
    }
}