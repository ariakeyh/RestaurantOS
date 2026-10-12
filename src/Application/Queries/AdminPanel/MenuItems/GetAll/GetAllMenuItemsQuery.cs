using MediatR;

namespace Application.Queries.AdminPanel.MenuItems.GetAll;

public record GetAllMenuItemsQuery (int? CategoryId, string? Name) : IRequest<List<GetAllMenuItemsQueryResult>>;

public record GetAllMenuItemsQueryResult(
    int Id,
    string Name,
    string Description,
    string Ingredients,
    string ImageName,
    int Price,
    byte Discount,
    int PriceWithDiscount,
    bool IsUnAvailable,
    int CategoryId,
    string CategoryName
);
