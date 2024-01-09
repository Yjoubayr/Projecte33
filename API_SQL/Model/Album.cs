using System.ComponentModel.DataAnnotations;

namespace dymj.ReproductorMusica.API_SQL 
{
    public class Album
    {
        [Key]
        public string Nom { get; set; }
        public int Any { get; set; }
        public string Imagen { get; set; }
    }
}