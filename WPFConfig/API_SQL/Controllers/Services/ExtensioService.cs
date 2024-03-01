using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using dymj.ReproductorMusica.API_SQL.Model;
using dymj.ReproductorMusica.API_SQL.Data;

namespace dymj.ReproductorMusica.API_SQL.Services;

public class ExtensioService
{
    private readonly DataContext _context;

    /// <summary>
    /// Constructor de la classe ExtensioService
    /// </summary>
    /// <param name="context">Contexte de dades utilitzat per a accedir a la base de dades.</param>
    public ExtensioService(DataContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Per obtenir totes les extensios
    /// </summary>
    /// <returns>El llistat de Extensions</returns>
    public async Task<List<Extensio>> GetAsync() {
        return await _context.Extensions.ToListAsync();
    }

    /// <summary>
    /// Per obtenir una Extensio
    /// </summary>
    /// <param name="Nom">Nom de la Extensio a obtenir</param>
    /// <returns>L'objecte de la Extensio</returns>
    public async Task<Extensio?> GetAsync(string Nom) =>
        await _context.Extensions
                            .Include(x => x.LCancons)
                            .FirstOrDefaultAsync(x => x.Nom == Nom);

    /// <summary>
    /// Per crear una Extensio
    /// </summary>
    /// <param name="newExtensio">L'objecte de la Extensio a crear</param>
    /// <returns>Verificacio de que la Extensio s'ha creat correctament</returns>
    public async Task CreateAsync(Extensio newExtensio) {
        await _context.Extensions.AddAsync(newExtensio);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Accedeix a la ruta /api/Extensio/updateCanco/{IDCanco} dins de ExtensioController per modificar una Canco
    /// </summary>
    /// <param name="cancoOriginal">L'objecte de la Canco original que volem modificar</param>
    /// <param name="updatedCanco">L'objecte de la Canco amb els elements modificats</param>
    /// <returns>Verificacio de que la Canco s'ha modificat correctament</returns>
    public async Task UpdateCancoRemoveAsync(Canco cancoOriginal, Canco updatedCanco) {
        
        List<Extensio> lExtensions = cancoOriginal.LExtensions.ToList<Extensio>();

        foreach (var extensio in lExtensions) {
            if (!updatedCanco.LExtensions.Contains(extensio)) {
                await RemoveCancoAsync(extensio.Nom, cancoOriginal);
            }
        }
    }

    /// <summary>
    /// Accedeix a la ruta /api/Extensio/updateCanco/{IDCanco} dins de ExtensioController per modificar 
    /// </summary>
    /// <param name="cancoOriginal">L'objecte de la Canco original que volem modificar</param>
    /// <param name="updatedCanco">L'objecte de la Canco amb els elements modificats</param>
    /// <returns>Verificacio de que la Canco s'ha modificat correctament</returns>
    public async Task UpdateCancoAddAsync(Canco cancoOriginal, Canco updatedCanco) {
        
        List<Extensio> lExtensions = updatedCanco.LExtensions.ToList<Extensio>();
        
        foreach (var extensio in lExtensions) {
            if (!cancoOriginal.LExtensions.Contains(extensio)) {
                await AddCancoAsync(extensio.Nom, cancoOriginal);
            }
        }
    }

    /// <summary>
    /// Per afegir una Canco a la llista de Cancons d'una Extensio
    /// </summary>
    /// <param name="nomExtensio">Nom de la Extensio de la qual volem afegir la canco</param>
    /// <param name="canco">L'objecte de la Canco a afegir</param>
    /// <returns>Verificacio de que la Canco s'ha afegit correctament al llistat de Cancons de l'Extensio</returns>
    public async Task AddCancoAsync(string nomExtensio, Canco canco) {
        Extensio? extensio = await GetAsync(nomExtensio);
        
        if (extensio == null) {
            extensio = new Extensio() {
                Nom = nomExtensio
            };
            await CreateAsync(extensio);
        }

        if (extensio.LCancons == null) extensio.LCancons = new List<Canco>();

        extensio.LCancons.Add(canco);
        _context.Entry(extensio).State = EntityState.Modified;
        
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Per eliminar una Canco de la llista de Cancons d'una Extensio
    /// </summary>
    /// <param name="nomExtensio">Nom de la Extensio de la qual volem eliminar la canco</param>
    /// <param name="canco">L'objecte de la Canco a eliminar de la llista</param>
    /// <returns>Verificacio de que la Canco s'ha eliminat correctament del llistat de Cancons de l'Extensio</returns>
    public async Task RemoveCancoAsync(string nomExtensio, Canco canco) {
        Extensio? extensio = await GetAsync(nomExtensio);

        extensio.LCancons.Remove(canco);
        _context.Entry(extensio).State = EntityState.Modified;
        
        await _context.SaveChangesAsync();
    }
}