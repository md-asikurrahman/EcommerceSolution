using MediatR;

namespace ECommerceSolution.Service.AllOperations.Promotions.PromotionCommand
{
    public class UpdatePromotionCommand : BaseProperty, IRequest<IList<string>>
    {
    }
}
