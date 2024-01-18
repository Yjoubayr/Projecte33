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
    public class GrupController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly GrupService _grupService;

        public GrupController(DataContext context)
        {
            _context = context;
            _grupService = new GrupService(context);
        }

        /// <summary>
        /// Accedeix a la ruta /api/Grup/getGrups per obtenir tots els grups
        /// </summary>
        /// <returns>Una llista de tots els grups</returns>
        [HttpGet("getGrups")]
        public async Task<ActionResult<IEnumerable<Grup>>> GetGrups()
        {
            return await _context.Grups.ToListAsync();
        }

        /// <summary>
        /// Accedeix a la ruta /api/Grup/getGrup/{Nom} per obtenir un Grup
        /// </summary>
        /// <param name="Nom"></param>
        /// <returns></returns>
        [HttpGet("getGrup/{Nom}")]
        public async Task<ActionResult<Grup>> GetGrup(string Nom)
        {
            var grup = await _context.Grups.FindAsync(Nom);

            if (grup == null)
            {
                return NotFound();
            }

            return grup;
        }

        // PUT: api/Grup/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("putGrup/{Nom}")]
        public async Task<IActionResult> PutGrup(string Nom, Grup grup)
        {
            if (Nom != grup.Nom)
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
                if (!GrupExists(Nom))
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
        [HttpPost("postGrup")]
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

            return CreatedAtAction("GetGrup", new { Nom = grup.Nom }, grup);
        }

        // DELETE: api/Grup/5
        [HttpDelete("deleteGrup/{Nom}")]
        public async Task<IActionResult> DeleteGrup(string Nom)
        {
            var grup = await _context.Grups.FindAsync(Nom);
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
