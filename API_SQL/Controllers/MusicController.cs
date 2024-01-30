using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using dymj.ReproductorMusica.API_SQL.Model;
using dymj.ReproductorMusica.API_SQL.Data;
using dymj.ReproductorMusica.API_SQL.Services;

namespace dymj.ReproductorMusica.API_SQL.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class MusicController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly MusicService _musicService;

        public MusicController(DataContext context)
        {
            _context = context;
            _musicService = new MusicService(context);
        }

        /// <summary>
        /// Accedeix a la ruta /api/Music/getMusics per obtenir tots els musics
        /// </summary>
        /// <returns>Una llista de tots els musics</returns>
        [HttpGet("getMusics")]
        public async Task<ActionResult<IEnumerable<Music>>> GetMusics()
        {
            return await _musicService.GetAsync();
        }

        /// <summary>
        /// Accedeix a la ruta /api/Music/getMusic/{Nom} per obtenir un music
        /// </summary>
        /// <param name="Nom">Nom del music a consultar</param>
        /// <returns>L'objecte del music consultat</returns>
        [HttpGet("getMusic/{Nom}")]
        public async Task<ActionResult<Music>> GetMusic(string Nom)
        {
            var music = await _musicService.GetAsync(Nom);

            if (music == null)
            {
                return NotFound();
            }

            return music;
        }

        /// <summary>
        /// Accedeix a la ruta /api/Music/putMusic/{Nom} per modificar un music
        /// </summary>
        /// <param name="Nom">Nom del music a modificar</param>
        /// <param name="updatedMusic">L'objecte del music a modificar</param>
        /// <returns>Verificacio que el music s'ha modificat correctament</returns>
        [HttpPut("putMusic/{Nom}")]
        public async Task<IActionResult> PutMusic(string Nom, Music updatedMusic)
        {
            var music = await _musicService.GetAsync(Nom);

            if (music == null)
            {
                return NotFound();
            }
            updatedMusic.Nom = music.Nom;

            await _musicService.UpdateAsync(updatedMusic);

            return NoContent();
        }

        /// <summary>
        /// Accedeix a la ruta /api/Music/updateGrup/{Nom} per modificar un grup
        /// </summary>
        /// <param name="Nom">Nom del grup a modificar</param>
        /// <param name="updatedGrup">Objecte del grup amb els elements modificats</param>
        /// <returns>Verificacio de que el grup s'ha modificat correctament</returns>
        [HttpPut("updateGrup/{Nom}")]
        public async Task<IActionResult> updateGrup(string Nom, Grup updatedGrup)
        {
            // Considerar la possibilitat de comprovar prèviament si existeix el nom del music i retornar un error 409
            IActionResult result;

            var grup = await _grupService.GetAsync(Nom);

            if (grup == null || Nom != updatedGrup.Nom)
            {
                return NotFound();
            }

            await _musicService.UpdateGrupRemoveAsync(grup, updatedGrup);
            await _musicService.UpdateGrupAddAsync(_grupService, grup, updatedGrup);
            return Ok();
        }
        
        /// <summary>
        /// Accedeix a la ruta /api/Music/postMusic per crear un music
        /// </summary>
        /// <param name="music">L'objecte del music a modificar</param>
        /// <returns>Verificacio que el music s'ha creat correctament</returns>
        [HttpPost("postMusic")]
        public async Task<IActionResult> PostMusic(Music music)
        {
            // Considerar la possibilitat de comprovar previament si existeix el nom del music i retornar un error 409
            IActionResult result;

            if (music.LGrups != null) {
                return BadRequest();
            } 

            List<Music> lMusics = _musicService.GetAsync().Result.ToList<Music>();

            if (lMusics.Any(m => m.Nom == music.Nom))
            {
                return Conflict();
            }
            
            await _musicService.CreateAsync(music);
            result = CreatedAtAction("GetMusic", new { Nom = music.Nom }, music);

            return result;
        }

        /// <summary>
        /// Accedeix a la ruta /api/Music/deleteMusic/{Nom} per eliminar un music
        /// </summary>
        /// <param name="Nom">Nom del music a eliminar</param>
        /// <returns>Verificacio que el music s'ha eliminat correctament</returns>
        [HttpDelete("deleteMusic/{Nom}")]
        public async Task<IActionResult> DeleteMusic(string Nom)
        {
            var music = await _musicService.GetAsync(Nom);
            if (music == null)
            {
                return NotFound();
            }

            await _musicService.RemoveAsync(music);

            return NoContent();
        }
    }
}
