using ECommerceSolution.Domain.Common;

namespace ECommerceSolution.Domain.Entities
{
    public class VendorType : BaseEntity
    {
        public string TypeName { get; set; }
        public IList<Vendor> Vendors { get; set; } = new List<Vendor>();
    }
}
