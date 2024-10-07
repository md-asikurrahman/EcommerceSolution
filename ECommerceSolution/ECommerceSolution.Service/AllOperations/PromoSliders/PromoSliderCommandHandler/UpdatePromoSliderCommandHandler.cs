using MediatR;
using ECommerceSolution.Service.AllOperations.PromoSliders.PromoSliderCommand;

namespace ECommerceSolution.Service.AllOperations.PromoSliders.PromoSliderCommandHandler
{
    public class UpdatePromoSliderCommandHandler : IRequestHandler<UpdatePromoSliderCommand, IList<string>>
    {
        public async Task<IList<string>> Handle(UpdatePromoSliderCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
