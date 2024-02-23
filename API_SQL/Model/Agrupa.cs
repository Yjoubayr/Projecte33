using System.ComponentModel.DataAnnotations;

namespace dymj.ReproductorMusica.API_SQL.Model {
public class Agrupa
{
    public DateTime DataIncorporacio { get; set; }
    public string NomGrup { get; set; }
    public DateTime DataFundacioGrup { get; set; }
    public int NomMusic { get; set; }
    public Grup GrupObj { get; set; }
    public Music MusicObj { get; set; }
}
}