using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Assignment_4.Models;
using Assignment_4.Models.Dto;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.VisualBasic;

namespace Assignment_4.Services;

public class CardsService
{
    private readonly IMongoCollection<Card> _cardCollection;
    private readonly IMongoCollection<Class> _classCollection;
    private readonly IMongoCollection<Set> _setCollection;
    private readonly IMongoCollection<Types> _typesCollection;
    private readonly IMongoCollection<Rarity> _rarityCollection;

    public CardsService(IOptions<MongoDbSettings> mongoDbSettings)
    {
        MongoClient client = new MongoClient(mongoDbSettings.Value.ConnectionURI);
        IMongoDatabase database = client.GetDatabase(mongoDbSettings.Value.DatabaseName);
        
        _cardCollection = database.GetCollection<Card>(mongoDbSettings.Value.CardCollectionName);
        _typesCollection = database.GetCollection<Types>(mongoDbSettings.Value.CardTypeCollectionName);
        _classCollection = database.GetCollection<Class>(mongoDbSettings.Value.ClassCollectionName);
        _rarityCollection = database.GetCollection<Rarity>(mongoDbSettings.Value.RarityCollectionName);
        _setCollection = database.GetCollection<Set>(mongoDbSettings.Value.SetCollectionName);
                
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
        if (_cardCollection.CountDocuments(c => true) != 0)
        {
            return;
        }
        
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
        //var query = from card in cards
        //            join classType in _classCollection.AsQueryable() on card.ClassId equals classType.Id
        //            join cardType in _cardTypeCollection.AsQueryable() on card.TypeId equals cardType.Id
        //            join rarity in _rarityCollection.AsQueryable() on card.RarityId equals rarity.Id
        //            join set in _setCollection.AsQueryable() on card.SetId equals set.Id
                   
        //            select new CardWithMetaDataDto
        //            {
        //                Id = card.Id,
        //                Name = card.Name,
        //                Type = cardType.Name,
        //                Class = classType.Name,
        //                Set = set.Name,
                        
        //                Rarity = rarity.Name,
        //                Health = card.Health,
        //                Attack = card.Attack,
        //                ManaCost = card.ManaCost,
        //                Artist = card.Artist,
        //                Text = card.Text,
        //                FlavorText = card.FlavorText,
        //            };

        //return query.ToList();
        
        var result = new List<CardWithMetaDataDto>();

        foreach (var card in cards)
        {
            result.Add(new CardWithMetaDataDto
            {
                Id = card.Id,
                Name = card.Name,
                Class = (from c in _classCollection.AsQueryable()
                         where c.Id == card.ClassId
                         select c.Name).FirstOrDefault(),
                
                Type = (from t in _typesCollection.AsQueryable()
                        where t.Id == card.TypeId
                        select t.Name).FirstOrDefault(),
                
                Set = (from s in _setCollection.AsQueryable()
                       where s.Id == card.SetId
                       select s.Name).FirstOrDefault(),
                
                SpellSchoolId = card.SpellSchoolId,
                
                Rarity = (from r in _rarityCollection.AsQueryable()
                          where r.Id == card.RarityId
                          select r.Name).FirstOrDefault(),
                
                Health = card.Health,
                Attack = card.Attack,
                ManaCost = card.ManaCost,
                Artist = card.Artist,
                Text = card.Text,
                FlavorText = card.FlavorText
            });
        }
        return result;
    }
}