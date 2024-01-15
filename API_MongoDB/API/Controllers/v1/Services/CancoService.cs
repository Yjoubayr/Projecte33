using API.Classes.Model;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace API.Services;

public class CancoService
{
    private readonly IMongoCollection<Canco> _CancoCollection;

    public CancoService(IOptions<MongoDBSettings> DatabaseSettings)
    {
        var mongoClient = new MongoClient(
            DatabaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            DatabaseSettings.Value.DatabaseName);

        _CancoCollection = mongoDatabase.GetCollection<Canco>(
            DatabaseSettings.Value.CancoCollectionName);
    }

    public async Task<List<Canco>> GetAsync() =>
        await _CancoCollection.Find(_ => true).ToListAsync();

    public async Task<Canco?> GetAsync(string id) =>
        await _CancoCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(Canco newCanco) =>
        await _CancoCollection.InsertOneAsync(newCanco);

    public async Task UpdateAsync(string id, Canco updatedCanco) =>
        await _CancoCollection.ReplaceOneAsync(x => x.Id == id, updatedCanco);

    public async Task RemoveAsync(string id) =>
        await _CancoCollection.DeleteOneAsync(x => x.Id == id);

    
}