using API.Classes.Model;
using API.Controllers;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace API.Services;

public class HistorialService
{
    private readonly IMongoCollection<Historial> _HistorialCollection;

    public HistorialService(
        IOptions<MongoDBSettings> DatabaseSettings)
    {
        var mongoClient = new MongoClient(
            DatabaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            DatabaseSettings.Value.DatabaseName);

        _HistorialCollection = mongoDatabase.GetCollection<Historial>(
            DatabaseSettings.Value.HistorialCollectionName);
    }

    public async Task<List<Historial>> GetAsync() =>
        await _HistorialCollection.Find(_ => true).ToListAsync();

    public async Task<Historial?> GetAsync(string id) =>
        await _HistorialCollection.Find(x => x.IDDispositiu == id).FirstOrDefaultAsync();
    public async Task CreateAsync(Historial newHistorial) =>
        await _HistorialCollection.InsertOneAsync(newHistorial);

    public async Task UpdateAsync(string id, Historial updatedHistorial) =>
        await _HistorialCollection.ReplaceOneAsync(x => x.IDDispositiu == id, updatedHistorial);

    public async Task RemoveAsync(string id) =>
        await _HistorialCollection.DeleteOneAsync(x => x.IDDispositiu == id);

    
}