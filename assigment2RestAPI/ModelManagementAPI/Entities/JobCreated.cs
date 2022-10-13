using System.ComponentModel.DataAnnotations;

namespace ModelManagementAPI.Entities
{
    public class JobCreated
    {
        public long Id { get; set; }
        public string Customer { get; set; } = "";
        public DateTimeOffset StartDate { get; set; }
        public int Days { get; set; }
        [MaxLength(128)]
        public string Location { get; set; } = "";
        [MaxLength(2000)]
        public string Comments { get; set; } = "";

    }
}
