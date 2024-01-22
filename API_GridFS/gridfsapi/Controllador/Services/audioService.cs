using System.IO;
using System.Threading.Tasks;
using gridfsapi.Model;
using Microsoft.AspNetCore.Http;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;

namespace gridfsapi.Services
{
    public class AudioService
    {
        private readonly IMongoDatabase _database;
        private readonly GridFSBucket _gridFs;

        public AudioService(IMongoDatabase database)
        {
            _database = database;
            _gridFs = new GridFSBucket(_database);
        }

        public async Task<ObjectId> UploadSongAsync(Stream fileStream, string fileName, string idCanco)
        {
            var options = new GridFSUploadOptions
            {
                Metadata = new BsonDocument
                {
                    { "IdCanco", idCanco }
                }
            };

            return await _gridFs.UploadFromStreamAsync(fileName, fileStream, options);
        }

        public async Task<Audio> DownloadSongAsync(ObjectId fileId)
        {
            var fileStream = await _gridFs.OpenDownloadStreamAsync(fileId);
            if (fileStream == null)
            {
                return null;
            }

            using (var memoryStream = new MemoryStream())
            {
                await fileStream.CopyToAsync(memoryStream);

                return new Audio
                {
                    IdCanco = fileStream.FileInfo.Metadata["IdCanco"].AsString,
                    Contingut = memoryStream.ToArray()
                };
            }
        }
    }
}
