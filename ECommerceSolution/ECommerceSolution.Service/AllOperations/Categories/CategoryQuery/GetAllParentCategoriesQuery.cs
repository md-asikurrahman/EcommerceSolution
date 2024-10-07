using MediatR;

namespace ECommerceSolution.Service.AllOperations.Categories.CategoryQuery
{
    public record class GetAllParentCategoriesQuery : IRequest<IList<string>>;

    public class GetAllParentCategoriesQueryHandler : IRequestHandler<GetAllParentCategoriesQuery, IList<string>>
    {
        public async Task<IList<string>> Handle(GetAllParentCategoriesQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
