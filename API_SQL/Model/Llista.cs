using System.ComponentModel.DataAnnotations;

namespace dymj.ReproductorMusica {
    public partial class Llista {
        public string MACAddress {get; set;}
        public string NomLlista {get; set;}
        public string NomDispositiu {get; set;}
        public ICollection<Canco>? LCancons { get; set; }
    }
}