using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using dymj.ReproductorMusica.API_SQL.Model;
using dymj.ReproductorMusica.API_SQL.Data;

namespace dymj.ReproductorMusica.API_SQL.Services;

public class LlistaServicce
{
    private readonly DataContext _context;
    public LlistaService(DataContext context)
    {
        _context = context;
    }

}
