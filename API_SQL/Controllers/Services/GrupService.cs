using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using dymj.ReproductorMusica.API_SQL.Model;
using dymj.ReproductorMusica.API_SQL.Data;

namespace dymj.ReproductorMusica.API_SQL.Services;

public class GrupServei
{
    private readonly DataContext _context;
    public GrupService(DataContext context)
    {
        _context = context;
    }

    public async Task<List<Grup>> GetAsync() {
        return await _context.Grups.ToListAsync();
    }

    
    public async Task<Grup?> GetAsync(string Nom) =>
        await _context.Grups
                            .Include(x => x.LListes)
                            .Include(x => x.LAlbums)
                            .Include(x => x.LMusics)
                            .Include(x => x.LGrups)
                            .Include(x => x.LExtensions)
                            .FirstOrDefaultAsync(x => x.Nom == Nom);

    public async Task CreateAsync(Grup newGrup) =>
        await _context.Grups.AddAsync(newGrup);

    public async Task UpdateAsync(string Nom, Grup updateGrup) {
        
        if (Nom == updateGrup.Nom)
        {
            _context.Entry(updateGrup).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

    }

    public async Task RemoveAsync(string Nom) {
        var grup = await _context.Grup.FindAsync(ID);
        _context.Grup.Remove(grup);
        await _context.SaveChangesAsync();
    }
}