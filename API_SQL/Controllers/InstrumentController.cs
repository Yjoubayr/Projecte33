using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using dymj.ReproductorMusica.API_SQL.Data;
using dymj.ReproductorMusica.API_SQL.Model;
using dymj.ReproductorMusica.API_SQL.Services;

namespace dymj.ReproductorMusica.API_SQL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstrumentController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly InstrumentService _instrumentService;

        public InstrumentController(DataContext context)
        {
            _context = context;
            _instrumentService = new InstrumentService(context);
        }

        /// <summary>
        /// Accedeix a la ruta /api/Instrument/getInstruments per obtenir tots els instruments
        /// </summary>
        /// <returns>Una llista de tots els instruments</returns>
        [HttpGet("getInstruments")]
        public async Task<ActionResult<IEnumerable<Instrument>>> GetInstrument()
        {
            return await _instrumentService.GetAsync();
        }

        /// <summary>
        /// Accedeix a la ruta /api/Instrument/getInstrument/{Nom} per obtenir un instrument
        /// </summary>
        /// <param name="Nom">Nom de l'instrument a consultar</param>
        /// <returns>L'objecte de l'instrument consultat</returns>
        [HttpGet("getInstrument/{Nom}")]
        public async Task<ActionResult<Instrument>> GetInstrument(string Nom)
        {
            var instrument = await _instrumentService.GetAsync(Nom);

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
        /// <returns>Verificacio que el instrument s'ha modificat correctament</returns>
        [HttpPut("putInstrument/{Nom}")]
        public async Task<IActionResult> PutInstrument(string Nom, Instrument updatedInstrument)
        {
            if (Nom != updatedInstrument.Nom)
            {
                return BadRequest();
            }

            _context.Entry(updatedInstrument).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InstrumentExists(Nom))
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
        /// <returns>Verificacio de que el instrument s'ha creat correctament</returns>
        [HttpPost("postInstrument")]
        public async Task<IActionResult> PostInstrument(Instrument instrument)
        {
            // Considerar la possibilitat de comprovar previament si existeix el nom del music i retornar un error 409
            IActionResult result;
            try
            {
                await _instrumentService.CreateAsync(instrument);
                result = CreatedAtAction("GetInstrument", new { Nom = instrument.Nom }, instrument);
            }
            catch (DbUpdateException)
            {
                if (_instrumentService.GetAsync(instrument.Nom) != null)
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

        /// <summary>
        /// Accedeix a la ruta /api/Instrument/deleteInstrument/{Nom} per eliminar un instrument
        /// </summary>
        /// <param name="Nom">Nom del instrument a eliminar</param>
        /// <returns>Verificacio de que el instrument s'ha eliminat correctament</returns>
        [HttpDelete("deleteInstrument/{Nom}")]
        public async Task<IActionResult> DeleteInstrument(string Nom)
        {
            var instrument = await _instrumentService.GetAsync(Nom);
            if (instrument is null)
            {
                return NotFound();
            }

            await _instrumentService.RemoveAsync(instrument);

            return NoContent();
        }

        private bool InstrumentExists(string id)
        {
            return _context.Instruments.Any(e => e.Nom == id);
        }
    }
}
