using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using dymj.ReproductorMusica.API_SQL.Model;
using dymj.ReproductorMusica.API_SQL.Data;

namespace dymj.ReproductorMusica.API_SQL.Services;

public class CancoService
{
    private readonly DataContext _context;
    public CancoService(DataContext context)
    {
        _context = context;
    }

    public async Task<List<Canco>> GetAsync(int start,int limit) {
        return await _context.Cancons.ToListAsync();
    }
        //await _context.Cancons.Find(c => true).Skip(start).Limit(limit).ToListAsync();

    
    public async Task<Canco?> GetAsync(string ID) =>
        await _context.Cancons
                            .Include(x => x.LListes)
                            .Include(x => x.LAlbums)
                            .Include(x => x.LMusics)
                            .Include(x => x.LGrups)
                            .Include(x => x.LExtensions)
                            .FirstOrDefaultAsync(x => x.ID == ID);

    public async Task CreateAsync(Canco newCanco) =>
        await _context.Cancons.AddAsync(newCanco);

    public async Task UpdateAsync(string ID, Canco updatedCanco) {
        
        if (ID == updatedCanco.ID)
        {
            _context.Entry(updatedCanco).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            //await _context.Cancons.ReplaceOneAsync(x => x.ID == ID, updatedCanco);
        }

    }

    public async Task RemoveAsync(string ID) {
        var canco = await _context.Cancons.FindAsync(ID);
        
        _context.Cancons.Remove(canco);
        await _context.SaveChangesAsync();
    }
}