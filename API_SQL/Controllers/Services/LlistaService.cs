using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using dymj.ReproductorMusica.API_SQL.Model;
using dymj.ReproductorMusica.API_SQL.Data;

namespace dymj.ReproductorMusica.API_SQL.Services;

public class LlistaService
{
    private readonly DataContext _context;

    /// <summary>
    /// Constructor de la classe LlistaService
    /// </summary>
    /// <param name="context">Contexte de dades utilitzat per a accedir a la base de dades.</param>
    public LlistaService(DataContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Accedeix a la ruta /api/Llista/getLlistes dins de LlistaController per obtenir totes les llistes
    /// </summary>
    /// <returns>El llistat de Llistes de reproduccio</returns>
    public async Task<List<Llista>> GetAsync() {
        return await _context.Llista
                            .Include(x => x.LCancons).ToListAsync();
    }

    /// <summary>   
    /// Accedeix a la ruta /api/Llista/getLlista/{MACAddress}/{NomLlista} dins de LlistaController per obtenir una Llista de reproduccio
    /// </summary>
    /// <param name="MACAddress">MACAddress de la Llista de reproduccio a obtenir</param>
    /// <param name="NomLlista">Nom de la Llista de reproduccio a obtenir</param>
    /// <returns>L'objecte de la Llista de reproduccio</returns>
    public async Task<Llista?> GetAsync(string MACAddress, string NomLlista) =>
        await _context.Llista
                            .Include(x => x.LCancons)
                            .FirstOrDefaultAsync(x => x.MACAddress == MACAddress && x.NomLlista == NomLlista);

    /// <summary>
    /// Accedeix a la ruta /api/Llista/postLlista dins de LlistaController per crear una Llista de reproduccio
    /// </summary>
    /// <param name="newLlista">L'objecte de la Llista de reproduccio a crear</param>
    /// <returns>Verificacio de que la Llista de reproduccio s'ha creat correctament</returns>
    public async Task CreateAsync(Llista newLlista) {
        await _context.Llista.AddAsync(newLlista);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Accedeix a la ruta /api/Llista/putLlista/{MACAddress}/{NomLlista} dins de LlistaController per modificar una Llista de reproduccio
    /// </summary>
    /// <param name="updatedLlista">L'objecte de la Llista de reproduccio a modificar</param>
    /// <returns>Verificacio de que la Llista de reproduccio s'ha modificat correctament</returns>
    public async Task UpdateAsync(Llista updatedLlista) {
        var llistaOriginal = await GetAsync(updatedLlista.MACAddress, updatedLlista.NomLlista);
        _context.Entry(llistaOriginal).CurrentValues.SetValues(updatedLlista);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Accedeix a la ruta /api/Llista/updateCanco/{IDCanco} dins de LlistaController per modificar una Canco
    /// </summary>
    /// <param name="cancoOriginal">L'objecte de la Canco original que volem modificar</param>
    /// <param name="updatedCanco">L'objecte de la Canco amb els elements modificats</param>
    /// <returns>Verificacio de que la Canco s'ha modificat correctament</returns>
    public async Task UpdateCancoRemoveAsync(Canco cancoOriginal, Canco updatedCanco) {
        
        List<Llista> lListes = cancoOriginal.LListes.ToList<Llista>();

        foreach (var llista in lListes) {
            if (!updatedCanco.LListes.Contains(llista)) {
                await RemoveCancoAsync(llista.MACAddress, llista.NomLlista, cancoOriginal);
            }
        }
    }


    /// <summary>
    /// Accedeix a la ruta /api/Llista/updateCanco/{IDCanco} dins de LlistaController per modificar una Canco
    /// </summary>
    /// <param name="cancoOriginal">L'objecte de la Canco original que volem modificar</param>
    /// <param name="updatedCanco">L'objecte de la Canco amb els elements modificats</param>
    /// <returns>Verificacio de que la Canco s'ha modificat correctament</returns>
    public async Task UpdateCancoAddAsync(Canco cancoOriginal, Canco updatedCanco) {
        
        List<Llista> lListes = updatedCanco.LListes.ToList<Llista>();
        
        foreach (var llista in lListes) {
            if (!cancoOriginal.LListes.Contains(llista)) {
                await AddCancoAsync(llista.MACAddress, llista.NomLlista, cancoOriginal);
            }
        }
    }


    /// <summary>
    /// Accedeix a la ruta /api/Llista/putLlista/{MACAddress}/{NomLlista}/{IDCanco} dins de LlistaController per afegir una Canco a una Llista de reproduccio
    /// </summary>
    /// <param name="MACAddress">Adreça MAC de la Llista de reproduccio a la qual volem afegir la Canco</param>
    /// <param name="NomLlista">Nom de la Llista de reproduccio a la qual volem afegir la Canco</param>
    /// <param name="canco">L'objecte de la Canco a afegir</param>
    /// <returns>Verificacio de que la Canco s'ha afegit correctament a la Llista de reproduccio</returns>
    public async Task AddCancoAsync(string MACAddress, string NomLlista, Canco canco) {
        Llista? llista = await GetAsync(MACAddress, NomLlista);

        if (llista == null) {
            llista = new Llista() {
                MACAddress = MACAddress,
                NomLlista = NomLlista
            };
            await CreateAsync(llista);
        }

        llista.LCancons.Add(canco);
        _context.Entry(llista).State = EntityState.Modified;
        
        await _context.SaveChangesAsync();        
    }


    /// <summary>
    /// Per eliminar una Canco de la llista de Cancons d'una Llista de reproduccio
    /// </summary>
    /// <param name="MACAddress">Adreça MAC de la Llista de reproduccio a la qual volem eliminar la Canco</param>
    /// <param name="NomLlista">Nom de la Llista de reproduccio a la qual volem eliminar la Canco</param>
    /// <param name="canco">L'objecte de la Canco a eliminar</param>
    /// <returns>Verificacio de que la Canco s'ha eliminat correctament a la Llista de reproduccio</returns>
    public async Task RemoveCancoAsync(string MACAddress, string NomLlista, Canco canco) {
        Llista? llista = await GetAsync(MACAddress, NomLlista);

        llista.LCancons.Remove(canco);
        _context.Entry(llista).State = EntityState.Modified;
        
        await _context.SaveChangesAsync();        
    }


    /// <summary>
    /// Accedeix a la ruta /api/Llista/deleteLlista/{MACAddress}/{NomLlista} dins de LlistaController per eliminar una Llista de reproduccio
    /// </summary>
    /// <param name="llista">L'objecte de la Llista de reproduccio a eliminar</param>
    /// <returns>Verificacio de que la Llista de reproduccio s'ha eliminat correctament</returns>
    public async Task RemoveAsync(Llista llista) {
        _context.Llista.Remove(llista);
        await _context.SaveChangesAsync();
    }
}
