using MediatR;

namespace Application.Queries.AdminPanel.MenuItems.Get;

public record GetMenuItemsQuery (int Id) : IRequest<GetMenuItemsQueryResult>;

public record GetMenuItemsQueryResult(
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
