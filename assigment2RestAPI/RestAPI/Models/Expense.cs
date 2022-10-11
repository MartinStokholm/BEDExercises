using System.ComponentModel.DataAnnotations.Schema;

namespace ModellingManagementAPI.Models
{
    public class Expense
    {
        public long Id { get; set; }

        [ForeignKey("Id")]
        public long ModelId { get; set; }

        [ForeignKey("Id")]
        public long JobId { get; set; }
        [Column(TypeName = "date")]
        public DateTime Date { get; set; }
        public string? Text { get; set; }
        [Column(TypeName = "decimal(9,2)")]
        public decimal amount { get; set; }
    }
}
