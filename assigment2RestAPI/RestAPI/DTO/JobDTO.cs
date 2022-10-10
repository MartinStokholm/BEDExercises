using System.ComponentModel.DataAnnotations;

namespace RestAPI.DTO
{
    public class JobDTO
    {
        [MaxLength(64)]
        public string? Customer { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public int Days { get; set; }
        [MaxLength(128)]
        public string? Location { get; set; }
        [MaxLength(2000)]
        public string? Comments { get; set; }
    }

    public class JobUpdateDTO
    {
        public DateTimeOffset StartDate { get; set; }
        public int Days { get; set; }
        [MaxLength(128)]
        public string? Location { get; set; }
        [MaxLength(2000)]
        public string? Comments { get; set; }
    }
}
