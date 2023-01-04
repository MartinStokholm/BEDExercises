using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Assignment_4.Models;
using Assignment_4.Models.Dto;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Assignment_4.Services;

public class CardsService
{
    private readonly IMongoCollection<Card> _cardCollection;
    private readonly IMongoCollection<Class> _classCollection;
    private readonly IMongoCollection<Set> _setCollection;
    private readonly IMongoCollection<Types> _cardTypeCollection;
    private readonly IMongoCollection<Rarity> _rarityCollection;

    public CardsService(IOptions<MongoDbSettings> mongoDbSettings)
    {
        MongoClient client = new MongoClient(mongoDbSettings.Value.ConnectionURI);
        IMongoDatabase database = client.GetDatabase(mongoDbSettings.Value.DatabaseName);
        _cardCollection = database.GetCollection<Card>("CardCollection");
        _classCollection = database.GetCollection<Class>("ClassCollection");
        _setCollection = database.GetCollection<Set>("SetCollection");
        _cardTypeCollection = database.GetCollection<Types>("CardTypeCollection");
        _rarityCollection = database.GetCollection<Rarity>("RarityCollection");
        CreateCards();
    }    
    public async Task<List<CardWithMetaDataDto>> GetCardsByQueryAsync(QueryParams queryParams)
    {
        var result = new List<Card>();
        var filter = Builders<Card>.Filter.Empty;
        
        if (queryParams.artist != null)
        {
            filter &= Builders<Card>.Filter.Eq(c => c.Artist, queryParams.artist);
        }
        if (queryParams.classid != null)
        {
            filter &= Builders<Card>.Filter.Eq(c => c.ClassId, queryParams.classid);
        }
        if (queryParams.rarityid != null)
        {
            filter &= Builders<Card>.Filter.Eq(c => c.RarityId, queryParams.rarityid);
        }
        if (queryParams.setid != null)
        {
            filter &= Builders<Card>.Filter.Eq(c => c.SetId, queryParams.setid);
        }
        if (queryParams.page != null)
        {
            result = await _cardCollection.Find(filter).Skip((queryParams.page - 1) * 100).Limit(100).ToListAsync();
        }
        else
        {
            result = await _cardCollection.Find(filter).ToListAsync();
        }

        return JoinCardsWithMetaData(result);
    }
    public void CreateCards()
    {
        foreach (var path in new[] { "cards.json" })
        {
            using (var file = new StreamReader(path))
            {
                var cards = JsonSerializer.Deserialize<List<Card>>(file.ReadToEnd(), new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                _cardCollection.InsertMany(cards);
            }
        }
    }
   
    // joins the card with the metadata by id and returns a list of cards with strings instead of ids
    public List<CardWithMetaDataDto> JoinCardsWithMetaData(List<Card> cards)
    {
        var query = from card in cards
                    join classType in _classCollection.AsQueryable() on card.ClassId equals classType.Id
                    join cardType in _cardTypeCollection.AsQueryable() on card.TypeId equals cardType.Id
                    join rarity in _rarityCollection.AsQueryable() on card.RarityId equals rarity.Id
                    join set in _setCollection.AsQueryable() on card.SetId equals set.Id
                   
                    select new CardWithMetaDataDto
                    {
                        Id = card.Id,
                        Name = card.Name,
                        Type = cardType.Name,
                        Class = classType.Name,
                        Set = set.Name,
                        SpellSchool = "not found",
                        Rarity = rarity.Name,
                        Health = card.Health,
                        Attack = card.Attack,
                        ManaCost = card.ManaCost,
                        Artist = card.Artist,
                        Text = card.Text,
                        FlavorText = card.FlavorText,
                    };

        return query.ToList();
    }
}