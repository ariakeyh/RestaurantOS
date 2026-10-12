using Application.Interface;
using Application.Queries.AdminPanel.Reports.ReportsDto;
using Common;
using Common.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries.AdminPanel.Reports.MenuItemPriceVolumeLeaderBoard;

public class GetMenuItemPriceVolumeLeaderboardHandler(IAppDbContext context, IAuthService authService) : IRequestHandler<GetMenuItemPriceVolumeLeaderboardQuery, List<HorizontalBarChartResult>>
{
    public async Task<List<HorizontalBarChartResult>> Handle(GetMenuItemPriceVolumeLeaderboardQuery request, CancellationToken cancellationToken)
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
                x.TotalPriceWithDiscount
            })
            .ToListAsync(cancellationToken);

        var grouped = items
            .GroupBy(x => new { x.MenuItemId, x.MenuItemName })
            .Select(g => new HorizontalBarChartResult(
                g.Key.MenuItemName,
                g.Sum(x => x.TotalPriceWithDiscount)
            )).ToList();
        
        var result = request.IsDescending ? grouped.OrderByDescending(x=>x.Value) : grouped
            .OrderBy(x => x.Value);

        return result.Take(request.NumberOfItems).ToList();
    }
}