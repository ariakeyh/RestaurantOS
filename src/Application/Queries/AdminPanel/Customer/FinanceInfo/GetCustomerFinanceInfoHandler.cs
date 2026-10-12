using Application.Interface;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries.AdminPanel.Customer.FinanceInfo;

public class GetCustomerFinanceInfoHandler(IAppDbContext context, IAuthService authService)
    : IRequestHandler<GetCustomerFinanceInfoQuery, FinanceInfo>
{
    public async Task<FinanceInfo> Handle(GetCustomerFinanceInfoQuery request, CancellationToken cancellationToken)
    {
        var shopId = authService.GetShopId();
        var credits = await context.CustomerCredits
            .Where(x => x.CustomerId == request.CustomerId && x.Customer.ShopId == shopId)
            .Select(x => new { x.Amount, x.SpentAmount, x.Expiration })
            .ToListAsync(cancellationToken);

        var now = DateTime.Now;
        var totalCredits = credits.Sum(c => c.Amount);
        var spentCredits = credits.Sum(c => c.SpentAmount);
        var expiredCredits = credits.Where(c => c.Expiration.HasValue && c.Expiration.Value < now)
            .Sum(c => c.Amount - c.SpentAmount);
        var activeCredits = totalCredits - spentCredits - expiredCredits;

        var orders = await context.Orders.Where(x =>
                x.Status == OrderStatus.Completed && x.CustomerId == request.CustomerId && x.ShopId == shopId)
            .Select(x => new { x.TotalPrice, x.TotalPriceWithDiscount, x.CreatedAt })
            .ToListAsync(cancellationToken);

        var totalOrderPrice = orders.Sum(o => o.TotalPrice);
        var totalOrderPriceWithDiscount = orders.Sum(o => o.TotalPriceWithDiscount);
        var totalGivenDiscount = totalOrderPrice - totalOrderPriceWithDiscount;
        var averageOrderPrice = orders.Count != 0 ? (int)orders.Average(o => o.TotalPrice) : 0;

        var topPriceOrders = orders
            .OrderByDescending(o => o.TotalPrice)
            .Take(5)
            .ToDictionary(o => o.CreatedAt, o => o.TotalPrice);

        var bottomPriceOrders = orders
            .OrderBy(o => o.TotalPrice)
            .Take(5)
            .ToDictionary(o => o.CreatedAt, o => o.TotalPrice);

        var financeInfo = new FinanceInfo(
            TotalCredits: totalCredits,
            SpentCredits: spentCredits,
            ExpiredCredits: expiredCredits,
            TotalOrderPrice: totalOrderPrice,
            TotalOrderPriceWithDiscount: totalOrderPriceWithDiscount,
            TotalGivenDiscount: totalGivenDiscount,
            AverageOrderPrice: averageOrderPrice,
            TopPriceOrders: topPriceOrders,
            BottomPriceOrders: bottomPriceOrders,
            ActiveCredits: activeCredits
        );
        return financeInfo;
    }
}