using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Assignment_4.Models;
using System.Text.Json;
namespace Assignment_4.Services;

public class TypesService
{
    private readonly IMongoCollection<Types> _typesCollection;

    public TypesService(IOptions<MongoDbSettings> mongoDbSettings)
    {
        MongoClient client = new MongoClient(mongoDbSettings.Value.ConnectionURI);
        IMongoDatabase database = client.GetDatabase(mongoDbSettings.Value.DatabaseName);
        _typesCollection = database.GetCollection<Types>(mongoDbSettings.Value.CardTypeCollectionName);
    }

    public async Task<List<Types>> GetAsync()
    {
        return await _typesCollection.Find(c => true).ToListAsync();
    }

    public void CreateTypes()
    {
        if (_typesCollection.Find(c => true).Any())
            return;

        foreach (var path in new[] { "metadata.json" })
        {
            using var file = new StreamReader(path);
            var metadata = JsonSerializer.Deserialize<Metadata>(file.ReadToEnd(), new JsonSerializerOptions
            { PropertyNameCaseInsensitive = true });

            if (metadata == null || metadata.Types == null)
                return;

            _typesCollection.InsertMany(metadata.Types);
        }
    }
}