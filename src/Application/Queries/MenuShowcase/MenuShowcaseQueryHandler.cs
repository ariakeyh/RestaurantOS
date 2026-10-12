using Application.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries.MenuShowcase;

public class MenuShowcaseQueryHandler(IAppDbContext context, IObjectStorage objectStorage, IAuthService authService)
    : IRequestHandler<MenuShowcaseQuery, List<MenuShowcaseQueryResult>>
{
    private const string _menuItemBucket = "menuitem";
    public async Task<List<MenuShowcaseQueryResult>> Handle(MenuShowcaseQuery request,
        CancellationToken cancellationToken)
    {
        var shopId = authService.GetShopId();
        
        var rawCategories = await context.Categories
            .Where(x=>x.ShopId == shopId)
            .AsNoTrackingWithIdentityResolution()
            .Select(c => new
            {
                c.Id,
                c.Name,
                c.ImageName,
                Items = c.Items.Select(i => new
                {
                    i.Id,
                    i.Name,
                    i.Ingredients,
                    i.Description,
                    i.ImageName,
                    i.Price,
                    PriceWithDiscount =  i.Price - i.Price * i.Discount / 100,
                    i.Discount
                }).ToList()
            })
            .ToListAsync(cancellationToken);

        var finalCategories = await Task.WhenAll(rawCategories.Select(async c =>
            new MenuShowcaseQueryResult(
                c.Id,
                c.Name,
                c.ImageName,
                (await Task.WhenAll(c.Items.Select(async i =>
                    new MenuItemDto(
                        i.Id,
                        i.Name,
                        i.Ingredients,
                        i.Description,
                        await objectStorage.GetFileUrl(i.ImageName),
                        i.Price,
                        i.PriceWithDiscount,
                        i.Discount
                    )
                ))).ToList()
            )
        ));

        return finalCategories.ToList();
    }
}