using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Assignment_4.Models;
using System.Text.Json;

namespace Assignment_4.Services;

public class ClassesService
{
    private readonly IMongoCollection<Class> _classCollection;

    public ClassesService(IOptions<MongoDbSettings> mongoDbSettings)
    {
        MongoClient client = new MongoClient(mongoDbSettings.Value.ConnectionURI);
        IMongoDatabase database = client.GetDatabase(mongoDbSettings.Value.DatabaseName);
        _classCollection = database.GetCollection<Class>(mongoDbSettings.Value.ClassCollectionName);
    }

    public async Task<List<Class>> GetAsync()
    {
        return await _classCollection.Find(c => true).ToListAsync();
    }

    public async Task<Class> GetAsync(int id)
    {
        return await _classCollection.Find(c => c.Id == id).FirstOrDefaultAsync();
    }

    public void CreateClasses()
    {
        if (_classCollection.Find(c => true).Any())
            return;
        
        foreach (var path in new[] { "metadata.json" })
        {
            using var file = new StreamReader(path);
            var metadata = JsonSerializer.Deserialize<Metadata>(file.ReadToEnd(), new JsonSerializerOptions
            { PropertyNameCaseInsensitive = true });

            if (metadata == null || metadata.Classes == null)
                return;

            _classCollection.InsertMany(metadata.Classes);
        }
    }
}

