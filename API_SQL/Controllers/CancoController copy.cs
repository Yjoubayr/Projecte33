using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using dymj.ReproductorMusica.API_SQL.Model;
using dymj.ReproductorMusica.API_SQL.Services;

namespace dymj.ReproductorMusica.API_SQL.Controllers
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

        /// <summary>
        /// Accedeix a la ruta /api/Canco/getCancons per obtenir totes les cancons
        /// </summary>
        /// <returns>Task<ActionResult<IEnumerable<Canco>>> un array de totes les cancons</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Canco>>> GetCancons()
        {
            return await _context.Cancons.ToListAsync();
        }


        /// <summary>
        /// Accedeix a la ruta /api/Canco/getCanco/{ID} per obtenir una canco
        /// </summary>
        /// <returns>L'objecte de la canco consultada</returns>
        [HttpGet("getCanco/{ID}")]
        public async Task<ActionResult<Canco>> GetCanco(string ID)
        {
            var canco = await _context.Cancons.FindAsync(ID);

            if (canco == null)
            {
                return NotFound();
            }

            return canco;
        }

        
        /// <summary>
        /// Accedeix a la ruta /api/Canco/getCanco/{ID} per modificar una canco
        /// </summary>
        /// <returns>Task<IActionResult> El resultat de l accio</returns>
        [HttpPut("putLlista/{ID}")]
        public async Task<IActionResult> PutCanco(string ID, Canco canco)
        {
            if (ID != canco.ID)
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
                if (!CancoExists(ID))
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

        
        /// <summary>
        /// Accedeix a la ruta /api/Canco/postCanco per inserir una canco
        /// </summary>
        /// <returns>Task<ActionResult<Canco>></returns>
        [HttpPost("postCanco")]
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

            return CreatedAtAction("GetCanco", new { ID = canco.ID }, canco);
        }

        
        /// <summary>
        /// Accedeix a la ruta /api/Canco/deleteCanco/{ID} per eliminar una canco
        /// </summary>
        /// <returns>Task<IActionResult></returns>
        [HttpDelete("deleteCanco/{ID}")]
        public async Task<IActionResult> DeleteCanco(string ID)
        {
            var canco = await _context.Cancons.FindAsync(ID);
            if (canco == null)
            {
                return NotFound();
            }

            _context.Cancons.Remove(canco);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Comprova si existeix una canco
        /// </summary>
        /// <param name="ID">Identificador de la Canco</param>
        /// <returns>bool</returns>
        private bool CancoExists(string ID)
        {
            return _context.Cancons.Any(e => e.ID == ID);
        }
    }
}
