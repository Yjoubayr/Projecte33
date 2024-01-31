using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using dymj.ReproductorMusica.API_SQL.Data;
using dymj.ReproductorMusica.API_SQL.Services;
using dymj.ReproductorMusica.API_SQL.Model;

namespace API_SQL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TocarController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly TocarService _tocarService;
        private readonly CancoService _cancoService;
        private readonly MusicService _musicService;
        private readonly GrupService _grupService;
        private readonly InstrumentService _instrumentService;

        public TocarController(DataContext context)
        {
            _context = context;
            _tocarService = new TocarService(context);
            _cancoService = new CancoService(context);
            _musicService = new MusicService(context);
            _grupService = new GrupService(context);
            _instrumentService = new InstrumentService(context);
        }

        
        /// <summary>
        /// Accedeix a la ruta /api/Tocar/getAllTocar per obtenir tots els registres de la classe Tocar
        /// </summary>
        /// <returns>Una llista de tots els registres de la classe Tocar</returns>
        [HttpGet("getAllTocar")]
        public async Task<ActionResult<IEnumerable<Tocar>>> GetAllTocar()
        {
            return await _tocarService.GetAsync();
        }


        /// <summary>
        /// Accedeix a la ruta /api/Tocar/getTocar/{IDCanco}/{NomMusic}/{NomGrup}/{NomInstrument} 
        /// per obtenir un registre de la classe Tocar
        /// </summary>
        /// <param name="IDCanco">Identificador de la Canco del registre Tocar a consultar</param>
        /// <param name="NomMusic">Nom de la Music del registre Tocar a consultar</param>
        /// <param name="NomGrup">Nom del Grup del registre Tocar a consultar</param>
        /// <param name="NomInstrument">Nom de l'Instrument del registre Tocar a consultar</param>
        /// <returns>L'objecte de la Llista de reproduccio consultada</returns>
        [HttpGet("getTocar/{IDCanco}/{NomMusic}/{NomGrup}/{NomInstrument}")]
        public async Task<ActionResult<Tocar>> GetTocar(string IDCanco, string NomMusic, string NomGrup, string NomInstrument)
        {
            var tocar = await _tocarService.GetAsync(IDCanco, NomMusic, NomGrup, NomInstrument);

            if (tocar == null)
            {
                return NotFound();
            }

            return tocar;
        }

        /// <summary>
        /// Accedeix a la ruta /api/Tocar/postTocar per crear un registre de la classe Tocar
        /// </summary>
        /// <param name="tocar">L'objecte de la classe Tocar a crear</param>
        /// <returns>Verificacio de que el registre de la classe Tocar s'ha creat correctament</returns>
        [HttpPost("postTocar")]
        public async Task<IActionResult> PostTocar(Tocar tocar)
        {
            // Considerar la possibilitat de comprovar prèviament si existeix el nom de la canco i retornar un error 409
            IActionResult result;

            if (tocar.CancoObj != null 
            || tocar.MusicObj != null 
            || tocar.InstrumentObj != null) {
                return BadRequest();
            }

            List<Tocar> lTocar = await _tocarService.GetAsync();

            if (lTocar.Any(x => x.IDCanco == tocar.IDCanco 
            && x.NomMusic == tocar.NomMusic 
            && x.NomGrup == tocar.NomGrup 
            && x.NomInstrument == tocar.NomInstrument)) {
                return Conflict();
            }

            if (_cancoService.GetAsync(tocar.IDCanco) == null
            || _musicService.GetAsync(tocar.NomMusic) == null
            || _instrumentService.GetAsync(tocar.NomInstrument) == null){
                return NotFound();
            }

            await _tocarService.CreateAsync(tocar);
            result = CreatedAtAction("GetTocar", new { IDCanco = tocar.IDCanco, NomMusic = tocar.NomMusic, NomGrup = tocar.NomGrup, NomInstrument = tocar.NomInstrument }, tocar);
            return result;
        }

        /// <summary>
        /// Accedeix a la ruta /api/Tocar/deleteTocar/{IDCanco}/{NomMusic}/{NomGrup}/{NomInstrument} per eliminar un registre de la classe Tocar
        /// </summary>
        /// <param name="IDCanco">Identificador de la Canco del registre Tocar a eliminar</param>
        /// <param name="NomMusic">Nom de la Music del registre Tocar a eliminar</param>
        /// <param name="NomGrup">Nom del Grup del registre Tocar a eliminar</param>
        /// <param name="NomInstrument">Nom de l'Instrument del registre Tocar a eliminar</param>
        /// <returns>Verificacio de que el registre de la classe Tocar s'ha eliminat correctament</returns>
        [HttpDelete("deleteTocar/{IDCanco}/{NomMusic}/{NomGrup}/{NomInstrument}")]
        public async Task<IActionResult> DeleteTocar(string IDCanco, string NomMusic, string NomGrup, string NomInstrument)
        {
            var tocar = await _tocarService.GetAsync(IDCanco, NomMusic, NomGrup, NomInstrument);
            if (tocar == null)
            {
                return NotFound();
            }

            await _tocarService.RemoveAsync(tocar);

            return NoContent();
        }

    }
}
