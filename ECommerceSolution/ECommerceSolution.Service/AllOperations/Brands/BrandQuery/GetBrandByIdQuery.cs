using MediatR;

namespace ECommerceSolution.Service.AllOperations.Brands.BrandQuery
{
    public record class GetBrandByIdQuery(int Id) : IRequest<string>;

    public class GetBrandByIdQueryHandler : IRequestHandler<GetBrandByIdQuery, string>
    {
        public async Task<string> Handle(GetBrandByIdQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
