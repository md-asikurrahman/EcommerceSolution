using MediatR;

namespace ECommerceSolution.Service.AllOperations.PromoSliders.PromoSliderQuery
{
    public record class GetPromoSliderByIdQuery : IRequest<string>;

    public class GetPromoSliderByIdQueryHandler : IRequestHandler<GetPromoSliderByIdQuery, string>
    {
        public async Task<string> Handle(GetPromoSliderByIdQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
