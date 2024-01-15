using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using dymj.ReproductorMusica.API_SQL.Model;
using dymj.ReproductorMusica.API_SQL.Data;

namespace dymj.ReproductorMusica.API_SQL.Services;

/// <summary>
/// Classe que proporciona serveis per a la gestió de grups de música.
/// </summary>
public class GrupServei
{
    private readonly DataContext _context;

    /// <summary>
    /// Constructor de la classe GrupServei.
    /// </summary>
    /// <param name="context">Contexte de dades utilitzat per a accedir a la base de dades.</param>
    public GrupService(DataContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Obté tots els grups de música de la base de dades.
    /// </summary>
    /// <returns>Llista de grups de música.</returns>
    public async Task<List<Grup>> GetAsync() {
        return await _context.Grups.ToListAsync();
    }

    /// <summary>
    /// Obté un grup de música específic a partir del seu nom.
    /// </summary>
    /// <param name="Nom">Nom del grup de música.</param>
    /// <returns>Grup de música corresponent al nom especificat.</returns>
    public async Task<Grup?> GetAsync(string Nom) =>
        await _context.Grups
                            .Include(x => x.LListes)
                            .Include(x => x.LAlbums)
                            .Include(x => x.LMusics)
                            .Include(x => x.LGrups)
                            .Include(x => x.LExtensions)
                            .FirstOrDefaultAsync(x => x.Nom == Nom);

    /// <summary>
    /// Crea un nou grup de música a la base de dades.
    /// </summary>
    /// <param name="newGrup">Dades del nou grup de música.</param>
    public async Task CreateAsync(Grup newGrup) =>
        await _context.Grups.AddAsync(newGrup);

    /// <summary>
    /// Actualitza les dades d'un grup de música existent a la base de dades.
    /// </summary>
    /// <param name="Nom">Nom del grup de música a actualitzar.</param>
    /// <param name="updateGrup">Dades actualitzades del grup de música.</param>
    public async Task UpdateAsync(string Nom, Grup updateGrup) {
        
        if (Nom == updateGrup.Nom)
        {
            _context.Entry(updateGrup).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

    }

    /// <summary>
    /// Elimina un grup de música de la base de dades a partir del seu nom.
    /// </summary>
    /// <param name="Nom">Nom del grup de música a eliminar.</param>
    public async Task RemoveAsync(string Nom) {
        var grup = await _context.Grup.FindAsync(ID);
        _context.Grup.Remove(grup);
        await _context.SaveChangesAsync();
    }
}