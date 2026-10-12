using MediatR;

namespace Application.Queries.MenuShowcase;

public class MenuShowcaseQuery : IRequest<List<MenuShowcaseQueryResult>>;

public record MenuShowcaseQueryResult(int Id, string Name, string ImageName, List<MenuItemDto> Items);
public record MenuItemDto (int Id, string Name, string Ingredients, string Description, string ImageName, int Price, int PriceWithDiscount, byte Discount);