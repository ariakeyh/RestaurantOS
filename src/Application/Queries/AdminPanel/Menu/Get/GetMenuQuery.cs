using MediatR;

namespace Application.Queries.AdminPanel.Menu.Get;

public sealed record GetMenuQuery : IRequest<GetMenuResult>;

public sealed record GetMenuResult(List<CategoryDto> Categories);

public sealed record CategoryDto(
    string Name,
    string ImageName,
    int ProductsCount,
    List<MenuitemDto> Items
);

public sealed record MenuitemDto(
    int Id,
    string Name,
    string Description,
    string Ingredients,
    int Price,
    byte Discount,
    int PriceWithDiscount,
    bool IsUnAvailable
)
{
    public string ImageName { get; set; }
}



    
