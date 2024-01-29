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
    /// Tambe crearem un objecte de la classe ExtensioService passant-li el contexte de dades
    /// </summary>
    /// <param name="context">Contexte de dades utilitzat per a accedir a la base de dades.</param>
    public CancoService(DataContext context)
    {
        _context = context;
        _extensioService = new ExtensioService(context);
    }

    /// <summary>
    /// Accedeix a la ruta /api/Canco/getCancons dins de CancoController per obtenir totes les cancons
    /// </summary>
    /// <returns>El llistat de Cancons</returns>
    public async Task<List<Canco>> GetAsync() {
        return await _context.Cancons
                            .Include(x => x.LListes)
                            .Include(x => x.LTocar).ToListAsync();
    }
    
    /// <summary>
    /// Accedeix a la ruta /api/Canco/getCanco/{IDCanco} dins de CancoController per obtenir una Canco
    /// </summary>
    /// <param name="IDCanco">Identificador de la Canco a obtenir</param>
    /// <returns>L'objecte de la Canco</returns>
    public async Task<List<Canco>> GetAsync(string IDCanco) =>
        await _context.Cancons
                            .Include(x => x.LExtensions)
                            .Include(x => x.LListes)
                            .Include(x => x.LTocar)
                            .Where(x => x.IDCanco == IDCanco).ToListAsync();
    /*public async Task<Canco?> GetAsync(string IDCanco) =>
        await _context.Cancons
                            .Include(x => x.LExtensions)
                            .Include(x => x.LListes)
                            .Include(x => x.LTocar)
                            .FirstOrDefaultAsync(x => x.IDCanco == IDCanco);*/

    /// <summary>
    /// Accedeix a la ruta /api/Canco/postCanco dins de CancoController per crear una Canco
    /// </summary>
    /// <param name="newCanco">L'objecte de la Canco a crear</param>
    /// <returns>Verificacio de que la Canco s'ha creat correctament</returns>
    public async Task CreateAsync(Canco newCanco) {
        newCanco.IDCanco = Guid.NewGuid().ToString();
        await _context.Cancons.AddAsync(newCanco);

        await _context.SaveChangesAsync();
    }
   

    /// <summary>
    /// Accedeix a la ruta /api/Canco/putCanco/{IDCanco} dins de CancoController per modificar una Canco
    /// </summary>
    /// <param name="cancoOriginal">L'objecte de la Canco original que volem modificar</param>
    /// <param name="updatedCanco">L'objecte de la Canco amb els elements modificats</param>
    /// <returns>Verificacio de que la Canco s'ha modificat correctament</returns>
    public async Task UpdateAsync(Canco cancoOriginal, Canco updatedCanco) {
        cancoOriginal.Nom = updatedCanco.Nom;
        cancoOriginal.Any = updatedCanco.Any;
        cancoOriginal.Estat = updatedCanco.Estat;

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