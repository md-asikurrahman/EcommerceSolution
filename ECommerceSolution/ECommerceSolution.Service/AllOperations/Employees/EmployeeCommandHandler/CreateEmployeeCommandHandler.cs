

using ECommerceSolution.Service.AllOperations.Employees.EmployeeCommand;
using MediatR;

namespace ECommerceSolution.Service.AllOperations.Employees.EmployeeCommandHandler
{
    public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, List<string>>
    {
        public async Task<List<string>> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
