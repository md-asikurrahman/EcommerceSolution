using MediatR;

namespace ECommerceSolution.Service.AllOperations.Promotions.PromotionCommand
{
    public class CreatePromotionCommand : BaseProperty, IRequest<IList<string>>
    {
    }
}
