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
    public async Task<Canco?> GetAsync(string IDCanco) {
        List<Canco> listCancons = await _context.Cancons
                                    .Include(x => x.LExtensions)
                                    .Include(x => x.LListes)
                                    .Include(x => x.LTocar)
                                    .Where(x => x.IDCanco == IDCanco).ToListAsync();

        if (listCancons.Count == 0) {
            return null;
        } else {
            return listCancons[0];
        }
    }

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
    /// Accedeix a la ruta /api/Canco/putCanco/{IDCanco} dins de CancoController per modificar
    /// una Canco eliminant registres a la Llista LTocar
    /// </summary>
    /// <param name="cancoOriginal">L'objecte de la Canco original que volem modificar</param>
    /// <param name="updatedCanco">L'objecte de la Canco amb els elements modificats</param>
    /// <returns>Verificacio de que la Canco s'ha modificat correctament</returns>
    public async Task UpdateLTocarRemoveAsync(Canco cancoOriginal, Canco updatedCanco) {
        List<Tocar> lTocar = cancoOriginal.LTocar.ToList<Tocar>();

        foreach (var tocar in lTocar) {
            if (!updatedCanco.LTocar.Contains(tocar)) {
                await RemoveLTocarAsync(tocar, cancoOriginal);
            }
        }
    }


    /// <summary>
    /// Accedeix a la ruta /api/Canco/putCanco/{IDCanco} dins de CancoController per modificar
    /// una Canco afegint registres a la Llista LTocar
    /// </summary>
    /// <param name="cancoOriginal">L'objecte de la Canco original que volem modificar</param>
    /// <param name="updatedCanco">L'objecte de la Canco amb els elements modificats</param>
    /// <returns>Verificacio de que la Canco s'ha modificat correctament</returns>
    public async Task UpdateLTocarAddAsync(Canco cancoOriginal, Canco updatedCanco) {
        List<Tocar> lTocar = updatedCanco.LTocar.ToList<Tocar>();

        foreach (var tocar in lTocar) {
            if (!cancoOriginal.LTocar.Contains(tocar)) {
                await AddLTocarAsync(tocar, cancoOriginal);
            }
        }
    }


    /// <summary>
    /// Accedeix a la ruta /api/Canco/updateLlista/{MACAddress}/{NomLlista}/{IDCanco} dins 
    /// de CancoController per modificar una Llista de reproduccio eliminant Cancons de la 
    /// Llista de Cancons
    /// </summary>
    /// <param name="llistaOriginal">L'objecte de la Llista original que volem modificar</param>
    /// <param name="updatedLlista">L'objecte de la Llista amb els elements modificats</param>
    /// <returns>Verificacio de que la Llista s'ha modificat correctament</returns>
    public async Task UpdateLlistaRemoveAsync(Llista llistaOriginal, Llista updatedLlista) {
        List<Canco> lCancons = llistaOriginal.LCancons.ToList<Canco>();

        foreach (var canco in lCancons) {
            if (!updatedLlista.LCancons.Contains(canco)) {
                await RemoveLlistaAsync(canco.IDCanco, llistaOriginal);
            }
        }
    }


    /// <summary>
    /// Accedeix a la ruta /api/Canco/updateLlista/{MACAddress}/{NomLlista}/{IDCanco} dins 
    /// de CancoController per modificar una Llista de reproduccio afegint Cancons a la Llista
    /// de Cancons
    /// </summary>
    /// <param name="llistaOriginal">L'objecte de la Llista original que volem modificar</param>
    /// <param name="updatedLlista">L'objecte de la Llista amb els elements modificats</param>
    /// <returns>Verificacio de que la Llista s'ha modificat correctament</returns>
    public async Task UpdateLlistaAddAsync(Llista llistaOriginal, Llista updatedLlista) {
        List<Canco> lCancons = updatedLlista.LCancons.ToList<Canco>();

        foreach (var canco in lCancons) {
            if (!llistaOriginal.LCancons.Contains(canco)) {
                await AddLlistaAsync(canco.IDCanco, llistaOriginal);
            }
        }
    }

    
    /// <summary>
    /// Accedeix a la ruta /api/Canco/putCanco/{IDCanco} dins de CancoController per modificar
    /// una Canco afegint registres a la Llista LTocar
    /// </summary>
    /// <param name="tocar">Objecte de la classe Tocar que volem afegir a la Canco
    /// <param name="canco">L'objecte de la Canco original</param>
    /// <returns>Verificacio de que la Canco s'ha modificat correctament</returns>
    public async Task AddLTocarAsync(Tocar tocar, Canco canco) {

        if (canco.LTocar == null) canco.LListes = new List<Llista>();

        canco.LTocar.Add(tocar);
        _context.Entry(canco).State = EntityState.Modified;

        await _context.SaveChangesAsync();
    }
    

    /// <summary>
    /// Accedeix a la ruta /api/Canco/putCanco/{IDCanco} dins de CancoController per modificar
    /// una Canco eliminant registres a la Llista LTocar
    /// </summary>
    /// <param name="tocar">Objecte de la classe Tocar que volem afegir a la Canco
    /// <param name="canco">L'objecte de la Canco original</param>
    /// <returns>Verificacio de que la Canco s'ha modificat correctament</returns>
    public async Task RemoveLTocarAsync(Tocar tocar, Canco canco) {

        canco.LTocar.Remove(tocar);
        _context.Entry(canco).State = EntityState.Modified;

        await _context.SaveChangesAsync();
    }


    /// <summary>
    /// Accedeix a la ruta /api/Canco/updateLlista/{MACAddress}/{NomLlista}/{IDCanco} dins 
    /// de CancoController per modificar una Llista de reproduccio afegint Cancons a la Llista
    /// de Cancons
    /// </summary>
    /// <param name="IDCanco">Identificador de la Canco que volem afegir a la Llista de Reproduccio</param>
    /// <param name="llista">L'objecte de la Llista original</param>
    /// <returns>Verificacio de que la Llista s'ha modificat correctament</returns>
    public async Task AddLlistaAsync(string IDCanco, Llista llista) {
        Canco? canco = await GetAsync(IDCanco);

        if (canco == null) {
            canco = new Canco() {
                IDCanco = IDCanco
            };
            await CreateAsync(canco);
        }

        if (canco.LListes == null) canco.LListes = new List<Llista>();

        canco.LListes.Add(llista);
        _context.Entry(canco).State = EntityState.Modified;

        await _context.SaveChangesAsync();
    }
    

    /// <summary>
    /// Accedeix a la ruta /api/Canco/updateLlista/{MACAddress}/{NomLlista}/{IDCanco} dins 
    /// de CancoController per modificar una Llista de reproduccio eliminant Cancons de la 
    /// Llista de Cancons
    /// </summary>
    /// <param name="IDCanco">Identificador de la Canco que volem eliminar de la Llista de Reproduccio</param>
    /// <param name="llista">L'objecte de la Llista original</param>
    /// <returns>Verificacio de que la Llista s'ha modificat correctament</returns>
    public async Task RemoveLlistaAsync(string IDCanco, Llista llista) {
        Canco? canco = await GetAsync(IDCanco);

        canco.LListes.Remove(llista);
        _context.Entry(canco).State = EntityState.Modified;

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