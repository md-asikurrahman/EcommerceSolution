using MediatR;

namespace ECommerceSolution.Service.AllOperations.Designations.DesignationCommand
{
    public class CreateDesignationCommand : BaseProperty, IRequest<List<string>>
    {

    }
}
