using ECommerceSolution.Domain.Common;

namespace ECommerceSolution.Domain.Entities
{
    public class Brand : BaseEntity
    {
        public string BrandName { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public IList<Product> Products { get; set; } = new List<Product>();

    }
}
