using MediatR;

namespace ECommerceSolution.Service.AllOperations.Promotions.PromotionQuery
{
    public record class GetPromotionByIdQuery : IRequest<string>;

    public class GetPromotionByIdQueryHandler : IRequestHandler<GetPromotionByIdQuery, string>
    {
        public async Task<string> Handle(GetPromotionByIdQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
