using MediatR;

namespace Application.Queries.AdminPanel.Categories.Get;

public record GetCategoriesQuery(int Id) : IRequest<GetCategoriesQueryResult>;
public record GetCategoriesQueryResult(int Id, string Name, string ImageName, int ProductsCount);