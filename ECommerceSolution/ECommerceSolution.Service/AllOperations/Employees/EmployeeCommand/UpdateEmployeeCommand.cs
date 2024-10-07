using MediatR;

namespace ECommerceSolution.Service.AllOperations.Employees.EmployeeCommand
{
    public class UpdateEmployeeCommand : BaseProperty, IRequest<List<string>>
    {
    }
}
