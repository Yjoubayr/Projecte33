using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using dymj.ReproductorMusica.API_SQL.Model;
using dymj.ReproductorMusica.API_SQL.Data;

namespace dymj.ReproductorMusica.API_SQL.Services;

/// <summary>
/// Classe que implementa els mètodes per a gestionar els instruments
/// </summary>
public class InstrumentService
{
    private readonly DataContext _context;

    /// <summary>
    /// Constructor de la classe InstrumentService
    /// </summary>
    /// <param name="context">Contexte de dades utilitzat per a accedir a la base de dades.</param>
    public InstrumentService(DataContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Accedeix a la ruta /api/Instrument/getInstruments dins de InstrumentController per obtenir tots els Instruments
    /// </summary>
    /// <returns>El llistat d'Instruments</returns>
    public async Task<List<Instrument>> GetAsync()
    {
        return await _context.Instruments.ToListAsync();
    }

    /// <summary>
    /// Accedeix a la ruta /api/Instrument/getInstrument/{Nom} dins de InstrumentController per obtenir un Instrument
    /// </summary>
    /// <param name="Nom">Nom de l'Instrument a obtenir</param>
    /// <returns>L'objecte de l'Instrument</returns>
    public async Task<Instrument?> GetAsync(string Nom) =>
        await _context.Instruments
                            .Include(x => x.LTocar)
                            .FirstOrDefaultAsync(x => x.Nom == Nom);

    /// <summary>
    /// Accedeix a la ruta /api/Instrument/postInstrument dins de InstrumentController per crear un Instrument
    /// </summary>
    /// <param name="newInstrument">L'objecte de l'Instrument a crear</param>
    /// <returns>Verificacio de que l'Instrument s'ha creat correctament</returns>
    public async Task CreateAsync(Instrument newInstrument) {
        await _context.Instruments.AddAsync(newInstrument);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Accedeix a la ruta /api/Instrument/putInstrument/{Nom} dins de InstrumentController per modificar un Instrument
    /// </summary>
    /// <param name="updatedInstrument">L'objecte de l'Instrument a modificar</param>
    /// <returns>Verificacio que l'Instrument s'ha modificat correctament</returns>
    public async Task UpdateAsync(Instrument updatedInstrument)
    {
        var instrumentOriginal = await GetAsync(updatedInstrument.Nom);
        _context.Entry(instrumentOriginal).CurrentValues.SetValues(updatedInstrument);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Accedeix a la ruta /api/Instrument/deleteInstrument/{Nom} dins de InstrumentController per eliminar un Instrument
    /// </summary>
    /// <param name="Instrument">L'objecte de l'Instrument a eliminar</param>
    /// <returns>Verificacio de que l'Instrument s'ha eliminat correctament</returns>
    public async Task RemoveAsync(Instrument instrument)
    {
        _context.Instruments.Remove(instrument);
        await _context.SaveChangesAsync();
    }
}