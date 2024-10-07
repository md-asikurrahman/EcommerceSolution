using ECommerceSolution.Domain.Common;

namespace ECommerceSolution.Domain.Entities
{
    public class Address : BaseEntity
    {
        public string HouseNumber { get; set; }
        public string RoadNumber {  get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string Division { get; set; }
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public int AddressTypeId { get; set; }
        public AddressType AddressType { get; set; }
    }
}
