using Application.Interface;
using Application.Queries.AdminPanel.Reports.ReportsDto;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries.AdminPanel.Reports.CategoriesPriceVolumeLeaderboard;

public class GetCategoriesPriceVolumeLeaderboardHandler(IAppDbContext context, IAuthService authService) : IRequestHandler<GetCategoriesPriceVolumeLeaderboardQuery, List<HorizontalBarChartResult>>
{
    public async Task<List<HorizontalBarChartResult>> Handle(GetCategoriesPriceVolumeLeaderboardQuery request, CancellationToken cancellationToken)
    {
        var shopId = authService.GetShopId();
        
        var (from, to) = request.GetNormalizedRange();
        
        var items = await context.OrderItems
            .AsNoTracking()
            .Where(x => x.Order.ShopId == shopId && x.CreatedAt >= from && x.CreatedAt <= to)
            .Select(x => new
            {
                x.MenuItem.CategoryId,
                CategoryName = x.MenuItem.Category.Name,
                x.TotalPriceWithDiscount
            })
            .ToListAsync(cancellationToken);

        var grouped = items
            .GroupBy(x => new { x.CategoryId, x.CategoryName })
            .Select(g => new HorizontalBarChartResult(
                g.Key.CategoryName,
                g.Sum(x => x.TotalPriceWithDiscount)
            )).ToList();
        
        var result = request.IsDescending ? grouped.OrderByDescending(x=>x.Value) : grouped
            .OrderBy(x => x.Value);

        return result.Take(request.NumberOfItems).ToList();
    }
}