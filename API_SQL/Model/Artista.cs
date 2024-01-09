using System.ComponentModel.DataAnnotations;

namespace dymj.ReproductorMusica.API_SQL 
{
    public class Artista
    {
        [Key]
        public int IdArtista { get; set; }
        public string Nombre { get; set; }
        public string Imagen { get; set; }
    }
}