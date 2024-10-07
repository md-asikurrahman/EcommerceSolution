using MediatR;
using ECommerceSolution.Service.AllOperations.SliderDetails.SliderDetailCommand;

namespace ECommerceSolution.Service.AllOperations.SliderDetails.SliderDetailCommandHandler
{
    public class UpdateSliderDetailCommandHandler : IRequestHandler<UpdateSliderDetailCommand, IList<string>>
    {
        public async Task<IList<string>> Handle(UpdateSliderDetailCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
