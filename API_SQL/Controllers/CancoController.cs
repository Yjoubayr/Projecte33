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
    public class CancoController : ControllerBase
    {
        private readonly DataContext _context;

        public CancoController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Canco
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Canco>>> GetCancons()
        {
            return await _context.Cancons.ToListAsync();
        }

        // GET: api/Canco/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Canco>> GetCanco(string id)
        {
            var canco = await _context.Cancons.FindAsync(id);

            if (canco == null)
            {
                return NotFound();
            }

            return canco;
        }

        // PUT: api/Canco/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCanco(string id, Canco canco)
        {
            if (id != canco.ID)
            {
                return BadRequest();
            }

            _context.Entry(canco).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CancoExists(id))
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

        // POST: api/Canco
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Canco>> PostCanco(Canco canco)
        {
            _context.Cancons.Add(canco);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CancoExists(canco.ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetCanco", new { id = canco.ID }, canco);
        }

        // DELETE: api/Canco/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCanco(string id)
        {
            var canco = await _context.Cancons.FindAsync(id);
            if (canco == null)
            {
                return NotFound();
            }

            _context.Cancons.Remove(canco);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CancoExists(string id)
        {
            return _context.Cancons.Any(e => e.ID == id);
        }
    }
}
