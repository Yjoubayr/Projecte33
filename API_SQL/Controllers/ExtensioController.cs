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

namespace API_SQL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExtensioController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly ExtensioService _extensioService;
        private readonly CancoService _cancoService;

        public ExtensioController(DataContext context)
        {
            _context = context;
            _extensioService = new ExtensioService(context);
            _cancoService = new CancoService(context);
        }

        /// <summary>
        /// Accedeix a la ruta /api/Extensio/getExtensions per obtenir totes les extensions
        /// </summary>
        /// <returns>Una llista de totes les extensions</returns>
        [HttpGet("getExtensions")]
        public async Task<ActionResult<IEnumerable<Extensio>>> GetExtensions()
        {
            return await _extensioService.GetAsync();
        }

        /// <summary>
        /// Accedeix a la ruta /api/Extensio/getExtensio/{Nom} per obtenir una extensio
        /// </summary>
        /// <param name="Nom">Nom de la extensio a consultar</param>
        /// <returns>L'objecte de la extensio consultada</returns>
        [HttpGet("getExtensio/{Nom}")]
        public async Task<ActionResult<Extensio>> GetExtensio(string Nom)
        {
            var extensio = await _extensioService.GetAsync(Nom);

            if (extensio == null)
            {
                return NotFound();
            }

            return extensio;
        }

        /// <summary>
        /// Accedeix a la ruta /api/Extensio/updateCanco/{IDCanco} per modificar una extensio
        /// </summary>
        /// <param name="IDCanco">Identifiador de la Canco a modificar</param>
        /// <param name="updatedCanco">Objecte de la canco amb els elements modificats</param>
        /// <returns>L'objecte de la extensio consultada</returns>
        [HttpPut("updateCanco/{IDCanco}")]
        public async Task<IActionResult> updateCanco(string IDCanco, Canco updatedCanco)
        {
            // Considerar la possibilitat de comprovar prèviament si existeix el nom de la canco i retornar un error 409
            IActionResult result;

            if (updatedCanco.LExtensions == null) {
                return BadRequest();
            }

            var canco = await _cancoService.GetAsync(IDCanco);

            if (canco == null || IDCanco != updatedCanco.IDCanco)
            {
                return NotFound();
            }

            await _extensioService.UpdateCancoRemoveAsync(canco, updatedCanco);
            await _extensioService.UpdateCancoAddAsync(canco, updatedCanco);
            return Ok();
        }

        /// <summary>
        /// Accedeix a la ruta /api/Extensio/postExtensio per crear una extensio
        /// </summary>
        /// <param name="extensio">L'objecte de la extensio a crear</param>
        /// <returns>Verificacio de que la extensio s'ha creat correctament</returns>
        [HttpPost]
        public async Task<ActionResult<Extensio>> PostExtensio(Extensio extensio)
        {
            _context.Extensions.Add(extensio);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ExtensioExists(extensio.Nom))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetExtensio", new { id = extensio.Nom }, extensio);
        }

        private bool ExtensioExists(string id)
        {
            return _context.Extensions.Any(e => e.Nom == id);
        }
    }
}
