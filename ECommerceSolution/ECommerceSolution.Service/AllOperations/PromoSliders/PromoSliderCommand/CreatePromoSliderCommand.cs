using MediatR;

namespace ECommerceSolution.Service.AllOperations.PromoSliders.PromoSliderCommand
{
    public class CreatePromoSliderCommand : BaseProperty, IRequest<IList<string>>
    {
    }
}
