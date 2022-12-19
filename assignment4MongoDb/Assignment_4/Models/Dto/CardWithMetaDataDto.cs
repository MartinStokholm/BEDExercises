using System.Text.Json.Serialization;

namespace Assignment_4.Models.Dto
{
    public class CardWithMetaDataDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Class { get; set; }
        public string? Type { get; set; }
        public string? Set { get; set; }
        public string? SpellSchool { get; set; }
        public string? Rarity { get; set; }
        public int? Health { get; set; }
        public int? Attack { get; set; }
        public int ManaCost { get; set; }
        public string? Artist { get; set; }
        public string? Text { get; set; }
        public string? FlavorText { get; set; }
    }
}
