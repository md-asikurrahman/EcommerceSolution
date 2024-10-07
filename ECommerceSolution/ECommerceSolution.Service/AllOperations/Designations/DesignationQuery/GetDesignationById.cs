using MediatR;

namespace ECommerceSolution.Service.AllOperations.Designations.DesignationQuery
{
    public record class GetDesignationById(int Id):IRequest<string>;

    public class GetDesignationByIdHandler : IRequestHandler<GetDesignationById, string>
    {
        public async Task<string> Handle(GetDesignationById request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
