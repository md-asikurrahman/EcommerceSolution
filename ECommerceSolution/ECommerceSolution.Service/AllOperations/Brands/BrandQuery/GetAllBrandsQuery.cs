using MediatR;


namespace ECommerceSolution.Service.AllOperations.Brands.BrandQuery
{
    public record class GetAllBrandsQuery : IRequest<IList<string>>;

    public class GetAllBrandsQueryHandler : IRequestHandler<GetAllBrandsQuery, IList<string>>
    {
        public async Task<IList<string>> Handle(GetAllBrandsQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
