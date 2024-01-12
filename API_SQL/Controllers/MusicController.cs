using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using dymj.ReproductorMusica.API_SQL.Model;
using dymj.ReproductorMusica.API_SQL.Data;

namespace dymj.ReproductorMusica.API_SQL.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class MusicController : ControllerBase
    {
        private readonly DataContext _context;

        public MusicController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Music/getMusics
        [HttpGet("getMusics")]
        public async Task<ActionResult<IEnumerable<Music>>> GetArtistes()
        {
            return await _context.Artistes.ToListAsync();
        }

        // GET: api/Music/getMusic/JustinBieber
        [HttpGet("getMusic/{Nom}")]
        public async Task<ActionResult<Music>> GetMusic(string Nom)
        {
            var music = await _context.Artistes.FindAsync(Nom);

            if (music == null)
            {
                return NotFound();
            }

            return music;
        }

        // PUT: api/Music/putMusic/JustinBieber
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("putMusic/{Nom}")]
        public async Task<IActionResult> PutMusic(string Nom, Music music)
        {
            if (Nom != music.Nom)
            {
                return BadRequest();
            }

            _context.Entry(music).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MusicExists(Nom))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Music/postMusic
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("postMusic")]
        public async Task<ActionResult<Music>> PostMusic(Music music)
        {
            _context.Artistes.Add(music);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (MusicExists(music.Nom))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetMusic", new { Nom = music.Nom }, music);
        }

        // DELETE: api/Music/deleteMusic/JustinBieber
        [HttpDelete("deleteMusic/{Nom}")]
        public async Task<IActionResult> DeleteMusic(string Nom)
        {
            var music = await _context.Artistes.FindAsync(Nom);
            if (music == null)
            {
                return NotFound();
            }

            _context.Artistes.Remove(music);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MusicExists(string id)
        {
            return _context.Artistes.Any(e => e.Nom == id);
        }
    }
}
