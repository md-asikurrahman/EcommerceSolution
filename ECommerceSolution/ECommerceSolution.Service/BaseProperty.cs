namespace ECommerceSolution.Service
{
    public class BaseProperty
    {
        public int Id { get; set; }
        public string? CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedAt { get; set; }
        public string UpdatedBy { get; set; } = string.Empty;
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
        public int SerialNumber { get; set; }
    }
}
