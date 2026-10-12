using MediatR;

namespace Application.Queries.AdminPanel.Categories.GetAll;

public record GetAllCategoriesQuery : IRequest<List<GetAllCategoriesQueryResult>>;

public record GetAllCategoriesQueryResult(int Id, string Name, string ImageName, int ProductsCount);