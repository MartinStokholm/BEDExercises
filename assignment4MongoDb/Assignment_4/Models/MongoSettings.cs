namespace Assignment_4.Models;

public class MongoDbSettings
{
    public string ConnectionURI { get; set; } = null!;
    public string DatabaseName { get; set; } = null!;
    public string CollectionName { get; set; } = null!;
    public string CardCollectionName { get; set; } = null!;
    public string CardTypeCollectionName { get; set; } = null!;
    public string ClassCollectionName { get; set; } = null!;
    public string RarityCollectionName { get; set; } = null!;
    public string SetCollectionName { get; set; } = null!;
}
