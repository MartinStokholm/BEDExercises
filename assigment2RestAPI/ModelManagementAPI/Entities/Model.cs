using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ModelManagementAPI.Entities
{
    public class Model
    {
        public long Id { get; set; }
        [MaxLength(64)]
        public string? FirstName { get; set; }
        [MaxLength(32)]
        public string? LastName { get; set; }
        [MaxLength(254)]
        public string? Email { get; set; }
        [MaxLength(12)]
        public string? PhoneNo { get; set; }
        [MaxLength(64)]
        public string? AddresLine1 { get; set; }
        [MaxLength(64)]
        public string? AddresLine2 { get; set; }
        [MaxLength(9)]
        public string? Zip { get; set; }
        [MaxLength(64)]
        public string? City { get; set; }
        [Column(TypeName = "date")]
        public DateTime BirthDay { get; set; }
        public double Height { get; set; }
        public int ShoeSize { get; set; }
        [MaxLength(32)]
        public string? HairColor { get; set; }
        [MaxLength(1000)]
        public string? Comments { get; set; }
        public List<JobUpdate> Jobs { get; set; } = new List<JobUpdate>();

        public List<ExpenseCreate> Expenses { get; set; } = new List<ExpenseCreate>();
    }

}
