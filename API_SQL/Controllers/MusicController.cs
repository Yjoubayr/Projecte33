using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using dymj.ReproductorMusica;

namespace API_SQL.Controllers
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

        // GET: api/Music
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Music>>> GetArtistes()
        {
            return await _context.Artistes.ToListAsync();
        }

        // GET: api/Music/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Music>> GetMusic(string id)
        {
            var music = await _context.Artistes.FindAsync(id);

            if (music == null)
            {
                return NotFound();
            }

            return music;
        }

        // PUT: api/Music/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMusic(string id, Music music)
        {
            if (id != music.Nom)
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
                if (!MusicExists(id))
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

        // POST: api/Music
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
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

            return CreatedAtAction("GetMusic", new { id = music.Nom }, music);
        }

        // DELETE: api/Music/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMusic(string id)
        {
            var music = await _context.Artistes.FindAsync(id);
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
