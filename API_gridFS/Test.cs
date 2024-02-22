/*using System;
using System.IO;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;

public class MongoDbManager
{
    private readonly IMongoDatabase _database;
    private readonly GridFSBucket _gridFs;
    
    public MongoDbManager(string connectionString, string databaseName)
    {
        var client = new MongoClient(connectionString);
        _database = client.GetDatabase(databaseName);
        _gridFs = new GridFSBucket(_database);
    }

    public async Task<ObjectId> UploadSongAsync(string filePath, string fileName)
    {
        using (var fileStream = new FileStream(filePath, FileMode.Open))
        {
            var options = new GridFSUploadOptions
            {
                Metadata = new BsonDocument("filename", fileName)
            };

            return await _gridFs.UploadFromStreamAsync(fileName, fileStream, options);
        }
    }
    public async Task DownloadSongAsync(ObjectId fileId, string destinationPath)
    {
        var fileStream = await _gridFs.OpenDownloadStreamAsync(fileId);
        var destinationFileStream = new FileStream(destinationPath, FileMode.Create);

        await fileStream.CopyToAsync(destinationFileStream);

        destinationFileStream.Close();
        fileStream.Close();
    }   

}

public partial class Program
{
    static string connectionString = "mongodb://root:a@localhost:27017/admin";
    static string databaseName = "Audios";
    static async Task Main(string[] args)
    {
        // Ruta del archivo de la canción que deseas subir
        string filePath = "C:\\Users\\fdily\\Downloads\\x.mp3";

        // Nombre del archivo en GridFS (puede ser diferente al nombre del archivo en el sistema de archivos local)
        string fileName = "xxxx.mp3";

        var mongoDbManager = new MongoDbManager(connectionString, databaseName);

        // Sube la canción a MongoDB usando GridFS
        ObjectId fileId = await mongoDbManager.UploadSongAsync(filePath, fileName);

        Console.WriteLine($"Canción subida con éxito. Id del archivo en GridFS: {fileId}");
    }
}
*/