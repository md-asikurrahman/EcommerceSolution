using MediatR;
using ECommerceSolution.Service.AllOperations.Employees.EmployeeCommand;


namespace ECommerceSolution.Service.AllOperations.Employees.EmployeeCommandHandler
{
    public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand, List<string>>
    {
        public async Task<List<string>> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
