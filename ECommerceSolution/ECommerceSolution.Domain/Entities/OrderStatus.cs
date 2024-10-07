using ECommerceSolution.Domain.Common;

namespace ECommerceSolution.Domain.Entities
{
    public class OrderStatus : BaseEntity
    {
        public string StatusName { get; set; }
        public string StatusDescription { get; set; }
    }
}
