using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using dymj.ReproductorMusica.API_SQL.Model;
using dymj.ReproductorMusica.API_SQL.Data;

namespace dymj.ReproductorMusica.API_SQL.Services;

public class AlbumService
{
    private readonly DataContext _context;
    private readonly CancoService _cancoService;

    /// <summary>
    /// Constructor de la classe AlbumService
    /// </summary>
    /// <param name="context">Contexte de dades utilitzat per a accedir a la base de dades.</param>
    public AlbumService(DataContext context)
    {
        _context = context;
        _cancoService = new CancoService(context);
    }

    /// <summary>
    /// Accedeix a la ruta /api/Album/getAlbums dins de AlbumController per obtenir tots els albums
    /// </summary>
    /// <returns>El llistat de Albums</returns>
    public async Task<List<Album>> GetAsync() {
        return await _context.Albums.ToListAsync();
    }
    
    /// <summary>
    /// Accedeix a la ruta /api/Album/getAnysAlbum/{Titol} dins de AlbumController per obtenir tots els albums
    /// </summary>
    /// <returns>El llistat d'anys d'un Album en concret</returns>
    public async Task<List<string>> GetYearsByTitle(string Titol) {
        return await _context.Albums
                                .Where(x => x.Titol == Titol)
                                .Select(x => x.Any.ToString())
                                .Distinct()
                                .ToListAsync();
    }
    
    /// <summary>
    /// Accedeix a la ruta /api/Album/getAlbum/{Titol}/{Any}/{IDCanco} dins de AlbumController per obtenir un album
    /// </summary>
    /// <param name="Titol">El titol de l'album a obtenir</param>
    /// <param name="Any">L'any de publicacio de l'album a obtenir</param>
    /// <param name="IDCanco">L'identificador de la canco de l'album a obtenir</param>
    /// <returns>L'objecte de l'Album trobat</returns>
    public async Task<Album?> GetAsync(string Titol, int Any, string IDCanco) {
        List<Album> listAlbums = await _context.Albums
                                    .Include(x => x.CancoObj)
                                    .Where(x => x.Titol == Titol && x.Any == Any && x.IDCanco == IDCanco).ToListAsync();

        if (listAlbums.Count == 0) {
            return null;
        } else {
            return listAlbums[0];
        }
    }

    /// <summary>
    /// Obte tots els albums amb el mateix titol
    /// </summary>
    /// <param name="Titol">El titol del/s Album/s a obtenir</param>
    /// <returns>El llistat d'Albums trobats</returns>
    public async Task<List<Album>> GetAlbumsByTitolAsync(string Titol) {
        List<Album> listAlbums = await _context.Albums
                                    .Include(x => x.CancoObj)
                                    .Where(x => x.Titol == Titol).ToListAsync();

        if (listAlbums.Count == 0) {
            return null;
        } else {
            return listAlbums;
        }
    }
        
    /// <summary>
    /// Accedeix a la ruta /api/Album/getAlbum/{Titol}/{Any} dins de AlbumController per obtenir un album
    /// </summary>
    /// <param name="Titol">El titol de l'album a obtenir</param>
    /// <param name="Any">L'any de publicacio de l'album a obtenir</param>
    /// <returns>L'objecte de l'Album trobat</returns>
    public async Task<List<Album>> GetAsync(string Titol, int Any) =>
        await _context.Albums.Where(x => x.Titol == Titol && x.Any == Any).ToListAsync();

    /// <summary>
    /// Accedeix a la ruta /api/Album/postAlbum dins de AlbumController per crear un album
    /// </summary>
    /// <param name="newAlbum">L'objecte de l'Album a crear</param>
    /// <returns>Verificacio de que l'Album s'ha creat correctament</returns>
    public async Task CreateAsync(Album newAlbum) {
        newAlbum.CancoObj = await _cancoService.GetAsync(newAlbum.IDCanco);
        await _context.Albums.AddAsync(newAlbum);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Accedeix a la ruta /api/Album/putAlbum/{Titol}/{Any} dins de AlbumController per modificar un album
    /// </summary>
    /// <param name="updatedAlbum">L'objecte de l'Album a modificar</param>
    /// <returns>Verificacio de que l'Album s'ha modificat correctament</returns>
    public async Task UpdateAsync(Album updatedAlbum) {
        updatedAlbum.CancoObj = await _cancoService.GetAsync(updatedAlbum.IDCanco);
        var albumOriginal = await GetAsync(updatedAlbum.Titol, updatedAlbum.Any, updatedAlbum.IDCanco);
        _context.Entry(albumOriginal).CurrentValues.SetValues(updatedAlbum);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Accedeix a la ruta /api/Album/deleteAlbum/{Titol}/{Any} dins de AlbumController per eliminar un album
    /// </summary>
    /// <param name="album">L'objecte de l'Album a eliminar</param>
    /// <returns>Verificacio de que l'Album s'ha eliminat correctament</returns>
    public async Task RemoveAsync(Album album) {
        _context.Albums.Remove(album);
        await _context.SaveChangesAsync();
    }
}