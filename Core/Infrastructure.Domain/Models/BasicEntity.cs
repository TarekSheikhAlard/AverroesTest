using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Domain.Models
{
    public class BasicEntity
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(50)]
        public string Code { get; set; }
    }

    public class BasicEntityWithAuditInfo : BasicEntity
    {
        public DateTime CreatedDate { get; set; }
        [MaxLength(50)]
        public string CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        [MaxLength(50)]
        public string? ModifiedBy { get; set; }
    }
}
