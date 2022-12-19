using System.Text.Json.Serialization;

namespace Assignment_4.Models
{
    public class QueryParams
    {
        public int? classid { get; set; }
        public string? artist { get; set; }
        public int? rarityid { get; set; }
        public int? setid { get; set; }
        public int? page { get; set; }

    }
}
