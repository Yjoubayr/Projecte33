using System.ComponentModel.DataAnnotations;

namespace dymj.ReproductorMusica.API_SQL 
{
    public class Instrument
    {
        [Key]
        public string Nom { get; set; }
        public string Tipus { get; set; }
    }
}