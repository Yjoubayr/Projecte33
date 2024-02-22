using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Driver.GridFS;  // Para GridFSUploadOptions


namespace gridfsapi;
/// <summary>
/// Servicio para gestionar operaciones relacionadas con audio.
/// </summary>
public class AudioService
{
    private readonly IMongoCollection<Audio> _songsCollection;
    private readonly GridFSBucket _gridFsBucket;

    /// <summary>
    /// Inicializa una nueva instancia de la clase AudioService.
    /// </summary>
    /// <param name="MongoStoreDatabaseSettings">Configuración de la base de datos.</param>
    public AudioService(IOptions<DataContext> MongoStoreDatabaseSettings)
    {
        IMongoClient mongoClient = new MongoClient(MongoStoreDatabaseSettings.Value.ConnectionString);
        IMongoDatabase mongoDatabase = mongoClient.GetDatabase(MongoStoreDatabaseSettings.Value.DatabaseName);
        _songsCollection = mongoDatabase.GetCollection<Audio>(MongoStoreDatabaseSettings.Value.AudioCollectionName);
        _gridFsBucket = new GridFSBucket(mongoDatabase);
    }

    /// <summary>
    /// Obtiene una lista de todos los audios.
    /// </summary>
    /// <returns>Una tarea que representa la operación asincrónica. La tarea contiene la lista de audios.</returns>
    public async Task<List<Audio>> GetAsync() => (await _songsCollection.FindAsync(Song => true)).ToList();

    /// <summary>
    /// Obtiene un audio por su identificador.
    /// </summary>
    /// <param name="id">Identificador del audio.</param>
    /// <returns>Una tarea que representa la operación asincrónica. La tarea contiene el audio encontrado o null si no se encuentra.</returns>
    public async Task<Audio?> GetAsync(string id) => await _songsCollection.Find(x => x.IdCanco_SQL == id).FirstOrDefaultAsync();

    /// <summary>
    /// Obtiene un audio por su identificador de audio.
    /// </summary>
    /// <param name="UID">Identificador de audio.</param>
    /// <returns>Una tarea que representa la operación asincrónica. La tarea contiene el audio encontrado o null si no se encuentra.</returns>
    public async Task<Audio?> GetByAudioIDAsync(string UID) => await _songsCollection.Find(x => x.IdCanco_SQL == UID).FirstOrDefaultAsync();

    /// <summary>
    /// Crea un nuevo audio.
    /// </summary>
    /// <param name="newSong">Nuevo audio a crear.</param>
    /// <returns>Una tarea que representa la operación asincrónica.</returns>
    public async Task CreateAsync(Audio newSong) => await _songsCollection.InsertOneAsync(newSong);

    /// <summary>
    /// Actualiza un audio existente.
    /// </summary>
    /// <param name="id">Identificador del audio a actualizar.</param>
    /// <param name="updatedSong">Audio actualizado.</param>
    /// <returns>Una tarea que representa la operación asincrónica.</returns>
    public async Task UpdateAsync(string id, Audio updatedSong) => await _songsCollection.ReplaceOneAsync(x => x.IdCanco_SQL == id, updatedSong);

    /// <summary>
    /// Sube un archivo de audio al sistema.
    /// </summary>
    /// <param name="filename">Nombre del archivo.</param>
    /// <param name="stream">Flujo de datos del archivo.</param>
    /// <param name="options">Opciones de carga del archivo.</param>
    /// <returns>Una tarea que representa la operación asincrónica. La tarea contiene el identificador del archivo subido.</returns>
    public async Task<ObjectId> UploadAudioAsync(string filename, Stream stream, GridFSUploadOptions options)
    {
        return await _gridFsBucket.UploadFromStreamAsync(filename, stream, options);
    }

    /// <summary>
    /// Obtiene el flujo de datos de un archivo de audio.
    /// </summary>
    /// <param name="audioFileId">Identificador del archivo de audio.</param>
    /// <returns>Una tarea que representa la operación asincrónica. La tarea contiene el flujo de datos del archivo de audio o null si no se encuentra.</returns>
    public async Task<Stream?> GetAudioStreamAsync(ObjectId audioFileId)
    {
        try
        {
            var filter = Builders<GridFSFileInfo>.Filter.Eq("_id", audioFileId);
            var cursor = await _gridFsBucket.FindAsync(filter);

            // Esperar a que la tarea se complete antes de intentar acceder a los resultados
            await cursor.MoveNextAsync();

            var fileInfo = cursor.Current.FirstOrDefault();

            if (fileInfo == null)
            {
                return null;
            }

            var audioStream = await _gridFsBucket.OpenDownloadStreamAsync(audioFileId);
            return audioStream;
        }
        catch (Exception ex)
        {
            // Log de depuración
            Console.WriteLine($"Error in GetAudioStreamAsync: {ex.Message}");
            return null;
        }
    }
}
