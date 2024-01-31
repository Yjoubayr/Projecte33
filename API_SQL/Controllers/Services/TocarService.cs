using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using dymj.ReproductorMusica.API_SQL.Model;
using dymj.ReproductorMusica.API_SQL.Data;

namespace dymj.ReproductorMusica.API_SQL.Services;

public class TocarService
{
    
    private readonly DataContext _context;
    private readonly CancoService _cancoService;
    private readonly MusicService _musicService;
    private readonly GrupService _grupService;
    private readonly InstrumentService _instrumentService;

    /// <summary>
    /// Constructor de la classe TocarService
    /// Tambe crearem un objecte de la classe CancoService, MusicService, GrupService i 
    /// InstrumentService passant-los el contexte de dades
    /// </summary>
    /// <param name="context">Contexte de dades utilitzat per a accedir a la base de dades.</param>
    public TocarService(DataContext context)
    {
        _context = context;
        _cancoService = new CancoService(context);
        _musicService = new MusicService(context);
        _grupService = new GrupService(context);
        _instrumentService = new InstrumentService(context);
    }
    

    public async Task<List<Tocar>> GetAsync() {
        return await _context.Tocar.ToListAsync();
    }



    /// <summary>
    /// Accedeix a la ruta /api/Tocar/getTocar/{IDCanco}/{NomMusic}/{NomGrup}/{NomInstrument} 
    /// dins de TocarController per obtenir un registre Tocar
    /// </summary>
    /// <param name="IDCanco">Identificador de la Canco del registre de Tocar a obtenir</param>
    /// <param name="NomMusic">Nom del Music del registre de Tocar a obtenir</param>
    /// <param name="NomGrup">Nom del Grup del registre de Tocar a obtenir</param>
    /// <param name="NomInstrument">Nom de l'instrument del registre de Tocar a obtenir</param>
    /// <returns>L'objecte de la classe Tocar</returns>
    public async Task<Tocar?> GetAsync(string IDCanco, string NomMusic, string NomGrup, string NomInstrument) {
        List<Tocar> listTocar = await _context.Tocar
                                    .Where(x => x.IDCanco == IDCanco 
                                    && x.NomMusic == NomMusic 
                                    && x.NomGrup == NomGrup 
                                    && x.NomInstrument == NomInstrument).ToListAsync();

        if (listTocar.Count == 0) {
            return null;
        } else {
            return listTocar[0];
        }
    }

    /// <summary>
    /// Accedeix a la ruta /api/Tocar/postTocar dins de TocarController per crear un
    /// registre Tocar
    /// </summary>
    /// <param name="newTocar">L'objecte de la classe Tocar a crear</param>
    /// <returns>Verificacio de que el registre de Tocar s'ha creat correctament</returns>
    public async Task CreateAsync(Tocar newTocar) {
        newTocar.CancoObj = await _cancoService.GetAsync(newTocar.IDCanco);
        newTocar.MusicObj = await _musicService.GetAsync(newTocar.NomMusic);
        if (newTocar.NomGrup != null) {
            newTocar.GrupObj = await _grupService.GetAsync(newTocar.NomGrup);
        }
        newTocar.InstrumentObj = await _instrumentService.GetAsync(newTocar.NomInstrument);
        await _context.Tocar.AddAsync(newTocar);
        await _context.SaveChangesAsync();
    }


    /// <summary>
    /// Accedeix a la ruta /api/Tocar/deleteTocar/{IDCanco}/{NomMusic}/{NomGrup}/{NomInstrument}
    /// dins de TocarController per eliminar un registre Tocar
    /// </summary>
    /// <param name="IDCanco">Identificador de la Canco del registre de Tocar a eliminar</param>
    /// <param name="NomMusic">Nom del Music del registre de Tocar a eliminar</param>
    /// <param name="NomGrup">Nom del Grup del registre de Tocar a eliminar</param>
    /// <param name="NomInstrument">Nom de l'instrument del registre de Tocar a eliminar</param>
    /// <returns>Verificacio de que el registre de Tocar s'ha eliminat correctament</returns>
    public async Task RemoveAsync(Tocar tocar) {
        _context.Tocar.Remove(tocar);
        await _context.SaveChangesAsync();
    }
}