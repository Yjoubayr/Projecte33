using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using dymj.ReproductorMusica.API_SQL.Model;

namespace dymj.ReproductorMusica.API_SQL.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class GrupController : ControllerBase
    {
        private readonly DataContext _context;

        public GrupController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Grup
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Grup>>> GetGrups()
        {
            return await _context.Grups.ToListAsync();
        }

        // GET: api/Grup/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Grup>> GetGrup(string id)
        {
            var grup = await _context.Grups.FindAsync(id);

            if (grup == null)
            {
                return NotFound();
            }

            return grup;
        }

        // PUT: api/Grup/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGrup(string id, Grup grup)
        {
            if (id != grup.Nom)
            {
                return BadRequest();
            }

            _context.Entry(grup).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GrupExists(id))
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

        // POST: api/Grup
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Grup>> PostGrup(Grup grup)
        {
            _context.Grups.Add(grup);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (GrupExists(grup.Nom))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetGrup", new { id = grup.Nom }, grup);
        }

        // DELETE: api/Grup/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGrup(string id)
        {
            var grup = await _context.Grups.FindAsync(id);
            if (grup == null)
            {
                return NotFound();
            }

            _context.Grups.Remove(grup);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GrupExists(string id)
        {
            return _context.Grups.Any(e => e.Nom == id);
        }
    }
}
