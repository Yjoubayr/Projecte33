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
    public async Task<List<string>> GetYearsByTitleAsync(string Titol) {
        return await _context.Albums
                                .Where(x => x.Titol == Titol)
                                .Select(x => x.Any.ToString())
                                .Distinct()
                                .ToListAsync();
    }
    
    /// <summary>
    /// Accedeix a la ruta /api/Album/getTitlesAlbums dins de AlbumController 
    /// per obtenir tots els titols dels albums
    /// </summary>
    /// <returns>El llistat de tots els Titols dels Albums</returns>
    public async Task<List<string>> GetTitlesAsync() {
        return await _context.Albums
                                .Select(x => x.Titol)
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
        Album listAlbums = await _context.Albums
                                    .Include(x => x.LCancons)
                                    .Where(x => x.Titol == Titol && x.Any == Any && x.IDCanco == IDCanco).FirstOrDefaultAsync();

        if (listAlbums == null) {
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
    public async Task<Album> CreateAsync(Album newAlbum) {
        if(newAlbum == null){
            return null;
        }
        await _context.Albums.AddAsync(newAlbum);
        await _context.SaveChangesAsync();
        return newAlbum;
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