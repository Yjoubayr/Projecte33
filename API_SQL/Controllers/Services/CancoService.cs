using dymj.ReproductorMusica.API_SQL.Model;
using Microsoft.Extensions.Options;

namespace dymj.ReproductorMusica.API_SQL.Services;

public class CancoService
{
    private readonly DataContext _context;
    
    public CancoService(Cancons context)
    {
        _context = context;
    }

    public async Task<List<Canco>> GetAsync(int start,int limit) =>
        await _context.Cancons.Find(_ => true).Skip(start).Limit(limit).ToListAsync();
        
    public async Task<Canco?> GetAsync(string ID) =>
        await _context.Cancons.Find(x => x.ID == ID).FirstOrDefaultAsync();

    public async Task CreateAsync(Canco newCanco) =>
        await _context.Cancons.InsertOneAsync(newCanco);

    public async Task UpdateAsync(string ID, Canco updatedCanco) =>
        await _context.Cancons.ReplaceOneAsync(x => x.ID == ID, updatedCanco);

    public async Task RemoveAsync(string ID) =>
        await _context.Cancons.DeleteOneAsync(x => x.ID == ID);

}