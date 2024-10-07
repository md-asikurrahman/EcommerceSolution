using MediatR;

namespace ECommerceSolution.Service.AllOperations.ProductPromotions.ProductPromotionCommand
{
    public class CreateProductPromotionCommand : BaseProperty, IRequest<IList<string>>
    {
    }
}
