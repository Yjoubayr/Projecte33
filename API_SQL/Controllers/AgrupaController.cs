using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using dymj.ReproductorMusica.API_SQL.Data;
using dymj.ReproductorMusica.API_SQL.Model;

namespace API_SQL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgrupaController : ControllerBase
    {
        private readonly DataContext _context;

        public AgrupaController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Agrupa
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Agrupa>>> GetAgrupa()
        {
            return await _context.Agrupa.ToListAsync();
        }

        // GET: api/Agrupa/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Agrupa>> GetAgrupa(string id)
        {
            var agrupa = await _context.Agrupa.FindAsync(id);

            if (agrupa == null)
            {
                return NotFound();
            }

            return agrupa;
        }

        // PUT: api/Agrupa/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAgrupa(string id, Agrupa agrupa)
        {
            if (id != agrupa.NomGrup)
            {
                return BadRequest();
            }

            _context.Entry(agrupa).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AgrupaExists(id))
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

        // POST: api/Agrupa
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Agrupa>> PostAgrupa(Agrupa agrupa)
        {
            _context.Agrupa.Add(agrupa);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (AgrupaExists(agrupa.NomGrup))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetAgrupa", new { id = agrupa.NomGrup }, agrupa);
        }

        // DELETE: api/Agrupa/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAgrupa(string id)
        {
            var agrupa = await _context.Agrupa.FindAsync(id);
            if (agrupa == null)
            {
                return NotFound();
            }

            _context.Agrupa.Remove(agrupa);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AgrupaExists(string id)
        {
            return _context.Agrupa.Any(e => e.NomGrup == id);
        }
    }
}
