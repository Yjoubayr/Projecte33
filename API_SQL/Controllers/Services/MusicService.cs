using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using dymj.ReproductorMusica.API_SQL.Model;
using dymj.ReproductorMusica.API_SQL.Data;

namespace dymj.ReproductorMusica.API_SQL.Services;

public class MusicService
{
    private readonly DataContext _context;
    
    /// <summary>
    /// Constructor de la classe MusicService
    /// </summary>
    /// <param name="context">Contexte de dades utilitzat per a accedir a la base de dades.</param>
    public MusicService(DataContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Accedeix a la ruta /api/Music/getArtistes dins de MusicController per obtenir totes les cancons
    /// </summary>
    /// <returns>El llistat d'artistes</returns>
    public async Task<List<Music>> GetAsync() {
        return await _context.Musics
                            .Include(x => x.LGrups)
                            .Include(x => x.LTocar)
                            .ToListAsync();
    }
    
    /// <summary>
    /// Accedeix a la ruta /api/Music/getMusic/{Nom} dins de MusicController per obtenir una Canco
    /// </summary>
    /// <param name="Nom">Nom del Music a obtenir</param>
    /// <returns>L'objecte del Music</returns>
    public async Task<Music?> GetAsync(string Nom) =>
        await _context.Musics
                            .Include(x => x.LGrups)
                            .Include(x => x.LTocar)
                            .FirstOrDefaultAsync(x => x.Nom == Nom);

    /// <summary>
    /// Accedeix a la ruta /api/Music/postMusic dins de MusicController per crear un Music
    /// </summary>
    /// <param name="newMusic">L'objecte del Music a crear</param>
    /// <param name="grupService">Objecte de la classe GrupService</param>
    /// <returns>Verificacio de que la Canco s'ha creat correctament</returns>
    public async Task CreateAsync(Music newMusic, GrupService grupService) {
        await _context.Musics.AddAsync(newMusic);
        
        foreach (var grup in newMusic.LGrups) {
            Grup? grupObj = await grupService.GetAsync(grup.Nom);

            if (grupObj != null) {
                grupObj.LMusics.Add(newMusic);
            }
        }
        await _context.SaveChangesAsync();
    }


    /// <summary>
    /// Accedeix a la ruta /api/Music/putMusic/{Nom} dins de MusicController per modificar una Canco
    /// </summary>
    /// <param name="updatedMusic">L'objecte del Music a modificar</param>
    /// <returns>Verificacio que el Music s'ha modificat correctament</returns>
    public async Task UpdateAsync(Music updatedMusic) {
        var musicOriginal = await GetAsync(updatedMusic.Nom);
        _context.Entry(musicOriginal).CurrentValues.SetValues(updatedMusic);
        await _context.SaveChangesAsync();
    }


    /// <summary>
    /// Accedeix a la ruta /api/Music/updateGrup/{Nom} dins de MusicController per eliminar un Grup
    /// de la llista de grups d'un Music
    /// </summary>
    /// <param name="grupOriginal">L'objecte del Grup original que volem modificar</param>
    /// <param name="updatedGrup">L'objecte del Grup amb els elements modificats</param>
    /// <returns>Verificacio que el Grup s'ha eliminat correctament al llistat de Grups</returns>
    public async Task UpdateGrupRemoveAsync(Grup grupOriginal, Grup updatedGrup) {
        
        List<Music> lMusics = grupOriginal.LMusics.ToList<Music>();

        foreach (var music in lMusics) {
            if (!updatedGrup.LMusics.Contains(music)) {
                await RemoveGrupAsync(music.Nom, grupOriginal);
            }
        }
    }


    /// <summary>
    /// Accedeix a la ruta /api/Music/updateGrup/{Nom} dins de MusicController per afegir un Grup
    /// a la llista de Grups d'un Music
    /// </summary>
    /// <param name="grupService">Objecte de la classe GrupService per afegir el grup </param>
    /// <param name="grupOriginal">L'objecte del Grup original que volem modificar</param>
    /// <param name="updatedGrup">L'objecte del Grup amb els elements modificats</param>
    /// <returns>Verificacio que el Grup s'ha afegit correctament al llistat de Grups</returns>
    public async Task UpdateGrupAddAsync(GrupService grupService, Grup grupOriginal, Grup updatedGrup) {
        
        List<Music> lMusics = updatedGrup.LMusics.ToList<Music>();
        
        foreach (var music in lMusics) {
            if (!grupOriginal.LMusics.Contains(music)) {
                await AddGrupAsync(grupService, music.Nom, grupOriginal);
            }
        }
    }


    /// <summary>
    /// Per Afegir un Grup de la llista de Grups d'un Music
    /// </summary>
    /// <param name="grupService">Objecte de la classe GrupService per afegir el grup </param>
    /// <param name="nomMusic">Nom del Music a modificar</param>
    /// <param name="grup">L'objecte del Grup a afegir</param>
    /// <returns>Verificacio que el Grup s'ha afegit correctament al llistat de Grups</returns>
    public async Task AddGrupAsync(GrupService grupService, string nomMusic, Grup grup) {
        Music? music = await GetAsync(nomMusic);

        if (music == null) {
            music = new Music() {
                Nom = nomMusic
            };
            await CreateAsync(music, grupService);
        }

        music.LGrups.Add(grup);
        _context.Entry(music).State = EntityState.Modified;
        
        await _context.SaveChangesAsync();
    }


    /// <summary>
    /// Per eliminar un Grup de la llista de Grups d'un Music
    /// </summary>
    /// <param name="nomMusic">Nom del Music a modificar</param>
    /// <param name="grup">L'objecte del Grup a eliminar</param>
    /// <returns>Verificacio que el Grup s'ha eliminat correctament al llistat de Grups</returns>
    public async Task RemoveGrupAsync(string nomMusic, Grup grup) {
        Music? music = await GetAsync(nomMusic);

        music.LGrups.Remove(grup);
        _context.Entry(music).State = EntityState.Modified;
        
        await _context.SaveChangesAsync();
    }


    /// <summary>
    /// Accedeix a la ruta /api/Music/deleteMusic/{Nom} dins de MusicController per eliminar un Music
    /// </summary>
    /// <param name="Music">L'objecte del Music a eliminar</param>
    /// <returns>Verificacio de que el Music s'ha eliminat correctament</returns>
    public async Task RemoveAsync(Music music) {
        _context.Musics.Remove(music);
        await _context.SaveChangesAsync();
    }
}