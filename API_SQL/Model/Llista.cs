using System.ComponentModel.DataAnnotations;

namespace dymj.ReproductorMusica.API_SQL.Model {
    public partial class Llista {
        public string MACAddress {get; set;}
        public string NomLlista {get; set;}
        [NotRequired]
        public string NomDispositiu {get; set;}
        public ICollection<Canco>? LCancons { get; set; }
    }
}