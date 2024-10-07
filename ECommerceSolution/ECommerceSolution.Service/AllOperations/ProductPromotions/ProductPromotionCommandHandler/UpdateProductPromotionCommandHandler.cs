using MediatR;
using ECommerceSolution.Service.AllOperations.ProductPromotions.ProductPromotionCommand;

namespace ECommerceSolution.Service.AllOperations.ProductPromotions.ProductPromotionCommandHandler
{
    public class UpdateProductPromotionCommandHandler : IRequestHandler<UpdateProductPromotionCommand, IList<string>>
    {
        public async Task<IList<string>> Handle(UpdateProductPromotionCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
