using MediatR;
using ECommerceSolution.Service.AllOperations.Promotions.PromotionCommand;

namespace ECommerceSolution.Service.AllOperations.Promotions.PromotionCommandHandler
{
    public class CreatePromotionCommandHandler : IRequestHandler<CreatePromotionCommand, IList<string>>
    {
        public async Task<IList<string>> Handle(CreatePromotionCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
