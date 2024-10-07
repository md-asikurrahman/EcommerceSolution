using MediatR;

namespace ECommerceSolution.Service.AllOperations.Categories.CategoryCommand
{
    public  class UpdateCategoryCommand : BaseProperty, IRequest<IList<string>> 
    {
        public int? ParentCategoryId { get; set; }
        public string CategoryName { get; set; }
        public string CategoryDescription { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        
    }
}
