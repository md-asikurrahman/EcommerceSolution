using MediatR;

namespace ECommerceSolution.Service.AllOperations.SliderDetails.SliderDetailQuery
{
    public record class GetAllSliderDetailsQuery : IRequest<IList<string>>;

    public class GetAllSliderDetailsQueryHandler : IRequestHandler<GetAllSliderDetailsQuery, IList<string>>
    {
        public async Task<IList<string>> Handle(GetAllSliderDetailsQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
