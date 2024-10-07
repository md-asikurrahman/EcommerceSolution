using MediatR;
using ECommerceSolution.Service.AllOperations.Promotions.PromotionCommand;

namespace ECommerceSolution.Service.AllOperations.Promotions.PromotionCommandHandler
{
    public class UpdatePromotionCommandHandler : IRequestHandler<UpdatePromotionCommand, IList<string>>
    {
        public async Task<IList<string>> Handle(UpdatePromotionCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
