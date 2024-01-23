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
    public class CancoController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly CancoService _cancoService;

        /// <summary>
        /// Constructor de la classe CancoController
        /// </summary>
        /// <param name="context">Contexte de dades utilitzat per a accedir a la base de dades.</param>
        public CancoController(DataContext context)
        {
            _context = context;
            _cancoService = new CancoService(context);
        }

        /// <summary>
        /// Accedeix a la ruta /api/Canco/getCancons per obtenir totes les cancons
        /// </summary>
        /// <returns>Una llista de totes les cancons</returns>
        [HttpGet("getCancons")]
        public async Task<ActionResult<IEnumerable<Canco>>> GetCancons()
        {        
            return await _cancoService.GetAsync();
        }


        /// <summary>
        /// Accedeix a la ruta /api/Canco/getCanco/{IDCanco} per obtenir una canco
        /// </summary>
        /// <param name="IDCanco">Identifiador de la Canco a consultar</param>
        /// <returns>L'objecte de la Canco consultada</returns>
        [HttpGet("getCanco/{IDCanco}")]
        public async Task<ActionResult<Canco>> GetCanco(string IDCanco)
        {
            var canco = await _cancoService.GetAsync(IDCanco);

            if (canco == null)
            {
                return NotFound();
            }

            return canco;
        }

        
        /// <summary>
        /// Accedeix a la ruta /api/Canco/getCanco/{IDCanco} per modificar una canco
        /// </summary>
        /// <param name="IDCanco">Identifiador de la Canco a modificar</param>
        /// <param name="extensio">Extensio de la Canco a modificar</param>
        /// <param name="updatedCanco">L'objecte de la Canco a modificar</param>
        /// <returns>Verificacio de que la Canco s'ha modificat correctament</returns>
        [HttpPut("putCanco/{IDCanco}/{extensio}")]
        public async Task<IActionResult> PutCanco(string IDCanco, string extensio, Canco updatedCanco)
        {
            var canco = await _cancoService.GetAsync(IDCanco);

            if (canco == null || IDCanco != updatedCanco.IDCanco)
            {
                return NotFound();
            }

            await _cancoService.UpdateAsync(extensio, canco, updatedCanco);

            return NoContent();
        }

        
        /// <summary>
        /// Accedeix a la ruta /api/Canco/postCanco per inserir una canco
        /// </summary>
        /// <param name="extensio">El nom de l'extensio de la Canco a modificar</param>
        /// <param name="canco">L'objecte de la Canco a modificar</param>
        /// <returns>Verificacio de que la Canco s'ha creat correctament</returns>
        [HttpPost("postCanco/{extensio}")]
        public async Task<IActionResult> PostCanco(string extensio, Canco canco)
        {
            // Considerar la possibilitat de comprovar prèviament si existeix el nom de la canco i retornar un error 409
            IActionResult result;

            try
            {
                await _cancoService.CreateAsync(extensio, canco);
                result = CreatedAtAction("GetCanco", new { IDCanco = canco.IDCanco }, canco);
            }
            catch (DbUpdateException)
            {
                if (extensio.Length > 5) {
                    return Conflict();
                }
                if (_cancoService.GetAsync(canco.IDCanco) == null)
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
        /// Accedeix a la ruta /api/Canco/deleteCanco/{IDCanco} per eliminar una canco
        /// </summary>
        /// <param name="IDCanco">Identifiador de la Canco a eliminar</param>
        /// <returns>Verificacio de que la Canco s'ha eliminat correctament</returns>
        [HttpDelete("deleteCanco/{IDCanco}")]
        public async Task<IActionResult> DeleteCanco(string IDCanco)
        {
            var canco = await _cancoService.GetAsync(IDCanco);
            
            if (canco == null)
            {
                return NotFound();
            }

            await _cancoService.RemoveAsync(canco);

            return NoContent();
        }

    }
}
