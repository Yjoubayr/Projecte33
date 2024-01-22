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
        /// Accedeix a la ruta /api/Music/getMusics per obtenir tots els músics
        /// </summary>
        /// <returns>Una llista de tots els musics</returns>
        [HttpGet("getMusics")]
        public async Task<ActionResult<IEnumerable<Music>>> GetMusics()
        {
            return await _musicService.GetAsync();
        }

        /// <summary>
        /// Accedeix a la ruta /api/Music/getMusic/{Nom} per obtenir un músic
        /// </summary>
        /// <param name="Nom">Nom del músic a consultar</param>
        /// <returns>L'objecte del músic consultat</returns>
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
        /// Accedeix a la ruta /api/Music/putMusic/{Nom} per modificar un músic
        /// </summary>
        /// <param name="Nom">Nom del músic a modificar</param>
        /// <param name="updatedMusic">L'objecte del músic a modificar</param>
        /// <returns>Verificació que el músic s'ha modificat correctament</returns>
        [HttpPut("putMusic/{Nom}")]
        public async Task<IActionResult> PutMusic(string Nom, Music updatedMusic)
        {
            var music = await _musicService.GetAsync(Nom);

            if (music is null)
            {
                return NotFound();
            }
            updatedMusic.Nom = music.Nom;

            await _musicService.UpdateAsync(updatedMusic);

            return NoContent();
        }

       /// <summary>
       /// Accedeix a la ruta /api/Music/postMusic per crear un músic
       /// </summary>
       /// <param name="music">L'objecte del músic a modificar</param>
       /// <returns>Verificació que el músic s'ha creat correctament</returns>
        [HttpPost("postMusic")]
        public async Task<IActionResult> PostMusic(Music music)
        {
            // Considerar la possibilitat de comprovar prèviament si existeix el nom del music i retornar un error 409
            IActionResult result;
            try
            {
                await _musicService.CreateAsync(music);
                result = CreatedAtAction("GetMusic", new { Nom = music.Nom }, music);
            }
            catch (DbUpdateException)
            {
                if (_musicService.GetAsync(music.Nom) != null)
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
        /// Accedeix a la ruta /api/Music/deleteMusic/{Nom} per eliminar un músic
        /// </summary>
        /// <param name="Nom">Nom del músic a eliminar</param>
        /// <returns>Verificació que el músic s'ha eliminat correctament</returns>
        [HttpDelete("deleteMusic/{Nom}")]
        public async Task<IActionResult> DeleteMusic(string Nom)
        {
            var music = await _musicService.GetAsync(Nom);
            if (music is null)
            {
                return NotFound();
            }

            await _musicService.RemoveAsync(music);

            return NoContent();
        }
    }
}
