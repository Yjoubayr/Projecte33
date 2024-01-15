using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using dymj.ReproductorMusica.API_SQL.Model;
using dymj.ReproductorMusica.API_SQL.Data;

namespace dymj.ReproductorMusica.API_SQL.Services;

public class AlbumService
{
    private readonly DataContext _context;
    public AlbumService(DataContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Accedeix a la ruta /api/Album/getAlbums per obtenir tots els albums
    /// </summary>
    /// <returns>El llistat de Albums</returns>
    public async Task<List<Album>> GetAsync() {
        return await _context.Albums.ToListAsync();
    }
    
    /// <summary>
    /// Accedeix a la ruta /api/Album/getAlbum/{Titol}/{Any} per obtenir un album
    /// </summary>
    /// <param name="Titol">El titol de l'album a obtenir</param>
    /// <param name="Any">L'any de publicacio de l'album a obtenir</param>
    /// <returns>L'objecte de l'Album trobat</returns>
    public async Task<Album?> GetAsync(string Titol, int Any) =>
        await _context.Albums
                            .Include(x => x.LCancons)
                            .FirstOrDefaultAsync(x => x.Titol == Titol && x.Any == Any);

    /// <summary>
    /// Accedeix a la ruta /api/Album/postAlbum per crear un album
    /// </summary>
    /// <param name="newAlbum">L'objecte de l'Album a crear</param>
    /// <returns>Verificacio de que l'Album s'ha creat correctament</returns>
    public async Task CreateAsync(Album newAlbum) =>
        await _context.Albums.AddAsync(newAlbum);

    /// <summary>
    /// Accedeix a la ruta /api/Album/putAlbum/{Titol}/{Any} per modificar un album
    /// </summary>
    /// <param name="Titol">Titol de l'Album a modificar</param>
    /// <param name="Any">Any de l'Album a modificar</param>
    /// <param name="updatedAlbum">L'objecte de l'Album a modificar</param>
    /// <returns>Verificacio de que l'Album s'ha modificat correctament</returns>
    public async Task UpdateAsync(string Titol, int Any, Album updatedAlbum) {
        var album = await _context.Albums
                            .FirstOrDefaultAsync(x => x.Titol == Titol && x.Any == Any);
        
        if (Titol == updatedAlbum.Titol && Any == updatedAlbum.Any && album != null)
        {
            _context.Entry(updatedAlbum).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }

    /// <summary>
    /// Accedeix a la ruta /api/Album/deleteAlbum/{Titol}/{Any} per eliminar un album
    /// </summary>
    /// <param name="Titol">Titol de l'album a eliminar</param>
    /// <param name="Any">Any de l'album a eliminar</param>
    /// <returns>Verificacio de que l'Album s'ha eliminat correctament</returns>
    public async Task RemoveAsync(string Titol, int Any) {
        var album = await _context.Albums
                            .FirstOrDefaultAsync(x => x.Titol == Titol && x.Any == Any);

        if (album != null) {
            _context.Albums.Remove(album);
            await _context.SaveChangesAsync();
        }
    }
}