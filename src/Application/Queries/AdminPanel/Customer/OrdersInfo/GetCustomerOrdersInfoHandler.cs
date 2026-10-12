using Application.Interface;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries.AdminPanel.Customer.OrdersInfo;

public class GetCustomerOrdersInfoHandler(IAppDbContext context, IAuthService authService)
    : IRequestHandler<GetCustomerOrdersInfoQuery, OrdersInfo>
{
   public async Task<OrdersInfo> Handle(GetCustomerOrdersInfoQuery request, CancellationToken cancellationToken)
{
    var shopId = authService.GetShopId();

    var completedOrdersQuery = context.Orders
        .AsNoTracking()
        .Where(o =>
            o.ShopId == shopId &&
            o.CustomerId == request.CustomerId &&
            o.Status == OrderStatus.Completed);

    var orders = await completedOrdersQuery
        .Select(o => new
        {
            o.Id,
            o.CreatedAt
        })
        .OrderBy(o => o.CreatedAt)
        .ToListAsync(cancellationToken);

    var totalOrdersCount = orders.Count;

    var topMenuItems = await context.OrderItems
        .AsNoTracking()
        .Where(oi => orders.Select(o => o.Id).Contains(oi.OrderId))
        .GroupBy(oi => oi.MenuItem.Name)
        .Select(g => new
        {
            Name = g.Key,
            Quantity = g.Sum(x => x.Quantity)
        })
        .OrderByDescending(x => x.Quantity)
        .Take(5)
        .ToDictionaryAsync(x => x.Name, x => x.Quantity, cancellationToken);

    var bottomMenuItems = await context.OrderItems
        .AsNoTracking()
        .Where(oi => orders.Select(o => o.Id).Contains(oi.OrderId))
        .GroupBy(oi => oi.MenuItem.Name)
        .Select(g => new
        {
            Name = g.Key,
            Quantity = g.Sum(x => x.Quantity)
        })
        .OrderBy(x => x.Quantity)
        .Take(5)
        .ToDictionaryAsync(x => x.Name, x => x.Quantity, cancellationToken);

    var averageDayBetweenOrders = 0;
    if (totalOrdersCount > 1)
    {
        var diffs = orders
            .Zip(orders.Skip(1))
            .Select(pair => (pair.Second.CreatedAt - pair.First.CreatedAt).TotalDays);

        averageDayBetweenOrders = (int)diffs.Average();
    }

    var orderCountPerWeekday = orders
        .GroupBy(o => o.CreatedAt.DayOfWeek)
        .ToDictionary(g => g.Key.ToString(), g => g.Count());

    return new OrdersInfo(
        TotalOrdersCount: totalOrdersCount,
        TopMenuItems: topMenuItems,
        BottomMenuItems: bottomMenuItems,
        AverageDayBetweenOrders: averageDayBetweenOrders,
        OrderCountPerWeekday: orderCountPerWeekday
    );
}

}