using Application.Interface;
using Application.Services.OrderServices;
using Common;
using Common.Exceptions;
using Domain.Entities.Orders;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Commands.AdminPanel.Order.Update;

public class UpdateOrderHandler(IAppDbContext context, IAuthService authService)
    : IRequestHandler<UpdateOrderCommand, UpdateOrderResult>
{
    public async Task<UpdateOrderResult> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
    {
        var currentShopId = authService.GetShopId();
        
        var order = await GetOrder(request.OrderId, currentShopId, cancellationToken);

        var orderItems = await GetMenuItems(request.Items, currentShopId, cancellationToken);

        order.UpdateOrderItems(orderItems);
        
        await context.SaveChangesAsync(cancellationToken);
        
        return new UpdateOrderResult(order.Id);
    }

    private async Task<List<OrderItem>> GetMenuItems(List<Item> request, int currentShopId,
        CancellationToken cancellationToken)
    {
        var itemIds = request.Select(x => x.ItemId).ToList();
        var menuItems = await context.MenuItems
            .Where(m => m.Category.ShopId == currentShopId && itemIds.Contains(m.Id))
            .ToListAsync(cancellationToken);
        
        var orderItems = menuItems.Select(i =>
        {
            var quantity = request.First(x => x.ItemId == i.Id).Quantity;
            return OrderItem.Create(
                menuItemId: i.Id,
                quantity: quantity,
                price: i.Price,
                priceWithDiscount: i.PriceWithDiscount()
            );
        }).ToList();
        return orderItems;
    }

    private async Task<Domain.Entities.Orders.Order> GetOrder(int orderId, int currentShopId,
        CancellationToken cancellationToken)
    {
        var order = await context.Orders.FirstOrDefaultAsync(x => x.Id == orderId && x.ShopId == currentShopId, cancellationToken);
        
        return order ?? throw new NotFoundException(ErrorMessages.Order_NotFound);
    }
}