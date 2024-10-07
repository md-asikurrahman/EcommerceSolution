using MediatR;
using ECommerceSolution.Service.AllOperations.SliderDetails.SliderDetailCommand;

namespace ECommerceSolution.Service.AllOperations.SliderDetails.SliderDetailCommandHandler
{
    public class CreateSliderDetailCommandHandler : IRequestHandler<CreateSliderDetailCommand, IList<string>>
    {
        public async Task<IList<string>> Handle(CreateSliderDetailCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
