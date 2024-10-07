using MediatR;
using ECommerceSolution.Service.AllOperations.Brands.BrandCommand;

namespace ECommerceSolution.Service.AllOperations.Brands.BrandCommandHandler
{
    public class CreateBrandCommandHandler : IRequestHandler<CreateBrandCommand, IList<string>>
    {
        public async Task<IList<string>> Handle(CreateBrandCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
