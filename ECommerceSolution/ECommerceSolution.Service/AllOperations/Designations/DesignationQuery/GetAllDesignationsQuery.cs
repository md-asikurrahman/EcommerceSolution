using MediatR;

namespace ECommerceSolution.Service.AllOperations.Designations.DesignationQuery
{
    public record class GetAllDesignationsQuery:IRequest<IList<string>>;

    public class GetAllDesignationsQueryHandler : IRequestHandler<GetAllDesignationsQuery, IList<string>>
    {
        public async Task<IList<string>> Handle(GetAllDesignationsQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
