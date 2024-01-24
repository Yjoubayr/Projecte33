using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using dymj.ReproductorMusica.API_SQL.Model;
using dymj.ReproductorMusica.API_SQL.Data;

namespace dymj.ReproductorMusica.API_SQL.Services;

public class ExtensioService
{
    private readonly DataContext _context;
    private readonly CancoService _cancoService;

    /// <summary>
    /// Constructor de la classe ExtensioService
    /// </summary>
    /// <param name="context">Contexte de dades utilitzat per a accedir a la base de dades.</param>
    public ExtensioService(DataContext context)
    {
        _context = context;
        _cancoService = new CancoService(context);
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

    public async Task AddSongAsync(string nomExtensio, string IDCanco) {
        Extensio? extensioObj = await GetAsync(nomExtensio);
        Canco? cancoObj = await _cancoService.GetAsync(IDCanco);
        extensioObj.LCancons.Add(cancoObj);
        await _context.SaveChangesAsync();
    }

}