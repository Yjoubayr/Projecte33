using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using dymj.ReproductorMusica.API_SQL.Model;
using dymj.ReproductorMusica.API_SQL.Data;

namespace dymj.ReproductorMusica.API_SQL.Services;

public class MusicService
{
    private readonly DataContext _context;
    public MusicService(DataContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Accedeix a la ruta /api/Music/getArtistes per obtenir totes les cancons
    /// </summary>
    /// <returns>El llistat d'artistes</returns>
    public async Task<List<Music>> GetAsync() {
        return await _context.Musics.ToListAsync();
    }
    
    /// <summary>
    /// Accedeix a la ruta /api/Music/getMusic/{Nom} per obtenir una Canco
    /// </summary>
    /// <param name="Nom">Nom del músic a obtenir</param>
    /// <returns>L'objecte del músic</returns>
    public async Task<Music?> GetAsync(string Nom) =>
        await _context.Musics
                            .Include(x => x.LGrups)
                            .Include(x => x.LCancons)
                            .FirstOrDefaultAsync(x => x.Nom == Nom);

    /// <summary>
    /// Accedeix a la ruta /api/Music/postMusic per crear un Music
    /// </summary>
    /// <param name="newMusic">L'objecte del músic a crear</param>
    /// <returns>Verificacio de que la Canco s'ha creat correctament</returns>
    public async Task CreateAsync(Music newMusic) =>
        await _context.Musics.AddAsync(newMusic);

    /// <summary>
    /// Accedeix a la ruta /api/Music/putMusic/{Nom} per modificar una Canco
    /// </summary>
    /// <param name="Nom">Nom del músic a modificar</param>
    /// <param name="updatedMusic">L'objecte del músic a modificar</param>
    /// <returns>Verificacio que el músic s'ha modificat correctament</returns>
    public async Task UpdateAsync(string Nom, Music updatedMusic) {
        
        if (Nom == updatedMusic.Nom)
        {
            _context.Entry(updatedMusic).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

    }

    /// <summary>
    /// Accedeix a la ruta /api/Music/deleteMusic/{Nom} per eliminar un músic
    /// </summary>
    /// <param name="Nom">Nom del músic a eliminar</param>
    /// <returns>Verificacio de que el músic s'ha eliminat correctament</returns>
    public async Task RemoveAsync(string Nom) {
        var music = await _context.Musics.FindAsync(Nom);
        
        _context.Musics.Remove(music);
        await _context.SaveChangesAsync();
    }
}