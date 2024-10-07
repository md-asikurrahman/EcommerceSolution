using MediatR;
using ECommerceSolution.Service.AllOperations.PromoSliders.PromoSliderCommand;

namespace ECommerceSolution.Service.AllOperations.PromoSliders.PromoSliderCommandHandler
{
    internal class CreatePromoSliderCommandHandler : IRequestHandler<CreatePromoSliderCommand, IList<string>>
    {
        public async Task<IList<string>> Handle(CreatePromoSliderCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
