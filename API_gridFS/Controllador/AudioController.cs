using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using System.IO;
using System.Threading.Tasks;
using gridfsapi;

namespace gridfsapi;


/// <summary>
/// Controlador per a les operacions relacionades amb els fitxers d'àudio.
/// </summary>
[Route("FitxersAPI/v1/[controller]")]
[ApiController]
public class AudioController : ControllerBase
{
    private readonly AudioService _SongService;

    /// <summary>
    /// Constructor de la classe AudioController.
    /// </summary>
    /// <param name="SongService">Servei per a les operacions relacionades amb els fitxers d'àudio.</param>
    public AudioController(AudioService SongService) =>
        _SongService = SongService;

    /// <summary>
    /// Obté tots els fitxers d'àudio.
    /// </summary>
    /// <returns>Llista de fitxers d'àudio.</returns>
    [HttpGet]
    public async Task<ActionResult<List<Audio>>> GetAll()
    {
        var songs = await _SongService.GetAsync();
        if (songs is null)
        {
            return NotFound();
        }
        return songs;
    }

    /// <summary>
    /// Obté un fitxer d'àudio pel seu identificador.
    /// </summary>
    /// <param name="_ID">Identificador del fitxer d'àudio.</param>
    /// <returns>Fitxer d'àudio.</returns>
    [HttpGet("{_ID}")]
    public async Task<ActionResult<Audio>> GetSong(string _ID)
    {
        var song = await _SongService.GetAsync(_ID);

        if (song is null)
        {
            return NotFound();
        }

        return song;
    }

    /// <summary>
    /// Obté l'àudio d'un fitxer d'àudio pel seu identificador.
    /// </summary>
    /// <param name="UID">Identificador del fitxer d'àudio.</param>
    /// <returns>Resposta HTTP amb l'àudio del fitxer.</returns>
    [HttpGet("GetAudio/{UID}")]
    public async Task<IActionResult> GetAudio(string UID)
    {
        try
        {
            var song = await _SongService.GetByAudioIDAsync(UID);

            if (song == null)
            {
                return NotFound("No existe la canción");
            }

            var audioStream = await _SongService.GetAudioStreamAsync(song.AudioFileId);

            if (audioStream == null)
            {
                return Conflict("No se ha podido recuperar el archivo de audio");
            }

            return File(audioStream, "audio/mp3", $"audio_{UID}.mp3");
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Message = "Error interno del servidor.", Error = ex.Message });
        }
    }

    /// <summary>
    /// Pujar una cançó i el seu fitxer d'àudio associat.
    /// </summary>
    /// <param name="songModel">Dades de la cançó i el fitxer d'àudio.</param>
    /// <returns>Resposta HTTP amb el missatge de confirmació.</returns>
    [HttpPost]
    public async Task<IActionResult> UploadSong([FromForm] AudioUpload songModel)
    {
        try
        {
            var song = new Audio
            {
                IdCanco_SQL = songModel.Uid
            };

            // Subir el archivo de audio a GridFS
            var uploadOptions = new GridFSUploadOptions
            {
                Metadata = new BsonDocument("contentType", songModel.Audio.ContentType)
            };

            using (var stream = songModel.Audio.OpenReadStream())
            {
                var fileId = await _SongService.UploadAudioAsync(songModel.Audio.FileName, stream, uploadOptions);
                song.AudioFileId = fileId;
            }

            await _SongService.CreateAsync(song);

            return Ok(new { Message = "Canción y archivo de audio subidos con éxito." });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Message = "Error interno del servidor.", Error = ex.Message });
        }
    }

    /// <summary>
    /// Actualitza un fitxer d'àudio existent.
    /// </summary>
    /// <param name="_ID">Identificador del fitxer d'àudio.</param>
    /// <param name="updatedSong">Dades actualitzades del fitxer d'àudio.</param>
    /// <returns>Resposta HTTP sense contingut.</returns>
    [HttpPut("{_ID}")]
    public async Task<IActionResult> Update(string _ID, Audio updatedSong)
    {
        var song = await _SongService.GetAsync(_ID);
        if (song is null)
        {
            return NotFound();
        }
        updatedSong.IdCanco_SQL = song.IdCanco_SQL;
        await _SongService.UpdateAsync(_ID, updatedSong);
        return NoContent();
    }
}
