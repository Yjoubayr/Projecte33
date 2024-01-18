using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using dymj.ReproductorMusica.API_SQL.Model;
using dymj.ReproductorMusica.API_SQL.Services;
using dymj.ReproductorMusica.API_SQL.Data;

namespace dymj.ReproductorMusica.API_SQL.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class LlistaController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly LlistaService _llistaService;

        /// <summary>
        /// Constructor de la classe LlistaController
        /// Tambe crearem un objecte de la classe LlistaService passant-li el contexte de dades
        /// </summary>
        /// <param name="context">Contexte de dades utilitzat per a accedir a la base de dades.</param>
        public LlistaController(DataContext context)
        {
            _context = context;
            _llistaService = new LlistaService(context);
        }

        // GET: api/Llista
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Llista>>> GetLlista()
        {
            return await _context.Llista.ToListAsync();
        }

        // GET: api/Llista/5
        [HttpGet("getLlista/{MACAddress}/{NomLlista}")]
        public async Task<ActionResult<Llista>> GetLlista(string MACAddress, string NomLlista)
        {
            if (_context.Llista == null || _context.Llista.FirstOrDefault(l => l.MACAddress == MACAddress && l.NomLlista == NomLlista) == null)
            {
                return NotFound();
            }

            else
            {
                var llista = await _context.Llista
                                            .Include(l => l.LCancons)
                                            .FirstOrDefaultAsync(l => l.MACAddress == MACAddress && l.NomLlista == NomLlista);
                
                if (llista == null) {
                    return NotFound();
                }

                return llista;
            }
        }

        // PUT: api/Llista/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("putLlista/{MACAddress}/{NomLlista}")]
        public async Task<IActionResult> PutLlista(string MACAddress, string NomLlista, Llista llista)
        {
            if (MACAddress != llista.MACAddress || NomLlista != llista.NomLlista)
            {
                return BadRequest();
            }


            _context.Entry(llista).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (_context.Llista.Find(llista.MACAddress, llista.NomLlista) == null)
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

        // POST: api/Llista
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Llista>> PostLlista(Llista llista)
        {
            if (_context.Llista == null)
            {
                return Problem("Entity set 'DataContext.Llista'  is null.");
            } 

            _context.Llista.Add(llista);
            
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (_context.Llista.Find(llista.MACAddress, llista.NomLlista) == null)
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetLlista", new { MACAddress = llista.MACAddress, NomLlista = llista.NomLlista }, llista);
        }

        // DELETE: api/Llista/5
        [HttpDelete("deleteLlista/{MACAddress}/{NomLlista}")]
        public async Task<IActionResult> DeleteLlista(string MACAddress, string NomLlista)
        {
            var llista = await _context.Llista.FindAsync(MACAddress, NomLlista);

            if (llista == null)
            {
                return NotFound();

            } else {
                _context.Llista.Remove(llista);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            
        }

    }
}
