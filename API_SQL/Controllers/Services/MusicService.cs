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
    /// <param name="Nom">Nom del Music a obtenir</param>
    /// <returns>L'objecte del Music</returns>
    public async Task<Music?> GetAsync(string Nom) =>
        await _context.Musics
                            .Include(x => x.LGrups)
                            .Include(x => x.LTocar)
                            .FirstOrDefaultAsync(x => x.Nom == Nom);

    /// <summary>
    /// Accedeix a la ruta /api/Music/postMusic per crear un Music
    /// </summary>
    /// <param name="newMusic">L'objecte del Music a crear</param>
    /// <returns>Verificacio de que la Canco s'ha creat correctament</returns>
    public async Task CreateAsync(Music newMusic) {
        await _context.Musics.AddAsync(newMusic);
        await _context.SaveChangesAsync();
    }
        

    /// <summary>
    /// Accedeix a la ruta /api/Music/putMusic/{Nom} per modificar una Canco
    /// </summary>
    /// <param name="Nom">Nom del Music a modificar</param>
    /// <param name="updatedMusic">L'objecte del Music a modificar</param>
    /// <returns>Verificacio que el Music s'ha modificat correctament</returns>
    public async Task UpdateAsync(Music updatedMusic) {
        var musicOriginal = await GetAsync(updatedMusic.Nom);
        _context.Entry(musicOriginal).CurrentValues.SetValues(updatedMusic);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Accedeix a la ruta /api/Music/deleteMusic/{Nom} per eliminar un Music
    /// </summary>
    /// <param name="Music">L'objecte del Music a eliminar</param>
    /// <returns>Verificacio de que el Music s'ha eliminat correctament</returns>
    public async Task RemoveAsync(Music music) {
        _context.Musics.Remove(music);
        await _context.SaveChangesAsync();
    }
}