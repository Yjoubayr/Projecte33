using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using dymj.ReproductorMusica.API_SQL.Model;
using dymj.ReproductorMusica.API_SQL.Data;

namespace dymj.ReproductorMusica.API_SQL.Model {
    public partial class Tocar {
        public string IDCanco {get; set;}
        [AllowNull]
        public Canco? CancoObj {get; set;}
        
        public string NomMusic {get; set;}
        [AllowNull]
        public Music? MusicObj {get; set;}
        
        public string NomGrup {get; set;}
        [AllowNull]
        public Grup? GrupObj {get; set;}
        
        public string NomInstrument {get; set;}
        [AllowNull]
        public Instrument? InstrumentObj {get; set;}
    }
}