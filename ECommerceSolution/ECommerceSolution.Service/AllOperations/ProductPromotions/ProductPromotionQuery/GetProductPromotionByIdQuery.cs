using MediatR;

namespace ECommerceSolution.Service.AllOperations.ProductPromotions.ProductPromotionQuery
{
    public record class GetProductPromotionByIdQuery(int Id):IRequest<string>;

    public class GetProductPromotionByIdQueryHandler : IRequestHandler<GetProductPromotionByIdQuery, string>
    {
        public async Task<string> Handle(GetProductPromotionByIdQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
