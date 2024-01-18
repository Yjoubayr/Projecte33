using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using dymj.ReproductorMusica.API_SQL.Model;
using dymj.ReproductorMusica.API_SQL.Data;

namespace dymj.ReproductorMusica.API_SQL.Services;

/// <summary>
/// Classe que proporciona serveis per a la gestio de grups de musica.
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
    /// Obte tots els grups de musica de la base de dades.
    /// </summary>
    /// <returns>Llista de grups de musica.</returns>
    public async Task<List<Grup>> GetAsync() {
        return await _context.Grups.ToListAsync();
    }

    /// <summary>
    /// Obte un grup de musica especific a partir del seu nom.
    /// </summary>
    /// <param name="Nom">Nom del grup de musica.</param>
    /// <returns>Grup de musica corresponent al nom especificat.</returns>
    public async Task<Grup?> GetAsync(string Nom) =>
        await _context.Grups
                            .Include(x => x.LCancons)
                            .Include(x => x.LMusics)
                            .FirstOrDefaultAsync(x => x.Nom == Nom);

    /// <summary>
    /// Crea un nou grup de musica a la base de dades.
    /// </summary>
    /// <param name="newGrup">L'objecte del nou grup de musica.</param>
    /// <returns>Verificacio de que el grup de musica s'ha creat correctament.</returns>
    public async Task CreateAsync(Grup newGrup) {
        await _context.Grups.AddAsync(newGrup);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Actualitza les dades d'un grup de musica existent a la base de dades.
    /// </summary>
    /// <param name="updatedGrup">Dades actualitzades del grup de musica.</param>
    /// <returns>Verificacio de que el grup de musica s'ha actualitzat correctament.</returns>
    public async Task UpdateAsync(Grup updatedGrup) {
        _context.Entry(updatedGrup).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Elimina un grup de musica de la base de dades a partir del seu nom.
    /// </summary>
    /// <param name="grup">L'objecte del grup de musica a eliminar.</param>
    /// <returns>Verificacio de que el grup de musica s'ha eliminat correctament.</returns>
    public async Task RemoveAsync(Grup grup) {
        _context.Grups.Remove(grup);
        await _context.SaveChangesAsync();
    }
}