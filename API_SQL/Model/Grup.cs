using System.ComponentModel.DataAnnotations;

namespace dymj.ReproductorMusica.API_SQL 
{
    public class Grup
    {
        [Key]
        public string Nom { get; set; }
        public int Any { get; set; }
    }
}