using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using dymj.ReproductorMusica.API_SQL.Model;
using dymj.ReproductorMusica.API_SQL.Data;

namespace dymj.ReproductorMusica.API_SQL.Services;

public class CancoService
{
    private readonly DataContext _context;

    /// <summary>
    /// Constructor de la classe CancoService
    /// </summary>
    /// <param name="context">Contexte de dades utilitzat per a accedir a la base de dades.</param>
    public CancoService(DataContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Accedeix a la ruta /api/Canco/getCancons per obtenir totes les cancons
    /// </summary>
    /// <returns>El llistat de Cancons</returns>
    public async Task<List<Canco>> GetAsync() {
        return await _context.Cancons.ToListAsync();
    }
    
    /// <summary>
    /// Accedeix a la ruta /api/Canco/getCanco/{IDCanco} per obtenir una Canco
    /// </summary>
    /// <param name="IDCanco">Identificador de la Canco a obtenir</param>
    /// <returns>L'objecte de la Canco</returns>
    public async Task<Canco?> GetAsync(string IDCanco) =>
        await _context.Cancons
                            .Include(x => x.LListes)
                            .Include(x => x.LTocar)
                            .FirstOrDefaultAsync(x => x.IDCanco == IDCanco);

    /// <summary>
    /// Accedeix a la ruta /api/Canco/postCanco per crear una Canco
    /// </summary>
    /// <param name="newCanco">L'objecte de la Canco a crear</param>
    /// <returns>Verificacio de que la Canco s'ha creat correctament</returns>
    public async Task CreateAsync(Canco newCanco) {
        newCanco.IDCanco = Guid.NewGuid().ToString();
        await _context.Cancons.AddAsync(newCanco);
        await _context.SaveChangesAsync();
    }
   

    /// <summary>
    /// Accedeix a la ruta /api/Canco/putCanco/{IDCanco} per modificar una Canco
    /// </summary>
    /// <param name="updatedCanco">L'objecte de la Canco a modificar</param>
    /// <returns>Verificacio de que la Canco s'ha modificat correctament</returns>
    public async Task UpdateAsync(Canco updatedCanco) {
        var cancoOriginal = await GetAsync(updatedCanco.IDCanco);
        _context.Entry(cancoOriginal).CurrentValues.SetValues(updatedCanco);
        //_context.Entry(updatedCanco).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Accedeix a la ruta /api/Canco/deleteCanco/{IDCanco} per eliminar una Canco
    /// </summary>
    /// <param name="canco">L'objecte de la Canco a eliminar</param>
    /// <returns>Verificacio de que la Canco s'ha eliminat correctament</returns>
    public async Task RemoveAsync(Canco canco) {        
        _context.Cancons.Remove(canco);
        await _context.SaveChangesAsync();
    }
}