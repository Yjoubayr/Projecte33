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
    public class GrupController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly GrupService _grupService;
        private readonly MusicService _musicService;

        /// <summary>
        /// Constructor de la classe GrupController
        /// Tambe crearem un objecte de la classe GrupService passant-li el contexte de dades
        /// </summary>
        /// <param name="context">Contexte de dades utilitzat per a accedir a la base de dades.</param>
        public GrupController(DataContext context)
        {
            _context = context;
            _grupService = new GrupService(context);
            _musicService = new MusicService(context);
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

            if (grup == null || Nom != updatedGrup.Nom)
            {
                return NotFound();
            }

            updatedGrup.Nom = grup.Nom;

            await _grupService.UpdateAsync(updatedGrup);

            return NoContent();
        }

        /// <summary>
        /// Accedeix a la ruta /api/Grup/updateMusic/{Nom} per modificar un Music
        /// </summary>
        /// <param name="Nom">Nom del Music a modificar</param>
        /// <param name="updatedMusic">Objecte del Music amb els elements modificats</param>
        /// <returns>Verificacio de que el Music s'ha modificat correctament</returns>
        [HttpPut("updateMusic/{Nom}")]
        public async Task<IActionResult> updateMusic(string Nom, Music updatedMusic)
        {
            
            if (updatedMusic.LGrups == null) {
                return BadRequest();
            }

            // Considerar la possibilitat de comprovar previament si existeix el nom del music i retornar un error 409
            IActionResult result;

            var music = await _musicService.GetAsync(Nom);

            if (music == null || Nom != updatedMusic.Nom)
            {
                return NotFound();
            }

            await _grupService.UpdateMusicRemoveAsync(music, updatedMusic);
            await _grupService.UpdateMusicAddAsync(music, updatedMusic);
            return Ok();
        }

        /// <summary>
        /// Accedeix a la ruta /api/Grup/postGrup per crear un grup
        /// </summary>
        /// <param name="grup">L'objecte del Grup a crear</param>
        /// <returns>Verificacio de que el Grup s'ha creat correctament</returns>
        [HttpPost("postGrup")]
        public async Task<IActionResult> PostGrup(Grup grup)
        {
            // Considerar la possibilitat de comprovar previament si existeix el nom del grup i retornar un error 409
            IActionResult result;

            if (grup.LMusics == null || grup.LMusics.Count < 1) {
                return BadRequest();
            }
            
            /*if (grup.LMusics != null) {
                return BadRequest();
            }

            List<Grup> lGrups = await _grupService.GetAsync();

            if (lGrups.Any(g => g.Nom == grup.Nom)) {
                return Conflict();
            }*/

            await _grupService.CreateAsync(grup);
            result = CreatedAtAction("GetGrup", new { Nom = grup.Nom }, grup);

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
            
            if (grup == null)
            {
                return NotFound();
            }

            await _grupService.RemoveAsync(grup);

            return NoContent();
        }
    }
}
