using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using dymj.ReproductorMusica.API_SQL.Model;
using dymj.ReproductorMusica.API_SQL.Data;
using dymj.ReproductorMusica.API_SQL.Services;

namespace dymj.ReproductorMusica.API_SQL.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlbumController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly AlbumService _albumService;

        /// <summary>
        /// Constructor de la classe AlbumController
        /// Tambe crearem un objecte de la classe AlbumService passant-li el contexte de dades
        /// </summary>
        /// <param name="context">Contexte de dades utilitzat per a accedir a la base de dades.</param>
        public AlbumController(DataContext context)
        {
            _context = context;
            _albumService = new AlbumService(context);
        }

        /// <summary>
        /// Accedeix a la ruta /api/Album/getAlbums per obtenir tots els albums
        /// </summary>
        /// <returns>Una llista de tots els albums</returns>
        [HttpGet("getAlbums")]
        public async Task<ActionResult<IEnumerable<Album>>> GetAlbums()
        {
            return await _albumService.GetAsync();
        }


        /// <summary>
        /// Accedeix a la ruta /api/Album/getAlbum/{Titol}/{Any} per obtenir un Album
        /// </summary>
        /// <param name="Titol">Titol de l'Album a consultar</param>
        /// <param name="Any">Any de l'Album a consultar</param>
        /// <param name="IDCanco">Identificador de la Canco de l'Album a consultar</param>
        /// <returns>L'objecte de l'Album consultat</returns>
        [HttpGet("getAlbum/{Titol}/{Any}/{IDCanco}")]
        public async Task<ActionResult<Album>> GetAlbum(string Titol, int Any, string IDCanco)
        {
            var album = await _albumService.GetAsync(Titol, Any, IDCanco);

            if (album == null)
            {
                return NotFound();
            }

            return album;
        }

        /// <summary>
        /// Accedeix a la ruta /api/Album/putAlbum/{Titol}/{Any} per modificar un album
        /// </summary>
        /// <param name="Titol">Titol de l'Album a modificar</param>
        /// <param name="Any">Any de l'Album a modificar</param>
        /// <param name="IDCanco">Identificador de la Canco de l'Album a modificar</param>
        /// <param name="updatedAlbum">L'objecte de l'Album a modificar</param>
        /// <returns>Verificacio de que l'Album s'ha modificat correctament</returns>
        [HttpPut("putAlbum/{Titol}/{Any}/{IDCanco}")]
        public async Task<IActionResult> PutAlbum(string Titol, int Any, string IDCanco, Album updatedAlbum)
        {
            var album = await _albumService.GetAsync(Titol, Any);

            if (album is null || Titol != updatedAlbum.Titol || Any != updatedAlbum.Any || IDCanco != updatedAlbum.IDCanco) {
                return NotFound();
            }

            updatedAlbum.Titol = album.Titol;
            updatedAlbum.Any = album.Any;
            updatedAlbum.IDCanco = album.IDCanco;

            await _albumService.UpdateAsync(updatedAlbum);

            return NoContent();
        }

        /// <summary>
        /// Accedeix a la ruta /api/Album/postAlbum per crear un album
        /// </summary>
        /// <param name="album">L'objecte de l'Album a modificar</param>
        /// <returns>Verificacio de que l'Album s'ha creat correctament</returns>
        [HttpPost("postAlbum")]
        public async Task<IActionResult> PostAlbum(Album album)
        {
            // Considerar la possibilitat de comprovar previament si existeix el nom de l'album i retornar un error 409
            IActionResult result;

            try
            {
                await _albumService.CreateAsync(album);
                result = CreatedAtAction("GetAlbum", new { Titol = album.Titol, Any = album.Any, IDCanco = album.IDCanco }, album);
            }
            catch (DbUpdateException)
            {
                if (_albumService.GetAsync(album.Titol, album.Any, album.IDCanco) == null)
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return result;
        }

        /// <summary>
        /// Accedeix a la ruta /api/Album/deleteAlbum/{Titol}/{Any} per eliminar un album
        /// </summary>
        /// <param name="Titol">Titol de l'album a eliminar</param>
        /// <param name="Any">Any de l'album a eliminar</param>
        /// <param name="IDCanco">Identificador de la Canco de l'Album a eliminar</param>
        /// <returns>Verificacio de que l'Album s'ha eliminat correctament</returns>
        [HttpDelete("deleteAlbum/{Titol}/{Any}/{IDCanco}")]
        public async Task<IActionResult> DeleteAlbum(string Titol, int Any, string IDCanco)
        {
            var album = await _albumService.GetAsync(Titol, Any, IDCanco);
            
            if (album is null)
            {
                return NotFound();
            }

            await _albumService.RemoveAsync(album);

            return NoContent();
        }
    }
}
