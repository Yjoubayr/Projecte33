using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using dymj.ReproductorMusica.API_SQL.Model;
using dymj.ReproductorMusica.API_SQL.Data;

namespace dymj.ReproductorMusica.API_SQL.Services;

/// <summary>
/// Classe que proporciona serveis per a la gestió de grups de música.
/// </summary>
public class GrupService
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
                            .Include(x => x.LCancons)
                            .Include(x => x.LMusics)
                            .FirstOrDefaultAsync(x => x.Nom == Nom);

    /// <summary>
    /// Crea un nou grup de música a la base de dades.
    /// </summary>
    /// <param name="newGrup">Dades del nou grup de música.</param>
    /// <returns>Verificació de que el grup de música s'ha creat correctament.</returns>
    public async Task CreateAsync(Grup newGrup) {
        await _context.Grups.AddAsync(newGrup);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Actualitza les dades d'un grup de música existent a la base de dades.
    /// </summary>
    /// <param name="Nom">Nom del grup de música a actualitzar.</param>
    /// <param name="updatedGrup">Dades actualitzades del grup de música.</param>
    /// <returns>Verificació de que el grup de música s'ha actualitzat correctament.</returns>
    public async Task UpdateAsync(string Nom, Grup updatedGrup) {
        
        if (Nom == updatedGrup.Nom)
        {
            _context.Entry(updatedGrup).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

    }

    /// <summary>
    /// Elimina un grup de música de la base de dades a partir del seu nom.
    /// </summary>
    /// <param name="Nom">Nom del grup de música a eliminar.</param>
    /// <returns>Verificació de que el grup de música s'ha eliminat correctament.</returns>
    public async Task RemoveAsync(string Nom) {
        var grup = await _context.Grups.FindAsync(Nom);
        _context.Grups.Remove(grup);
        await _context.SaveChangesAsync();
    }
}