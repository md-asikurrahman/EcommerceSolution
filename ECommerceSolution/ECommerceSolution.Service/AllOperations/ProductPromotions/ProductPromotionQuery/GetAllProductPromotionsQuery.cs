using MediatR;

namespace ECommerceSolution.Service.AllOperations.ProductPromotions.ProductPromotionQuery
{
    public record class GetAllProductPromotionsQuery : IRequest<IList<string>>;

    public class GetAllProductPromotionsQueryHandler : IRequestHandler<GetAllProductPromotionsQuery, IList<string>>
    {
        public async Task<IList<string>> Handle(GetAllProductPromotionsQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
