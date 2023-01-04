using System;
using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Assignment_4.Models
{
    public class Card
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int ClassId { get; set; }
        
        [JsonPropertyName("cardTypeId")]
        public int TypeId { get; set; }
        
        [JsonPropertyName("cardSetId")]
        public int SetId { get; set; }
        public int? SpellSchoolId { get; set; }
        public int RarityId { get; set; }
        public int? Health { get; set; }
        public int? Attack { get; set; }
        public int ManaCost { get; set; }
        
        [JsonPropertyName("artistName")]
        public string? Artist { get; set; }
        public string? Text { get; set; }
        public string? FlavorText { get; set; }
    }
}
