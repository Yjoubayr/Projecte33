using System.ComponentModel.DataAnnotations;

namespace dymj.ReproductorMusica.API_SQL.Model {
public class Agrupa
{
    public DateTime DataIncorporacio { get; set; }
    public string NomGrup { get; set; }
    public DateTime DataFundacioGrup { get; set; }
    public string NomMusic { get; set; }
    public Grup GrupObj { get; set; }
    public Music MusicObj { get; set; }
}
}