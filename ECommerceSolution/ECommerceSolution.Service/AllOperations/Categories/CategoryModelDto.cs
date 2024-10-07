namespace ECommerceSolution.Service.AllOperations.Categories
{
    public class CategoryModelDto: BaseProperty
    {
        public int? ParentCategoryId { get; set; }
        public string CategoryName { get; set; }
        public string CategoryDescription { get; set; } = string.Empty;
        public string ImageUrl { get; set; }
    }
}
