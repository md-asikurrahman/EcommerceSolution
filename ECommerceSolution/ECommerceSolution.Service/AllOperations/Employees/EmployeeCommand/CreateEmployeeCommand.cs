using MediatR;

namespace ECommerceSolution.Service.AllOperations.Employees.EmployeeCommand
{
    public class CreateEmployeeCommand : BaseProperty, IRequest<List<string>>
    {
    }
}
