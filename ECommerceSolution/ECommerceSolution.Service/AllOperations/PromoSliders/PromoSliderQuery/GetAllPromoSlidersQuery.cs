using MediatR;

namespace ECommerceSolution.Service.AllOperations.PromoSliders.PromoSliderQuery
{
    public record class GetAllPromoSlidersQuery : IRequest<IList<string>>;

    public class GetAllPromoSlidersQueryHandler : IRequestHandler<GetAllPromoSlidersQuery, IList<string>>
    {
        public async Task<IList<string>> Handle(GetAllPromoSlidersQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
