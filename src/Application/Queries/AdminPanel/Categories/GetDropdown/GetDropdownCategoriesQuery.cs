using MediatR;

namespace Application.Queries.AdminPanel.Categories.GetDropdown;

public record GetDropdownCategoriesQuery : IRequest<List<GetDropdownCategoriesQueryResult>>;

public record GetDropdownCategoriesQueryResult(int Id, string Name, string ImageName, int ProductsCount);