using MediatR;
using ECommerceSolution.Service.AllOperations.Brands.BrandCommand;

namespace ECommerceSolution.Service.AllOperations.Brands.BrandCommandHandler
{
    public class UpdateBrandCommandHandler : IRequestHandler<UpdateBrandCommand, IList<string>>
    {
        public async Task<IList<string>> Handle(UpdateBrandCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
