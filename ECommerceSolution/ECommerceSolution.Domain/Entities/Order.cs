using ECommerceSolution.Domain.Common;

namespace ECommerceSolution.Domain.Entities
{
    public class Order : BaseEntity
    {
        public DateTime OrderDate { get; set; }
        public string OrderNo { get; set; } 
        public double TotalOrderAmount { get; set; }
        public int ShippingCharge { get; set; } = 0;
        public string? Remarks { get; set; }
        public string TrackingNumber { get; set; } = string.Empty;
        public int UserId { get; set; }
        public Customer Customer { get; set; } 
        public int? EmployeeId { get; set; }
        public Employee? Employee { get; set; }  
        public IList<OrderStatus> OrderStatus { get; set; }
        public IList<PaymentStatus> PaymentStatus { get; set; }
        public IList<OrderDetails> OrderDetails { get; set; } 
        public IList<Address> OrderAddresses { get; set; } 
    }
}
