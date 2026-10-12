using Application.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries.AdminPanel.Orders.GetMenuItems;

public class GetOrderMenuItemsHandler (IAppDbContext context, IAuthService authService) : IRequestHandler<GetOrderMenuItems, List<OrderMenuItemDto>>
{
    public async Task<List<OrderMenuItemDto>> Handle(GetOrderMenuItems request, CancellationToken cancellationToken)
    {
        var shopId = authService.GetShopId();
        var result = await context.OrderItems
            .Where(x => x.Order.ShopId == shopId && x.OrderId == request.Id)
            .Select(x => new OrderMenuItemDto
            {
                MenuItemId = x.MenuItemId,
                MenuItemName = x.MenuItem.Name,
                Price = x.Price,
                Quantity = x.Quantity,
                PriceWithDiscount = x.PriceWithDiscount
            }).ToListAsync(cancellationToken);

        return result;
    }
}