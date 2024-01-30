using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using dymj.ReproductorMusica.API_SQL.Model;
using dymj.ReproductorMusica.API_SQL.Data;

namespace dymj.ReproductorMusica.API_SQL.Services;

/// <summary>
/// Classe que proporciona serveis per a la gestio de grups de musica.
/// </summary>
public class GrupService
{
    private readonly DataContext _context;
    private readonly MusicService _musicService;
    
    /// <summary>
    /// Constructor de la classe GrupServei.
    /// </summary>
    /// <param name="context">Contexte de dades utilitzat per a accedir a la base de dades.</param>
    public GrupService(DataContext context)
    {
        _context = context;
        _musicService = new MusicService(context);
    }

    /// <summary>
    /// Obte tots els grups de musica de la base de dades.
    /// </summary>
    /// <returns>Llista de grups de musica.</returns>
    public async Task<List<Grup>> GetAsync() {
        return await _context.Grups.ToListAsync();
    }


    /// <summary>
    /// Obte un grup de musica especific a partir del seu nom.
    /// </summary>
    /// <param name="Nom">Nom del grup de musica.</param>
    /// <returns>Grup de musica corresponent al nom especificat.</returns>
    public async Task<Grup?> GetAsync(string Nom) =>
        await _context.Grups
                            .Include(x => x.LMusics)
                            .Include(x => x.LTocar)
                            .FirstOrDefaultAsync(x => x.Nom == Nom);


    /// <summary>
    /// Crea un nou grup de musica a la base de dades.
    /// </summary>
    /// <param name="newGrup">L'objecte del nou grup de musica.</param>
    /// <returns>Verificacio de que el grup de musica s'ha creat correctament.</returns>
    public async Task CreateAsync(Grup newGrup) {
        await _context.Grups.AddAsync(newGrup);
        await _context.SaveChangesAsync();

        //List<Music> lMusicsComplet = await _musicService.GetAsync();

        //foreach (var music in newGrup.LMusics) {

            /*if (!lMusicsComplet.Any(x => x.Nom == music.Nom)) {
                music.LGrups.Add(newGrup);
                await _musicService.CreateAsync(music);
                await _context.SaveChangesAsync();
            } else {
                Music musicObj = lMusicsComplet.Find(x => x.Nom == music.Nom);
                musicObj.LGrups.Add(newGrup);
                await _context.SaveChangesAsync();
            }*/






            /*Music musicObj = null; 
            List<Music> listMusics = await _context.Musics
                                    .Include(x => x.LGrups)
                                    .Where(x => x.Nom == music.Nom).ToListAsync();*/







            /*if (listMusics.Count > 0) {
                musicObj = listMusics[0];
            }

            Music musicObj = await _context.Musics
                            .Include(x => x.LGrups)
                            .Include(x => x.LTocar)
                            .FirstOrDefaultAsync(x => x.Nom == music.Nom);
            Music? musicObj = await _musicService.GetAsync(music.Nom);*/

            /*if (listMusics.Count > 0) {
                musicObj = listMusics[0];
                musicObj.LGrups.Add(newGrup);
                await _context.SaveChangesAsync();
            } else {
               // musicObj = new Music() { 
               //     Nom = music.Nom
               // };
                musicObj.LGrups.Add(newGrup);
               // await _musicService.CreateAsync(musicObj);
                await _context.SaveChangesAsync();
            }*/
        //}
    }


    /// <summary>
    /// Actualitza les dades d'un grup de musica existent a la base de dades.
    /// </summary>
    /// <param name="updatedGrup">Dades actualitzades del grup de musica.</param>
    /// <returns>Verificacio de que el grup de musica s'ha actualitzat correctament.</returns>
    public async Task UpdateAsync(Grup updatedGrup) {
        var grupOriginal = await GetAsync(updatedGrup.Nom);
        _context.Entry(grupOriginal).CurrentValues.SetValues(updatedGrup);
        await _context.SaveChangesAsync();
    }


    /// <summary>
    /// Accedeix a la ruta /api/Grup/updateMusic/{Nom} dins de GrupController per eliminar un Music
    /// de la llista de Musics d'un Grup
    /// </summary>
    /// <param name="musicOriginal">L'objecte del Mus original que volem modificar</param>
    /// <param name="updatedMusic">L'objecte del Music amb els elements modificats</param>
    /// <returns>Verificacio que el Music s'ha eliminat correctament al llistat de Musics</returns>
    public async Task UpdateMusicRemoveAsync(Music musicOriginal, Music updatedMusic) {
        
        List<Grup> lGrups = musicOriginal.LGrups.ToList<Grup>();

        foreach (var grup in lGrups) {
            if (!updatedMusic.LGrups.Contains(grup)) {
                await RemoveMusicAsync(grup.Nom, musicOriginal);
            }
        }
    }

    /// <summary>
    /// Accedeix a la ruta /api/Grup/updateMusic/{Nom} dins de GrupController per afegir un Music
    /// de la llista de Musics d'un Grup
    /// </summary>
    /// <param name="musicOriginal">L'objecte del Music original que volem modificar</param>
    /// <param name="updatedMusic">L'objecte del Music amb els elements modificats</param>
    /// <returns>Verificacio que el Music s'ha afegit correctament al llistat de Musics</returns>
    public async Task UpdateMusicAddAsync(Music musicOriginal, Music updatedMusic) {
        
        List<Grup> lGrups = updatedMusic.LGrups.ToList<Grup>();
        
        foreach (var grup in lGrups) {
            if (!musicOriginal.LGrups.Contains(grup)) {
                await AddMusicAsync(grup.Nom, musicOriginal);
            }
        }
    }

    /// <summary>
    /// Per afegir un Music de la llista de Musics d'un Grup
    /// </summary>
    /// <param name="nomGrup">Nom del Grup al qual volem afegir el Music</param>
    /// <param name="music">L'objecte del Music a afegir</param>
    /// <returns>Verificacio de que el Music s'ha afegit correctament al llistat de Musics del Grup</returns>
    public async Task AddMusicAsync(string nomGrup, Music music) {
        Grup? grup = await GetAsync(nomGrup);
        
        if (grup == null) {
            grup = new Grup() {
                Nom = nomGrup
            };
            await CreateAsync(grup);
        }

        grup.LMusics.Add(music);
        _context.Entry(grup).State = EntityState.Modified;
        
        await _context.SaveChangesAsync();
    }

    
    /// <summary>
    /// Per eliminar un Music de la llista de Musics d'un Grup
    /// </summary>
    /// <param name="nomGrup">Nom del Grup al qual volem eliminar el Music</param>
    /// <param name="music">L'objecte del Music a eliminar</param>
    /// <returns>Verificacio de que el Music s'ha eliminat correctament al llistat de Musics del Grup</returns>
    public async Task RemoveMusicAsync(string nomGrup, Music music) {
        Grup? grup = await GetAsync(nomGrup);

        grup.LMusics.Remove(music);
        _context.Entry(grup).State = EntityState.Modified;
        
        await _context.SaveChangesAsync();
    }


    /// <summary>
    /// Elimina un grup de musica de la base de dades a partir del seu nom.
    /// </summary>
    /// <param name="grup">L'objecte del grup de musica a eliminar.</param>
    /// <returns>Verificacio de que el grup de musica s'ha eliminat correctament.</returns>
    public async Task RemoveAsync(Grup grup) {
        _context.Grups.Remove(grup);
        await _context.SaveChangesAsync();
    }
}