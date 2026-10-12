using Application.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries.AdminPanel.Dashboard.GetDashboardInfoQuery;

public class GetDashboardInfoQueryQuery(IAppDbContext context, IAuthService authService)
    : IRequestHandler<GetDashboardInfoQuery, GetDashboardInfoResult>
{
    public async Task<GetDashboardInfoResult> Handle(GetDashboardInfoQuery request,
        CancellationToken cancellationToken)
    {
        var shopId = authService.GetShopId();
        var todayStart = DateTime.Today;
        var now = DateTime.Now;

        var orderStats = await context.Orders
            .AsNoTracking()
            .Where(o => o.ShopId == shopId && o.CreatedAt >= todayStart)
            .GroupBy(o => 1)
            .Select(g => new
            {
                TotalOrders = g.Count(),
                TotalAmount = g.Sum(o => o.TotalPrice),
                MostExpensiveOrder = g.OrderByDescending(o => o.TotalPrice)
                    .Select(o => new { o.CustomerId, o.Customer.User.Name, o.Customer.User.PhoneNumber, o.TotalPrice, o.TotalPriceWithDiscount })
                    .FirstOrDefault()
            })
            .FirstOrDefaultAsync(cancellationToken);

        var totalNewCustomers = await context.Customers
            .AsNoTracking()
            .Where(c => c.ShopId == shopId && c.CreatedAt >= todayStart)
            .CountAsync(cancellationToken);

        var mostOrderedItem = await context.OrderItems
            .AsNoTracking()
            .Where(oi => oi.Order.ShopId == shopId &&
                         oi.Order.ActualPreparation >= todayStart &&
                         oi.Order.ActualPreparation <= now)
            .GroupBy(oi => new { oi.MenuItemId, oi.MenuItem.Name })
            .Select(g => new
            {
                g.Key.Name,
                TotalQuantity = g.Sum(oi => oi.Quantity)
            })
            .OrderByDescending(x => x.TotalQuantity)
            .FirstOrDefaultAsync(cancellationToken);

        return new GetDashboardInfoResult(
            orderStats?.TotalOrders ?? 0,
            totalNewCustomers,
            orderStats?.TotalAmount ?? 0,
            mostOrderedItem?.Name ?? string.Empty,
            orderStats?.MostExpensiveOrder is null
                ? new MostExpensiveOrderDto(Guid.Empty, string.Empty, string.Empty,0, 0)
                : new MostExpensiveOrderDto(
                    orderStats.MostExpensiveOrder.CustomerId,
                    orderStats.MostExpensiveOrder.Name,
                    orderStats.MostExpensiveOrder.PhoneNumber,
                    orderStats.MostExpensiveOrder.TotalPrice,
                    orderStats.MostExpensiveOrder.TotalPriceWithDiscount)
        );
    }
}