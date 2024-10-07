using MediatR;

namespace ECommerceSolution.Service.AllOperations.ProductPromotions.ProductPromotionQuery
{
    public record class GetProductPromotionsByPromotionIdQuery(int PromotionId) : IRequest<IList<string>>;

    public class GetProductPromotionsByPromotionIdQueryHandler : IRequestHandler<GetProductPromotionsByPromotionIdQuery, IList<string>>
    {
        public async Task<IList<string>> Handle(GetProductPromotionsByPromotionIdQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
