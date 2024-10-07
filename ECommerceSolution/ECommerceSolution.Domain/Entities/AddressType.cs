using ECommerceSolution.Domain.Common;

namespace ECommerceSolution.Domain.Entities
{
    public class AddressType : BaseEntity
    {
        public string TypeName { get; set; }
        public IList<Address> Addresses { get; set; } = new List<Address>();
    }
}
