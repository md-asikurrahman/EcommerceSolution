using MediatR;

namespace ECommerceSolution.Service.AllOperations.Brands.BrandCommand
{
    public class UpdateBrandCommand : BaseProperty, IRequest<IList<string>>
    {
    }
}
