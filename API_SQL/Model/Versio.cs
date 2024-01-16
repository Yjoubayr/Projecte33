using System.ComponentModel.DataAnnotations;
using dymj.ReproductorMusica.API_SQL.Model;
using dymj.ReproductorMusica.API_SQL.Data;

namespace dymj.ReproductorMusica.API_SQL.Model {
    public partial class Versio {
        public string IDCanco {get; set;}
        public string NomVersio {get; set;}
    }
}