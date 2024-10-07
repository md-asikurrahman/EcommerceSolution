using MediatR;

namespace ECommerceSolution.Service.AllOperations.SliderDetails.SliderDetailQuery
{
    public record class GetSliderDetailsByIdQuery : IRequest<string>;

    public class GetSliderDetailsByIdQueryHandler : IRequestHandler<GetSliderDetailsByIdQuery, string>
    {
        public async Task<string> Handle(GetSliderDetailsByIdQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
