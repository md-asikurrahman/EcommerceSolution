using MediatR;

namespace ECommerceSolution.Service.AllOperations.SliderDetails.SliderDetailCommand
{
    public class CreateSliderDetailCommand : BaseProperty, IRequest<IList<string>>
    {
    }
}
