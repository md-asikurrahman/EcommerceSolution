using MediatR;
using ECommerceSolution.Service.AllOperations.ProductPromotions.ProductPromotionCommand;

namespace ECommerceSolution.Service.AllOperations.ProductPromotions.ProductPromotionCommandHandler
{
    public class CreateProductPromotionCommandHandler : IRequestHandler<CreateProductPromotionCommand, IList<string>>
    {
        public async Task<IList<string>> Handle(CreateProductPromotionCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
