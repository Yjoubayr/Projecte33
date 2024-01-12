using System.ComponentModel.DataAnnotations;

namespace dymj.ReproductorMusica.API_SQL.Model {
    public partial class Participa {
        public string IDCanco {get; set;}
        public string NomMusic {get; set;}
        public string NomGrup {get; set;}
        public string NomInstrument {get; set;}
        public ICollection<Music>? LMusics{get; set;}
        public ICollection<Grup>? LGrups{get; set;}
        public ICollection<Canco>? LCancons { get; set; }
        public ICollection<Instrument>? LInstruments {get; set;}
    }
}