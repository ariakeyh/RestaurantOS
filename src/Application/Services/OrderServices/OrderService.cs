using Application.Events;
using Application.Interface;
using Common;
using Common.Exceptions;
using Domain.Entities.Orders;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.OrderServices;

public class OrderService(
    CustomerService customerService,
    OrderCreatorFactory orderCreatorFactory,
    IAppDbContext context,
    IAuthService authService,
    IMediator mediator)
{
    public async Task CreateOrder(
        string customerName,
        string customerPhoneNumber,
        OrderSource source,
        List<Item> items,
        ServeInfoDto serveInfo,
        PaymentInfoDto paymentInfo,
        CancellationToken cancellationToken)
    {
        var shopId = authService.GetShopId();
        var itemIds = items.Select(x => x.ItemId).ToList();
        var menuItems = await context.MenuItems
            .Where(m => itemIds.Contains(m.Id) && m.Category.ShopId == shopId)
            .ToListAsync(cancellationToken);
        var customer = await customerService.GetCustomer(customerName, customerPhoneNumber, shopId, cancellationToken);

        if (await context.Orders.AnyAsync(o => o.Customer.Id == customer.Id && o.Status < OrderStatus.Completed,
                cancellationToken))
            throw new BadRequestException(ErrorMessages.AnOrderAlreadyInProgress);

        await ValidateServeInfo(serveInfo, cancellationToken);

        var orderItems = menuItems.Select(i =>
        {
            var quantity = items.First(x => x.ItemId == i.Id).Quantity;
            return OrderItem.Create(
                menuItemId: i.Id,
                quantity: quantity,
                price: i.Price,
                priceWithDiscount: i.PriceWithDiscount()
            );
        }).ToList();

        var orderCreator = orderCreatorFactory.GetCreator(source);
        var order = await orderCreator.CreateOrder(shopId, customer.Id, serveInfo, paymentInfo, orderItems,
            cancellationToken);

        context.Orders.Add(order);
        await context.SaveChangesAsync(cancellationToken);

        await mediator.Publish(new OrderStatusChangeEvent(order.Id, order.Status), cancellationToken);
    }

    public async Task EditOrder(
        int orderId,
        string customerName,
        string customerPhoneNumber,
        List<Item> items,
        ServeInfoDto serveInfo,
        CancellationToken cancellationToken)
    {
        var shopId = authService.GetShopId();

        var order = await context.Orders
            .Include(o => o.Items)
            .Include(o => o.Customer)
            .ThenInclude(c => c.User)
            .FirstOrDefaultAsync(o => o.Id == orderId && o.ShopId == shopId, cancellationToken);

        if (order is null)
            throw new NotFoundException(ErrorMessages.Order_NotFound);

        if (items is null || items.Count == 0)
            throw new BadRequestException(ErrorMessages.Order_Items_NotEmpty);

        var itemIds = items.Select(i => i.ItemId).ToList();

        var menuItems = await context.MenuItems
            .Where(m => m.Category.ShopId == shopId && itemIds.Contains(m.Id))
            .ToListAsync(cancellationToken);

        if (menuItems.Count != items.Count)
            throw new BadRequestException(ErrorMessages.MenuItem_NotFound);

        var orderItems = menuItems.Select(menuItem =>
        {
            var quantity = items.First(i => i.ItemId == menuItem.Id).Quantity;

            return OrderItem.Create(
                menuItemId: menuItem.Id,
                quantity: quantity,
                price: menuItem.Price,
                priceWithDiscount: menuItem.PriceWithDiscount()
            );
        }).ToList();

        order.UpdateOrderItems(orderItems);

        if (order.Customer.User.PhoneNumber == customerPhoneNumber)
        {
            order.Customer.Name = customerName;
        }
        else
        {
            var newCustomer = await customerService.GetCustomer(
                customerName,
                customerPhoneNumber,
                shopId,
                cancellationToken);

            newCustomer.Name = customerName;

            order.ChangeCustomer(newCustomer);
        }

        await ValidateServeInfo(serveInfo, cancellationToken);

        var domainServeInfo = serveInfo.MapToModel();
        order.ChangeServeInfo(domainServeInfo);

        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task ChangeStatus(int orderId, OrderStatus status, CancellationToken cancellationToken)
    {
        var shopId = authService.GetShopId();

        var order = await context.Orders.FirstOrDefaultAsync(x => x.Id == orderId && x.ShopId == shopId,
            cancellationToken);

        if (order is null)
            throw new NotFoundException(ErrorMessages.Order_NotFound);

        order.ChangeStatus(status);

        await context.SaveChangesAsync(cancellationToken);
        await mediator.Publish(new OrderStatusChangeEvent(order.Id, status), cancellationToken);
    }

    public async Task<OrderMarkettingInfoDto> GetOrderMarketingInfo(int orderId)
    {
        var order = await context.Orders
            .Where(x => x.Id == orderId)
            .Select(x => new OrderMarkettingInfoDto
            {
                Id = x.Id,
                ShopId = x.ShopId,
                ShopName = x.Shop.Name,
                CustomerId = x.CustomerId,
                CustomerPhone = x.Customer.User.PhoneNumber,
                CustomerName = x.Customer.Name,
                TotalPrice = x.TotalPrice,
                TotalPriceWithDiscount = x.TotalPriceWithDiscount
            })
            .FirstOrDefaultAsync();

        if (order is null)
            throw new NotFoundException(ErrorMessages.Order_NotFound);

        return order;
    }

    private async Task ValidateServeInfo(ServeInfoDto serveInfo, CancellationToken cancellationToken)
    {
        switch (serveInfo)
        {
            case TakeAwayServeInfoDto:
                return;
            case OnSiteServeInfoDto onSiteServeInfo:
                var shopId = authService.GetShopId();
                var shopInfo = await context.Shops.FirstAsync(x => x.Id == shopId, cancellationToken);
                if (shopInfo.NumberOfTables < onSiteServeInfo.TableNumber)
                    throw new BadRequestException(ErrorMessages.Order_TableNumber_Invalid);
                return;
            case DeliveryServeInfoDto deliveryServeInfo:
                if (string.IsNullOrWhiteSpace(deliveryServeInfo.Address))
                    throw new BadRequestException(ErrorMessages.Order_AddressForDelivery_Invalid);
                break;
            default:
                throw new BadRequestException(ErrorMessages.Order_ServeInfo_NotValid);
        }
    }
}

public record Item(int ItemId, int Quantity);