using System.ComponentModel.DataAnnotations;

namespace dymj.ReproductorMusica.API_SQL 
{
    public class LlistaReproduccio
    {
        [Key]
        public string Nom { get; set; }
        public string Dispositiu { get; set; }
    }
}