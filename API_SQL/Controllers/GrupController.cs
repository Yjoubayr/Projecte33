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
            return await _grupService.GetAsync();
        }

        /// <summary>
        /// Accedeix a la ruta /api/Grup/getGrup/{Nom} per obtenir un Grup
        /// </summary>
        /// <param name="Nom">Nom del Grup a consultar</param>
        /// <returns>L'objecte del Grup consultat</returns>
        [HttpGet("getGrup/{Nom}")]
        public async Task<ActionResult<Grup>> GetGrup(string Nom)
        {
            var grup = await _grupService.GetAsync(Nom);

            if (grup == null)
            {
                return NotFound();
            }

            return grup;
        }

        /// <summary>
        /// Accedeix a la ruta /api/Grup/putGrup/{Nom} per modificar un grup
        /// </summary>
        /// <param name="Nom">Nom del Grup a modificar</param>
        /// <param name="grup">Objecte del Grup a modificar</param>
        /// <returns>Verificacio de que el Grup s'ha modificat correctament</returns>
        [HttpPut("putGrup/{Nom}")]
        public async Task<IActionResult> PutGrup(string Nom, Grup updatedGrup)
        {
            var grup = await _grupService.GetAsync(Nom);

            if (grup is null)
            {
                return NotFound();
            }

            updatedGrup.Nom = grup.Nom;

            await _grupService.UpdateAsync(Nom, updatedGrup);

            return NoContent();
        }

        /// <summary>
        /// Accedeix a la ruta /api/Grup/postGrup per crear un grup
        /// </summary>
        /// <param name="grup">L'objecte del Grup a crear</param>
        /// <returns>Verificacio de que el Grup s'ha creat correctament</returns>
        [HttpPost("postGrup")]
        public async Task<ActionResult<Grup>> PostGrup(Grup grup)
        {
            // Considerar la possibilitat de comprovar previament si existeix el nom del grup i retornar un error 409
            IActionResult result;

            try
            {
                await _cancoService.CreateAsync(canco);
                result = CreatedAtAction("GetGrup", new { Nom = grup.Nom }, grup);
            }
            catch (DbUpdateException)
            {
                if (_grupService.GetAsync(grup.Nom) == null)
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
        /// Accedeix a la ruta /api/Grup/deleteGrup/{Nom} per eliminar un grup
        /// </summary>
        /// <param name="Nom">Nom del Grup a eliminar</param>
        /// <returns>Verificacio de que el Grup s'ha eliminat correctament</returns>
        [HttpDelete("deleteGrup/{Nom}")]
        public async Task<IActionResult> DeleteGrup(string Nom)
        {
            var grup = await _grupService.GetAsync(Nom);
            
            if (grup is null)
            {
                return NotFound();
            }

            await _grupService.RemoveAsync(grup);

            return NoContent();
        }

        private bool GrupExists(string id)
        {
            return _context.Grups.Any(e => e.Nom == id);
        }
    }
}
