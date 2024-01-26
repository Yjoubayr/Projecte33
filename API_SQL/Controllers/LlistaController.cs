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
        private readonly CancoService _cancoService;

        /// <summary>
        /// Constructor de la classe LlistaController
        /// Tambe crearem un objecte de la classe LlistaService passant-li el contexte de dades
        /// </summary>
        /// <param name="context">Contexte de dades utilitzat per a accedir a la base de dades.</param>
        public LlistaController(DataContext context)
        {
            _context = context;
            _llistaService = new LlistaService(context);
            _cancoService = new CancoService(context);
        }

        /// <summary>
        /// Accedeix a la ruta /api/Llista/getLlistes per obtenir totes les llistes
        /// </summary>
        /// <returns>Una llista de totes les llistes de reproduccio</returns>
        [HttpGet("getLlistes")]
        public async Task<ActionResult<IEnumerable<Llista>>> GetLlista()
        {
            return await _llistaService.GetAsync();
        }

        /// <summary>
        /// Accedeix a la ruta /api/Llista/getLlista/{MACAddress}/{NomLlista} per obtenir una Llista de reproduccio
        /// </summary>
        /// <param name="MACAddress">MACAddress de la Llista de reproduccio a consultar</param>
        /// <param name="NomLlista">Nom de la Llista de reproduccio a consultar</param>
        /// <returns>L'objecte de la Llista de reproduccio consultada</returns>
        [HttpGet("getLlista/{MACAddress}/{NomLlista}")]
        public async Task<ActionResult<Llista>> GetLlista(string MACAddress, string NomLlista)
        {
            var llista = await _llistaService.GetAsync(MACAddress, NomLlista);

            if (llista == null)
            {
                return NotFound();
            }

            return llista;
        }

        /// <summary>
        /// Accedeix a la ruta /api/Llista/putLlista/{MACAddress}/{NomLlista} per modificar una Llista de reproduccio
        /// </summary>
        /// <param name="MACAddress">MACAddress de la Llista de reproduccio a modificar</param>
        /// <param name="NomLlista">Nom de la Llista de reproduccio a modificar</param>
        /// <param name="updatedLlista">L'objecte de la Llista de reproduccio a modificar</param>
        /// <returns>Verificacio de que la Llista de reproduccio s'ha modificat correctament</returns>
        [HttpPut("putLlista/{MACAddress}/{NomLlista}")]
        public async Task<IActionResult> PutLlista(string MACAddress, string NomLlista, Llista updatedLlista)
        {
            var llista = await _llistaService.GetAsync(MACAddress, NomLlista);
            
            if (llista == null || MACAddress != updatedLlista.MACAddress || NomLlista != updatedLlista.NomLlista)
            {
                return NotFound();
            }

            updatedLlista.MACAddress = llista.MACAddress;
            updatedLlista.NomLlista = llista.NomLlista;

            await _llistaService.UpdateAsync(updatedLlista);

            return NoContent();
        }


        /// <summary>
        /// Accedeix a la ruta /api/Llista/updateCanco/{IDCanco} per modificar una extensio
        /// </summary>
        /// <param name="IDCanco">Identifiador de la Canco a modificar</param>
        /// <param name="updatedCanco">Objecte de la canco amb els elements modificats</param>
        /// <returns>L'objecte de la extensio consultada</returns>
        [HttpPut("updateCanco/{IDCanco}")]
        public async Task<IActionResult> updateCanco(string IDCanco, Canco updatedCanco)
        {
            if (updatedCanco.LListes == null) {
                return BadRequest();
            }

            var canco = await _cancoService.GetAsync(IDCanco);

            if (canco == null || IDCanco != updatedCanco.IDCanco)
            {
                return NotFound();
            }

            await _llistaService.UpdateCancoRemoveAsync(canco, updatedCanco);
            await _llistaService.UpdateCancoAddAsync(canco, updatedCanco);
            return Ok();
        }


        /// <summary>
        /// Accedeix a la ruta /api/Llista/putLlista/{MACAddress}/{NomLlista}/{IDCanco} per afegir una canco a una Llista de reproduccio
        /// </summary>
        /// <param name="MACAddress">MACAddress de la Llista de reproduccio a modificar</param>
        /// <param name="NomLlista">Nom de la Llista de reproduccio a modificar</param>
        /// <param name="IDCanco">ID de la Canco a afegir a la Llista de reproduccio</param>
        /// <returns>Verificacio de que la Canco s'ha afegit a la Llista de reproduccio correctament</returns>
        [HttpPut("putLlistaCanco/{MACAddress}/{NomLlista}/{IDCanco}")]
        public async Task<IActionResult> PutLlistaCanco(string MACAddress, string NomLlista, string IDCanco)
        {
            var llista = await _llistaService.GetAsync(MACAddress, NomLlista);

            var canco = await _cancoService.GetAsync(IDCanco);

            if (llista == null || canco == null)
            {
                return NotFound();
            }

            await _llistaService.AddCancoAsync(llista.MACAddress, llista.NomLlista, canco);

            return NoContent();
        }

        /// <summary>
        /// Accedeix a la ruta /api/Llista/postLlista per crear una Llista de reproduccio
        /// </summary>
        /// <param name="llista">L'objecte de la Llista de reproduccio a crear</param>
        /// <returns>Verificacio de que la Llista de reproduccio s'ha creat correctament</returns>
        [HttpPost("postLlista")]
        public async Task<IActionResult> PostLlista(Llista llista)
        {
            // Considerar la possibilitat de comprovar previament si existeix el nom de la llista i retornar un error 409
            IActionResult result;

            try
            {
                await _llistaService.CreateAsync(llista);
                result = CreatedAtAction("GetLlista", new { MACAddress = llista.MACAddress, NomLlista = llista.NomLlista }, llista);
            }
            catch (DbUpdateException)
            {
                if (_llistaService.GetAsync(llista.MACAddress, llista.NomLlista) == null)
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
        /// Accedeix a la ruta /api/Llista/deleteLlista/{MACAddress}/{NomLlista} per eliminar una Llista de reproduccio
        /// </summary>
        /// <param name="MACAddress">MACAddress de la Llista de reproduccio a eliminar</param>
        /// <param name="NomLlista">Nom de la Llista de reproduccio a eliminar</param>
        /// <returns></returns>
        [HttpDelete("deleteLlista/{MACAddress}/{NomLlista}")]
        public async Task<IActionResult> DeleteLlista(string MACAddress, string NomLlista)
        {
            var llista = await _llistaService.GetAsync(MACAddress, NomLlista);

            if (llista == null)
            {
                return NotFound();
            }

            await _llistaService.RemoveAsync(llista);

            return NoContent();
        }

    }
}
