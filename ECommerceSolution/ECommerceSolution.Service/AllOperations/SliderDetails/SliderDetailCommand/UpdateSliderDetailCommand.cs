using MediatR;

namespace ECommerceSolution.Service.AllOperations.SliderDetails.SliderDetailCommand
{
    public class UpdateSliderDetailCommand : BaseProperty, IRequest<IList<string>>
    {
    }
}
