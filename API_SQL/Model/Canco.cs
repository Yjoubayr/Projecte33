using System.ComponentModel.DataAnnotations;

namespace dymj.ReproductorMusica.API_SQL 
{
    public class Canco
    {
        [Key]
        public int IdCanco { get; set; }
        public string Nom { get; set; }
        public string Imagen { get; set; }
    }
}