using ECommerceSolution.Service.AllOperations.Designations.DesignationCommand;
using MediatR;


namespace ECommerceSolution.Service.AllOperations.Designations.DesignationHandler
{
    public class CreateDesignationCommandHandler : IRequestHandler<CreateDesignationCommand, List<string>>
    {
        public async Task<List<string>> Handle(CreateDesignationCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
