using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using dymj.ReproductorMusica.API_SQL.Model;
using dymj.ReproductorMusica.API_SQL.Data;

namespace dymj.ReproductorMusica.API_SQL.Services;

public class CancoService
{
    private readonly DataContext _context;
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
    /// Accedeix a la ruta /api/Canco/getCanco/{ID} per obtenir una Canco
    /// </summary>
    /// <param name="ID">ID de la Canco a obtenir</param>
    /// <returns>L'objecte de la Canco</returns>
    public async Task<Canco?> GetAsync(string ID, DataContext context) =>
        await context.Cancons
                            .Include(x => x.LListes)
                            .Include(x => x.LAlbums)
                            .Include(x => x.LMusics)
                            .Include(x => x.LGrups)
                            .Include(x => x.LExtensions)
                            .FirstOrDefaultAsync(x => x.ID == ID);

    /// <summary>
    /// Accedeix a la ruta /api/Canco/postCanco per crear una Canco
    /// </summary>
    /// <param name="newCanco">L'objecte de la Canco a crear</param>
    /// <returns>Verificacio de que la Canco s'ha creat correctament</returns>
    public async Task CreateAsync(Canco newCanco, DataContext context) {
        await context.Cancons.AddAsync(newCanco);
        await context.SaveChangesAsync();
    }
   

    /// <summary>
    /// Accedeix a la ruta /api/Canco/putCanco/{ID} per modificar una Canco
    /// </summary>
    /// <param name="ID">ID de la canco a modificar</param>
    /// <param name="updatedCanco">L'objecte de la Canco a modificar</param>
    /// <returns>Verciicacio de que la Canco s'ha modificat correctament</returns>
    public async Task UpdateAsync(string ID, Canco updatedCanco, DataContext context) {
        
        if (ID == updatedCanco.ID)
        {
            context.Entry(updatedCanco).State = EntityState.Modified;
            await context.SaveChangesAsync();
            //await _context.Cancons.ReplaceOneAsync(x => x.ID == ID, updatedCanco);
        }

    }

    /// <summary>
    /// Accedeix a la ruta /api/Canco/deleteCanco/{ID} per eliminar una Canco
    /// </summary>
    /// <param name="ID">ID de la Canco a eliminar</param>
    /// <returns>Verciicacio de que la Canco s'ha eliminat correctament</returns>
    public async Task RemoveAsync(string ID, DataContext context) {
        var canco = await context.Cancons.FindAsync(ID);
        
        context.Cancons.Remove(canco);
        await context.SaveChangesAsync();
    }
}