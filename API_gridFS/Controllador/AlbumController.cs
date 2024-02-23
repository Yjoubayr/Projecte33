using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using System.IO;
using System.Threading.Tasks;


namespace gridfsapi;

[Route("FitxersAPI/v1/[controller]")]
[ApiController]
public class AlbumController : ControllerBase
{
    private readonly AlbumService _albumService;

    public AlbumController(AlbumService albumService) =>
        _albumService = albumService;

    [HttpGet]
    public async Task<ActionResult<List<Album>>> GetAll()
    {
        var album = await _albumService.GetAsync();
        if (album is null)
        {
            return NotFound();
        }
        return album;
    }
    [HttpGet("{_ID}")]
    public async Task<ActionResult<Album>> GetSong(string _ID)
    {
        var album = await _albumService.GetAsync(_ID);

        if (album is null)
        {
            return NotFound();
        }

        return album;
    }

    [HttpGet("GetPortada/{albumName}/{year}")]
    public async Task<IActionResult> GetPortada(string albumName, int year)
    {
        try
        {
            var album = await _albumService.GetByAlbumNameAndYearAsync(albumName, year);

            if (album == null)
            {
                return NotFound("No existe la canción");
            }

            var albumStream = await _albumService.GetAlbumStreamAsync(album.ImatgePortadaId);

            if (albumStream == null)
            {
                return Conflict("No se ha podido recuperar el archivo de audio");
            }

            return File(albumStream, "image/png", $"contraportada_{albumName}_{year}.png");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }

    }


    [HttpGet("GetPortada/{albumName}/{year}")]
    public async Task<IActionResult> GetContraPortada(string albumName, int year)
    {
        try
        {
            var album = await _albumService.GetByAlbumNameAndYearAsync(albumName, year);

            if (album == null)
            {
                return NotFound("No existe la canción");
            }

            var albumStream = await _albumService.GetAlbumStreamAsync(album.ImatgeContraPortadaId);

            if (albumStream == null)
            {
                return Conflict("No se ha podido recuperar el archivo de audio");
            }

            return File(albumStream, "image/png", $"portada_{albumName}_{year}.png");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }

    }

    [HttpGet("GetPortadaUidSong/{UID}")]
    public async Task<IActionResult> GetPortadaUidSong(string UID)
    {
        try
        {
            var album = await _albumService.GetByAlbumIDAsync(UID);

            if (album == null)
            {
                return NotFound("No existe la canción");
            }

            var albumStream = await _albumService.GetAlbumStreamAsync(album.ImatgePortadaId);

            if (albumStream == null)
            {
                return Conflict("No se ha podido recuperar el archivo de audio");
            }

            return File(albumStream, "image/png", $"image_{UID}.png");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpGet("GetContraPortadaUidSong/{UID}")]
    public async Task<IActionResult> GetContraPortadaUidSong(string UID)
    {
        try
        {
            var album = await _albumService.GetByAlbumIDAsync(UID);

            if (album == null)
            {
                return NotFound("No existe la canción");
            }

            var albumStream = await _albumService.GetAlbumStreamAsync(album.ImatgeContraPortadaId);

            if (albumStream == null)
            {
                return Conflict("No se ha podido recuperar el archivo de audio");
            }

            return File(albumStream, "image/png", $"image_{UID}.png");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpPost]
    public async Task<IActionResult> UploadAlbum([FromForm] AlbumUpload albumModel)
    {
        try
        {
            var album = new Album
            {
                UIDSong = albumModel.UIDSong,
                Any = albumModel.Any,
                Genere = albumModel.Genere,
                Titol = albumModel.Titol
            };

            // Subir el archivo de audio a GridFS
            var uploadOptions = new GridFSUploadOptions
            {
                Metadata = new BsonDocument("contentType", albumModel.ImatgePortada.ContentType)

            };
            var uploadOptions2 = new GridFSUploadOptions
            {
                Metadata = new BsonDocument("contentType", albumModel.ImatgePortada.ContentType)

            };

            using (var stream = albumModel.ImatgePortada.OpenReadStream())
            {
                var fileId = await _albumService.UploadAlbumAsync(albumModel.ImatgePortada.FileName, stream, uploadOptions);
                album.ImatgePortadaId = fileId;
            }
            using (var stream = albumModel.ImatgeContraPortada.OpenReadStream())
            {
                var fileId = await _albumService.UploadAlbumAsync(albumModel.ImatgeContraPortada.FileName, stream, uploadOptions2);
                album.ImatgeContraPortadaId = fileId;
            }


            await _albumService.CreateAsync(album);

            return Ok(new { Message = "Canción y archivo de audio subidos con éxito." });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Message = "Error interno del servidor.", Error = ex.Message });
        }
    }
    [HttpPut("{_ID}")]
    public async Task<IActionResult> Update(string _ID, Album updatedAlbum)
    {
        var album = await _albumService.GetAsync(_ID);
        if (album is null)
        {
            return NotFound();
        }

        if (updatedAlbum.ImatgePortadaId != null)
        {
            album.ImatgePortadaId = updatedAlbum.ImatgePortadaId;
        }
        if (updatedAlbum.ImatgeContraPortadaId != null)
        {
            album.ImatgeContraPortadaId = updatedAlbum.ImatgeContraPortadaId;
        }

        updatedAlbum._ID = album._ID;
        await _albumService.UpdateAsync(_ID, updatedAlbum);
        return NoContent();
    }


}