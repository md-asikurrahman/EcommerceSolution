using ECommerceSolution.Domain.Common;

namespace ECommerceSolution.Domain.Entities
{
    public class OrderDetails : BaseEntity
    {
        public int Quantity { get; set; }

        public double Price { get; set; }

        public int OrderId { get; set; }

        public Order? Order { get; set; }
    }
}
