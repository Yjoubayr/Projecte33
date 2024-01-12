using Microsoft.Extensions.Options;
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
        await _context.Cancons.ToListAsync();
    }
        //await _context.Cancons.Find(c => true).Skip(start).Limit(limit).ToListAsync();

    /*
    public async Task<Canco?> GetAsync(string ID) =>
        await _context.Cancons.Find(x => x.ID == ID).FirstOrDefaultAsync();

    public async Task CreateAsync(Canco newCanco) =>
        await _context.Cancons.InsertOneAsync(newCanco);

    public async Task UpdateAsync(string ID, Canco updatedCanco) =>
        await _context.Cancons.ReplaceOneAsync(x => x.ID == ID, updatedCanco);

    public async Task RemoveAsync(string ID) =>
        await _context.Cancons.DeleteOneAsync(x => x.ID == ID);
    */

}