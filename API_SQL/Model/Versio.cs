using System.ComponentModel.DataAnnotations;

namespace dymj.ReproductorMusica.API_SQL 
{
    public class Versio
    {
        [Key]
        public string Nom { get; set; }
        public int Duracio { get; set; }
        public int Any { get; set; }
    }
}