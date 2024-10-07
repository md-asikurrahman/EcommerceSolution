
using MediatR;

namespace ECommerceSolution.Service.AllOperations.Employees.EmployeeQuery
{
    public class GetAllEmployeesQuery : IRequest<IList<string>>;

    public class GetAllEmployeesQueryHandler : IRequestHandler<GetAllEmployeesQuery, IList<string>>
    {
        public async Task<IList<string>> Handle(GetAllEmployeesQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
