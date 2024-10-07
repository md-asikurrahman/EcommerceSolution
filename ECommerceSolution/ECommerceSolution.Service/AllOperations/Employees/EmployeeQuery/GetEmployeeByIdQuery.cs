using MediatR;

namespace ECommerceSolution.Service.AllOperations.Employees.EmployeeQuery
{
    public record class GetEmployeeByIdQuery(int Id):IRequest<string>;

    public class GetEmployeeByIdQueryHandler : IRequestHandler<GetEmployeeByIdQuery, string>
    {
        public async Task<string> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
