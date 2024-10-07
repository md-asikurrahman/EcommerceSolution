using ECommerceSolution.Domain.Common;

namespace ECommerceSolution.Domain.Entities
{
    public class Designation : BaseEntity
    {
        public string DesignationName { get; set; }
        public string Description { get; set; } = string.Empty;
        public IList<Employee> Employees { get; set; } = new List<Employee>();
    }
}
