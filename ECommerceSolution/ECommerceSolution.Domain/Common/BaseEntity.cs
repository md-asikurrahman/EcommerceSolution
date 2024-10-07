
namespace ECommerceSolution.Domain.Common
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; } = null;
        public string? UpdatedBy { get; set; } = string.Empty;
        public bool IsDeleted { get; set; }
        public bool IsActive {  get; set; }
        public int SerialNumber {  get; set; }

    }
}
