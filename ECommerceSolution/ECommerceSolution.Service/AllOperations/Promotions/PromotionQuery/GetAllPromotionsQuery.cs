using MediatR;

namespace ECommerceSolution.Service.AllOperations.Promotions.PromotionQuery
{
    public record class GetAllPromotionsQuery : IRequest<IList<string>>;

    public class GetAllPromotionsQueryHandler : IRequestHandler<GetAllPromotionsQuery, IList<string>>
    {
        public async Task<IList<string>> Handle(GetAllPromotionsQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
