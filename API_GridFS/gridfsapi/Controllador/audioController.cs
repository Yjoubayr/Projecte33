using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System.IO;
using System.Threading.Tasks;
using gridfsapi.Services;

namespace gridfsapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AudioController : ControllerBase
    {
        private readonly AudioService _mongoDbManager;

        public AudioController(AudioService mongoDbManager)
        {
            _mongoDbManager = mongoDbManager;
        }

        [HttpPost]
        public async Task<IActionResult> UploadAudio([FromForm] IFormFile file, [FromForm] string idCanco)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("Archivo no válido");
            }

            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                var fileId = await _mongoDbManager.UploadSongAsync(memoryStream, file.FileName, idCanco);
                return Ok(new { FileId = fileId.ToString() });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> DownloadAudio(string id)
        {
            if (ObjectId.TryParse(id, out ObjectId fileId))
            {
                var audio = await _mongoDbManager.DownloadSongAsync(fileId);

                if (audio != null)
                {
                    return File(audio.Contingut, "audio/mpeg", $"{audio.IdCanco}.mp3");
                }
                else
                {
                    return NotFound("Canción no encontrada");
                }
            }
            else
            {
                return BadRequest("Id de archivo no válido");
            }
        }
    }
}
