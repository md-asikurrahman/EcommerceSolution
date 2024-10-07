using ECommerceSolution.Domain.Common;

namespace ECommerceSolution.Domain.Entities
{
    public class Category : BaseEntity
    {
        public int? ParentCategoryId { get; set; }
        public string CategoryName { get; set; }
        public string CategoryDescription { get; set; } = string.Empty;
        public string ImageUrl { get; set; }
        public IList<Brand> Brands { get; set; } = new List<Brand>();
    }
}
