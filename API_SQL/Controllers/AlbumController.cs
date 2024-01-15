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

        public AlbumController(DataContext context, AlbumService albumService)
        {
            _context = context;
            _albumService = albumService;
        }

        // GET: api/Album
        [HttpGet("getAlbums")]
        public async Task<ActionResult<IEnumerable<Album>>> GetAlbums()
        {
            return await _context.Albums.ToListAsync();
        }

        // GET: api/Album/5
        [HttpGet("getAlbums/{Titol}/{Any}")]
        public async Task<ActionResult<Album>> GetAlbum(string Titol, int Any)
        {
            var album = await _albumService.GetAsync(Titol, Any);

            if (album == null)
            {
                return NotFound();
            }

            return album;
        }

        // PUT: api/Album/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("putAlbum/{Titol}/{Any}")]
        public async Task<IActionResult> PutAlbum(string Titol, int Any, Album updatedAlbum)
        {
            var album = await _albumService.GetAsync(Titol, Any);

            if (album is null) {
                return NotFound();
            }

            updatedAlbum.Titol = Titol;
            updatedAlbum.Any = Any;

            await _albumService.UpdateAsync(Titol, Any, updatedAlbum);

            return NoContent();
        }

        // POST: api/Album
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("postAlbum")]
        public async Task<IActionResult> PostAlbum(Album album)
        {
            // Considerar la possibilitat de comprovar prèviament si existeix el nom de la llibreria i retornar un error 409
            IActionResult result;

            try
            {
                await _albumService.CreateAsync(album);
                result = CreatedAtAction("GetAlbum", new { Titol = album.Titol, Any = album.Any }, album);
            }
            catch (DbUpdateException)
            {
                if (_albumService.GetAsync(album.Titol, album.Any) == null)
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

        // DELETE: api/Album/5
        [HttpDelete("deleteAlbum/{Titol}/{Any}")]
        public async Task<IActionResult> DeleteAlbum(string Titol, int Any)
        {
            var album = await _albumService.GetAsync(Titol, Any);
            
            if (album == null)
            {
                return NotFound();
            }

            await _albumService.RemoveAsync(Titol, Any);

            return NoContent();
        }
    }
}
