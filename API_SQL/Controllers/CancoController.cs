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
        private readonly LlistaService _llistaService;

        /// <summary>
        /// Constructor de la classe CancoController
        /// </summary>
        /// <param name="context">Contexte de dades utilitzat per a accedir a la base de dades.</param>
        public CancoController(DataContext context)
        {
            _context = context;
            _cancoService = new CancoService(context);
            _llistaService = new LlistaService(context);
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
        /// <param name="updatedCanco">L'objecte de la Canco a modificar</param>
        /// <returns>Verificacio de que la Canco s'ha modificat correctament</returns>
        [HttpPut("putCanco/{IDCanco}")]
        public async Task<IActionResult> PutCanco(string IDCanco, Canco updatedCanco)
        {
            // Considerar la possibilitat de comprovar prèviament si existeix el nom de la canco i retornar un error 409
            IActionResult result;

            var canco = await _cancoService.GetAsync(IDCanco);

            if (canco == null || IDCanco != updatedCanco.IDCanco)
            {
                return NotFound();
            }

            if (updatedCanco.LListes != null
            || updatedCanco.LVersions != null
            || updatedCanco.LAlbums != null
            || updatedCanco.LTocar != null
            || updatedCanco.LExtensions != null) {
                return BadRequest();
            }

            await _cancoService.UpdateAsync(canco, updatedCanco);
            result = CreatedAtAction("GetCanco", new { IDCanco = updatedCanco.IDCanco }, updatedCanco);

            return result;
        }

        /// <summary>
        /// Accedeix a la ruta /api/Canco/updateLlista/{MACAddress}/{NomLlista} per modificar una Llista de reproduccio
        /// </summary>
        /// <param name="MACAddress">Adreça MAC de la Llista de reproduccio a modificar</param>
        /// <param name="NomLlista">Nom de la Llista de reproduccio a modificar</param>
        /// <param name="updatedLlista">L'objecte de la Llista de reproduccio a modificar</param>
        /// <returns>Verificacio de que la Llista de reproduccio s'ha modificat correctament</returns>
        [HttpPut("updateLlista/{MACAddress}/{NomLlista}")]
        public async Task<IActionResult> updateLlista(string MACAddress, string NomLlista, Llista updatedLlista) 
        {
            if (updatedLlista == null) {
                return BadRequest();
            }

            var llista = await _llistaService.GetAsync(MACAddress, NomLlista);

            if (llista == null || MACAddress != updatedLlista.MACAddress || NomLlista != updatedLlista.NomLlista) {
                return NotFound();
            }

            await _cancoService.UpdateLlistaRemoveAsync(llista, updatedLlista);
            await _cancoService.UpdateLlistaAddAsync(llista, updatedLlista);
            return Ok();
        }

        /// <summary>
        /// Accedeix a la ruta /api/Canco/postCanco per inserir una canco
        /// </summary>
        /// <param name="canco">L'objecte de la Canco a modificar</param>
        /// <returns>Verificacio de que la Canco s'ha creat correctament</returns>
        [HttpPost("postCanco")]
        public async Task<IActionResult> PostCanco(Canco canco)
        {
            // Considerar la possibilitat de comprovar prèviament si existeix el nom de la canco i retornar un error 409
            IActionResult result;

            if (canco.LListes != null
            || canco.LVersions != null
            || canco.LAlbums != null 
            || canco.LTocar != null
            || canco.LExtensions != null) {
                return BadRequest();
            }

            await _cancoService.CreateAsync(canco);
            result = CreatedAtAction("GetCanco", new { IDCanco = canco.IDCanco }, canco);
            
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
