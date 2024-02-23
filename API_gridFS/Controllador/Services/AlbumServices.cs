using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;

namespace gridfsapi;

public class AlbumService
{
    private readonly IMongoCollection<Album> _albumCollection;
    private readonly GridFSBucket _gridFsBucket;

    public AlbumService(IOptions<DataContext> MongoStoreDatabaseSettings)
    {
        IMongoClient mongoClient = new MongoClient(MongoStoreDatabaseSettings.Value.ConnectionString);
        IMongoDatabase mongoDatabase = mongoClient.GetDatabase(MongoStoreDatabaseSettings.Value.DatabaseName);
        _albumCollection = mongoDatabase.GetCollection<Album>(MongoStoreDatabaseSettings.Value.AlbumCollectionName);
        _gridFsBucket = new GridFSBucket(mongoDatabase);
    }

    public async Task<Album?> GetByAlbumIDAsync(string UID) =>
            await _albumCollection.Find(x => x.UIDSong == UID).FirstOrDefaultAsync();

    public async Task<Album?> GetByAlbumNameAndYearAsync(string albumName, int year) =>
            (await _albumCollection.FindAsync(x => x.Titol == albumName && x.Any == year)).FirstOrDefault();

    public async Task<List<Album>> GetAsync() =>
            (await _albumCollection.FindAsync(Album => true)).ToList();

    public async Task<Album?> GetAsync(string id) =>
        await _albumCollection.Find(x => x._ID == id).FirstOrDefaultAsync();

    public async Task CreateAsync(Album newAlbum) =>
        await _albumCollection.InsertOneAsync(newAlbum);

    public async Task UpdateAsync(string id, Album updatedAlbum) =>
            await _albumCollection.ReplaceOneAsync(x => x._ID == id, updatedAlbum);

    public async Task<ObjectId> UploadAlbumAsync(string filename, Stream stream, GridFSUploadOptions options)
    {
        return await _gridFsBucket.UploadFromStreamAsync(filename, stream, options);
    }

    public async Task<Stream?> GetAlbumStreamAsync(ObjectId albumFileId)
    {
        try
        {
            var filter = Builders<GridFSFileInfo>.Filter.Eq("_id", albumFileId);
            var cursor = await _gridFsBucket.FindAsync(filter);

            // Esperar a que la tarea se complete antes de intentar acceder a los resultados
            await cursor.MoveNextAsync();

            var fileInfo = cursor.Current.FirstOrDefault();

            if (fileInfo == null)
            {
                return null;
            }

            var albumStream = await _gridFsBucket.OpenDownloadStreamAsync(albumFileId);
            return albumStream;
        }
        catch (Exception ex)
        {
            // Log de depuración
            Console.WriteLine($"Error in GetAlbumStreamAsync: {ex.Message}");
            return null;
        }
    }
}