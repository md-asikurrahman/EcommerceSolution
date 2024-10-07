using MediatR;

namespace ECommerceSolution.Service.AllOperations.SliderDetails.SliderDetailQuery
{
    public record class GetSliderDetailsByPromoSliderIdQuery : IRequest<IList<string>>;

    public class GetSliderDetailsByPromoSliderIdQueryHandler : IRequestHandler<GetSliderDetailsByPromoSliderIdQuery, IList<string>>
    {
        public async Task<IList<string>> Handle(GetSliderDetailsByPromoSliderIdQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
