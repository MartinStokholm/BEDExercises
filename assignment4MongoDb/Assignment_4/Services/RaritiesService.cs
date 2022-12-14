using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Assignment_4.Models;
using System.Text.Json;

namespace Assignment_4.Services;

public class RaritiesService
{
    private readonly IMongoCollection<Rarity> _rarityCollection;

    public RaritiesService(IOptions<MongoDbSettings> mongoDbSettings)
    {
        MongoClient client = new MongoClient(mongoDbSettings.Value.ConnectionURI);
        IMongoDatabase database = client.GetDatabase(mongoDbSettings.Value.DatabaseName);
        _rarityCollection = database.GetCollection<Rarity>(mongoDbSettings.Value.RarityCollectionName);
    }

    public async Task<List<Rarity>> GetAsync()
    {
        return await _rarityCollection.Find(r => true).ToListAsync();
    }

    public void CreateRarities()
    {
        if (_rarityCollection.Find(r => true).Any())
            return;
        
        foreach (var path in new[] { "metadata.json" })
        {
            using var file = new StreamReader(path);
            var metadata = JsonSerializer.Deserialize<Metadata>(file.ReadToEnd(), new JsonSerializerOptions
            { PropertyNameCaseInsensitive = true });

            if (metadata == null || metadata.Rarities == null)
                return;

            _rarityCollection.InsertMany(metadata.Rarities);
        }
    }
}