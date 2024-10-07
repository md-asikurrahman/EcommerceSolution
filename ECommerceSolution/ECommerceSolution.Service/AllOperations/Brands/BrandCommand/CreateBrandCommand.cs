using MediatR;

namespace ECommerceSolution.Service.AllOperations.Brands.BrandCommand
{
    public class CreateBrandCommand : BaseProperty, IRequest<IList<string>>
    {
    }
}
