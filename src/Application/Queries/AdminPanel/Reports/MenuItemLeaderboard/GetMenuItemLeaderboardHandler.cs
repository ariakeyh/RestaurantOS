using Application.Interface;
using Application.Queries.AdminPanel.Reports.ReportsDto;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries.AdminPanel.Reports.MenuItemLeaderboard;

public class GetMenuItemLeaderboardHandler(IAuthService authService,IAppDbContext context) : IRequestHandler<GetMenuItemLeaderboardQuery, List<HorizontalBarChartResult>>
{
    public async Task<List<HorizontalBarChartResult>> Handle(GetMenuItemLeaderboardQuery request, CancellationToken cancellationToken)
    {
        var shopId = authService.GetShopId();
        
        var(from,to) = request.GetNormalizedRange();
        
        var items = await context.OrderItems
            .AsNoTracking()
            .Where(x => x.Order.ShopId == shopId && x.CreatedAt >= from && x.CreatedAt <= to)
            .Select(x => new
            {
                x.MenuItemId,
                MenuItemName = x.MenuItem.Name,
                x.Quantity
            })
            .ToListAsync(cancellationToken);

        var grouped = items
            .GroupBy(x => new { x.MenuItemId, x.MenuItemName })
            .Select(g => new HorizontalBarChartResult(
                g.Key.MenuItemName,
                g.Sum(x => x.Quantity)
            ));

        var result = request.IsDescending
            ? grouped.OrderByDescending(x => x.Value)
            : grouped.OrderBy(x => x.Value);

        return result
            .Take(request.NumberOfItems)
            .ToList();
    }
}