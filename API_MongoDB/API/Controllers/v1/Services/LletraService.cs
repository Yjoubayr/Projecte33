using API.Classes.Model;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace API.Services;

public class LletraService
{
    private readonly IMongoCollection<Lletra> _LletraCollection;

    public LletraService(
        IOptions<MongoDBSettings> DatabaseSettings)
    {
        var mongoClient = new MongoClient(
            DatabaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            DatabaseSettings.Value.DatabaseName);

        _LletraCollection = mongoDatabase.GetCollection<Lletra>(
            DatabaseSettings.Value.LletraCollectionName);
    }

    public async Task<List<Lletra>> GetAsync() =>
        await _LletraCollection.Find(_ => true).ToListAsync();

    public async Task<Lletra?> GetAsync(string id) =>
        await _LletraCollection.Find(x => x.IDLletra == id).FirstOrDefaultAsync();

    public async Task CreateAsync(Lletra newLletra) =>
        await _LletraCollection.InsertOneAsync(newLletra);

    public async Task UpdateAsync(string id, Lletra updatedLletra) =>
        await _LletraCollection.ReplaceOneAsync(x => x.IDLletra == id, updatedLletra);

    public async Task RemoveAsync(string id) =>
        await _LletraCollection.DeleteOneAsync(x => x.IDLletra == id);

    
}