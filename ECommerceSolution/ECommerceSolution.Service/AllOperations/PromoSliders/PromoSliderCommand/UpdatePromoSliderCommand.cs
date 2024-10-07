using MediatR;

namespace ECommerceSolution.Service.AllOperations.PromoSliders.PromoSliderCommand
{
    public class UpdatePromoSliderCommand : BaseProperty, IRequest<IList<string>>
    {
    }
}
