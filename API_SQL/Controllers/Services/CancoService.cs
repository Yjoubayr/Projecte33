using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using dymj.ReproductorMusica.API_SQL.Model;
using dymj.ReproductorMusica.API_SQL.Data;

namespace dymj.ReproductorMusica.API_SQL.Services;

public class CancoService
{
    private readonly DataContext _context;
    private readonly ExtensioService _extensioService;

    /// <summary>
    /// Constructor de la classe CancoService
    /// </summary>
    /// <param name="context">Contexte de dades utilitzat per a accedir a la base de dades.</param>
    public CancoService(DataContext context, ExtensioService extensioService)
    {
        _context = context;
        _extensioService = extensioService;
    }

    /// <summary>
    /// Accedeix a la ruta /api/Canco/getCancons dins de CancoController per obtenir totes les cancons
    /// </summary>
    /// <returns>El llistat de Cancons</returns>
    public async Task<List<Canco>> GetAsync() {
        return await _context.Cancons.ToListAsync();
    }
    
    /// <summary>
    /// Accedeix a la ruta /api/Canco/getCanco/{IDCanco} dins de CancoController per obtenir una Canco
    /// </summary>
    /// <param name="IDCanco">Identificador de la Canco a obtenir</param>
    /// <returns>L'objecte de la Canco</returns>
    public async Task<Canco?> GetAsync(string IDCanco) =>
        await _context.Cancons
                            .Include(x => x.LListes)
                            .Include(x => x.LTocar)
                            .FirstOrDefaultAsync(x => x.IDCanco == IDCanco);

    /// <summary>
    /// Accedeix a la ruta /api/Canco/postCanco dins de CancoController per crear una Canco
    /// </summary>
    /// <param name="newCanco">L'objecte de la Canco a crear</param>
    /// <returns>Verificacio de que la Canco s'ha creat correctament</returns>
    public async Task CreateAsync(Canco newCanco) {
        newCanco.IDCanco = Guid.NewGuid().ToString();
        await _context.Cancons.AddAsync(newCanco);

        // Afegim una nova extensio si no existeix, 
        // obtenint-la del nom de la canco
        string[] cancoSeparada = newCanco.Nom.Split('.');
        string nomExtensio = cancoSeparada[cancoSeparada.Length - 1];
        Extensio? extensio = await _extensioService.GetAsync(nomExtensio);

        if (extensio == null) {
            extensio = new Extensio();
            extensio.Nom = nomExtensio;
            await _extensioService.CreateAsync(extensio);
        }
        
        await _context.SaveChangesAsync();
    }
   

    /// <summary>
    /// Accedeix a la ruta /api/Canco/putCanco/{IDCanco} dins de CancoController per modificar una Canco
    /// </summary>
    /// <param name="updatedCanco">L'objecte de la Canco a modificar</param>
    /// <returns>Verificacio de que la Canco s'ha modificat correctament</returns>
    public async Task UpdateAsync(Canco updatedCanco) {
        var cancoOriginal = await GetAsync(updatedCanco.IDCanco);
        _context.Entry(cancoOriginal).CurrentValues.SetValues(updatedCanco);

        // Afegim una nova extensio si no existeix, 
        // obtenint-la del nom de la canco modificada
        string[] cancoSeparada = updatedCanco.Nom.Split('.');
        string nomExtensio = cancoSeparada[cancoSeparada.Length - 1];
        Extensio? extensio = await _extensioService.GetAsync(nomExtensio);

        if (extensio == null) {
            extensio = new Extensio();
            extensio.Nom = nomExtensio;
            await _extensioService.CreateAsync(extensio);
        }

        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Accedeix a la ruta /api/Canco/deleteCanco/{IDCanco} dins de CancoController per eliminar una Canco
    /// </summary>
    /// <param name="canco">L'objecte de la Canco a eliminar</param>
    /// <returns>Verificacio de que la Canco s'ha eliminat correctament</returns>
    public async Task RemoveAsync(Canco canco) {        
        _context.Cancons.Remove(canco);
        await _context.SaveChangesAsync();
    }
}