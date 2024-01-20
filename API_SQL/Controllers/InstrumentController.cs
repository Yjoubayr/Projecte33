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
    public class InstrumentController : ControllerBase
    {
        private readonly DataContext _context;

        public InstrumentController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Instrument
        [HttpGet("getInstruments")]
        public async Task<ActionResult<IEnumerable<Instrument>>> GetInstrument()
        {
            return await _context.Instrument.ToListAsync();
        }

        // GET: api/Instrument/5
        [HttpGet("getInstrument/{Nom}")]
        public async Task<ActionResult<Instrument>> GetInstrument(string id)
        {
            var instrument = await _context.Instrument.FindAsync(id);

            if (instrument == null)
            {
                return NotFound();
            }

            return instrument;
        }

        /// <summary>
        /// Accedeix a la ruta /api/Instrument/putInstrument/{Nom} per modificar un instrument
        /// </summary>
        /// <param name="Nom">Nom del instrument a modificar</param>
        /// <param name="updatedInstrument">L'objecte del instrument a modificar</param>
        /// <returns></returns>
        [HttpPut("putInstrument/{Nom}")]
        public async Task<IActionResult> PutInstrument(string Nom, Instrument updatedInstrument)
        {
            if (id != instrument.Nom)
            {
                return BadRequest();
            }

            _context.Entry(instrument).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InstrumentExists(id))
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
        /// Accedeix a la ruta /api/Instrument/postInstrument per crear un instrument
        /// </summary>
        /// <param name="instrument">L'objecte del instrument a crear</param>
        /// <returns>Verificació que el instrument s'ha creat correctament</returns>
        [HttpPost("postInstrument")]
        public async Task<ActionResult<Instrument>> PostInstrument(Instrument instrument)
        {
            _context.Instrument.Add(instrument);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (InstrumentExists(instrument.Nom))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetInstrument", new { id = instrument.Nom }, instrument);
        }

        /// <summary>
        /// Accedeix a la ruta /api/Instrument/deleteInstrument/{Nom} per eliminar un instrument
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("deleteInstrument/{Nom}")]
        public async Task<IActionResult> DeleteInstrument(string Nom)
        {
            var instrument = await _context.Instrument.FindAsync(id);
            if (instrument == null)
            {
                return NotFound();
            }

            _context.Instrument.Remove(instrument);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool InstrumentExists(string id)
        {
            return _context.Instrument.Any(e => e.Nom == id);
        }
    }
}
