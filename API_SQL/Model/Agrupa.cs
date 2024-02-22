using System.ComponentModel.DataAnnotations;

namespace dymj.ReproductorMusica.API_SQL.Model {
public class Agrupa
{
    public DateTime DataIncorporacio { get; set; }
    public int IDGrup { get; set; }
    public int IDMusic { get; set; }
    public Grup GrupObj { get; set; }
    public Music MusicObj { get; set; }
}
}