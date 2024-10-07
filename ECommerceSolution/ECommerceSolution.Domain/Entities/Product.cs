using ECommerceSolution.Domain.Common;

namespace ECommerceSolution.Domain.Entities
{
    public class Product : BaseEntity
    {
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public string ImageUrl { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public DateTime ManufactureDate { get; set; }
        public DateTime ExpireDate { get; set; }
        public int BrandId { get; set; }
        public Brand Brand { get; set; }
    }
}
