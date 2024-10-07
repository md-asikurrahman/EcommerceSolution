using MediatR;

namespace ECommerceSolution.Service.AllOperations.ProductPromotions.ProductPromotionCommand
{
    public class UpdateProductPromotionCommand : BaseProperty, IRequest<IList<string>>
    {
    }
}
