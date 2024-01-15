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

        public CancoController(DataContext context, CancoService cancoService)
        {
            _context = context;
            _cancoService = cancoService;
        }

        /// <summary>
        /// Accedeix a la ruta /api/Canco/getCancons per obtenir totes les cancons
        /// </summary>
        /// <returns>Un array de totes les cancons</returns>
        [HttpGet("getCancons/start/{start}/limit/{limit}")]
        public async Task<ActionResult<IEnumerable<Canco>>> GetCancons()
        {        
            return await _cancoService.GetAsync();
        }


        /// <summary>
        /// Accedeix a la ruta /api/Canco/getCanco/{ID} per obtenir una canco
        /// </summary>
        /// <returns>L objecte de la canco consultada</returns>
        [HttpGet("getCanco/{ID}")]
        public async Task<ActionResult<Canco>> GetCanco(string ID)
        {
            var canco = await _cancoService.GetAsync(ID);

            if (canco == null)
            {
                return NotFound();
            }

            return canco;
        }

        
        /// <summary>
        /// Accedeix a la ruta /api/Canco/getCanco/{ID} per modificar una canco
        /// </summary>
        /// <returns>La verificacio de que s ha modificat correctament</returns>
        [HttpPut("putLlista/{ID}")]
        public async Task<IActionResult> PutCanco(string ID, Canco updatedCanco)
        {
            var canco = await _cancoService.GetAsync(ID);

            if (canco is null)
            {
                return NotFound();
            }

            updatedCanco.ID = canco.ID;

            await _cancoService.UpdateAsync(ID, updatedCanco);

            return NoContent();
        }

        
        /// <summary>
        /// Accedeix a la ruta /api/Canco/postCanco per inserir una canco
        /// </summary>
        /// <returns>La verificacio de que s ha inserit correctament</returns>
        [HttpPost("postCanco")]
        public async Task<IActionResult> PostCanco(Canco canco)
        {
            // Considerar la possibilitat de comprovar prèviament si existeix el nom de la llibreria i retornar un error 409
            IActionResult result;

            try
            {
                await _cancoService.CreateAsync(canco);
                result = CreatedAtAction("GetParaula", new { ID = canco.ID }, canco);
            }
            catch (DbUpdateException ex)
            {
                if (_cancoService.GetAsync(canco.ID) == null)
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
        /// Accedeix a la ruta /api/Canco/deleteCanco/{ID} per eliminar una canco
        /// </summary>
        /// <returns>La verificacio de que s ha eliminat correctament</returns>
        [HttpDelete("deleteCanco/{ID}")]
        public async Task<IActionResult> DeleteCanco(string ID)
        {
            var canco = await _cancoService.GetAsync(ID);
            
            if (canco is null)
            {
                return NotFound();
            }

            await _cancoService.RemoveAsync(ID);

            return NoContent();
        }

    }
}
