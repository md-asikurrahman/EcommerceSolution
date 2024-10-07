using ECommerceSolution.Domain.Common;


namespace ECommerceSolution.Domain.Entities
{
    public class Vendor : BaseEntity
    {
        public string? VendorName { get; set; }
        public string? MobileNumber { get; set; }
        public string? Description { get; set; }
        public int VendorTypeId { get; set; }
        public VendorType VendorType { get; set; }
        public IList<Purchase> Purchases { get; set; } = new List<Purchase>();
    }
}
