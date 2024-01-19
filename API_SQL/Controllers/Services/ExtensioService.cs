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
    /// Accedeix a la ruta /api/Extensio/getExtensios per obtenir totes les extensios
    /// </summary>
    /// <returns>El llistat de Extensios</returns>
    public async Task<List<Extensio>> GetAsync() {
        return await _context.Extensios.ToListAsync();
    }

    /// <summary>
    /// Accedeix a la ruta /api/Extensio/getExtensio/{Nom} per obtenir una Extensio
    /// </summary>
    /// <param name="Nom">Nom de la Extensio a obtenir</param>
    /// <returns>L'objecte de la Extensio</returns>
    public async Task<Extensio?> GetAsync(string Nom) =>
        await _context.Extensios
                            .Include(x => x.LCancons)
                            .FirstOrDefaultAsync(x => x.Nom == Nom);

    /// <summary>
    /// Accedeix a la ruta /api/Extensio/postExtensio per crear una Extensio
    /// </summary>
    /// <param name="newExtensio">L'objecte de la Extensio a crear</param>
    /// <returns>Verificacio de que la Extensio s'ha creat correctament</returns>
    public async Task CreateAsync(Extensio newExtensio) {
        await _context.Extensios.AddAsync(newExtensio);
        await _context.SaveChangesAsync();
    }
}