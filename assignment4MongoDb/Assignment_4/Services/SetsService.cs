using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Bson;
using Microsoft.AspNetCore.Mvc;
using Assignment_4.Models;
using System.Text.Json;
//using Assignment_4.Models.Dto;
namespace Assignment_4.Services;

public class SetsService
{
    private readonly IMongoCollection<Set> _setCollection;

    public SetsService(IOptions<MongoDbSettings> mongoDbSettings)
    {
        MongoClient client = new MongoClient(mongoDbSettings.Value.ConnectionURI);
        IMongoDatabase database = client.GetDatabase(mongoDbSettings.Value.DatabaseName);
        _setCollection = database.GetCollection<Set>("SetCollection");
    }

    public async Task<List<Set>> GetAsync()
    {
        return await _setCollection.Find(c => true).ToListAsync();
    }

    public async Task<Set> GetAsync(int id)
    {
        return await _setCollection.Find(c => c.Id == id).FirstOrDefaultAsync();
    }

    public void CreateSets()
    {
        foreach (var path in new[] { "metadata.json" })
        {
            using (var file = new StreamReader(path))
            {
                var metadata = JsonSerializer.Deserialize<Metadata>(file.ReadToEnd(), new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                if (metadata == null || metadata.Types == null)
                {
                    return;
                }

                _setCollection.InsertMany(metadata.Sets);
            }
        }
    }
}