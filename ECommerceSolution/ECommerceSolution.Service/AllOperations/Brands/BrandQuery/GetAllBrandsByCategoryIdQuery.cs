using MediatR;

namespace ECommerceSolution.Service.AllOperations.Brands.BrandQuery
{
    public record class GetAllBrandsByCategoryIdQuery(int CategoryId):IRequest<IList<string>>;

    public class GetAllBrandsByCategoryIdQueryHandler : IRequestHandler<GetAllBrandsByCategoryIdQuery, IList<string>>
    {
        public async Task<IList<string>> Handle(GetAllBrandsByCategoryIdQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
