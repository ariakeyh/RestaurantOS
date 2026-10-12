using Application.Interface;
using Application.Queries.AdminPanel.Reports.ReportsDto;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries.AdminPanel.Reports.CategoriesLeaderBoard;

public class GetCategoriesLeaderboardQueryHandler(IAppDbContext context, IAuthService authService) : IRequestHandler<GetCategoriesLeaderboardQuery, List<HorizontalBarChartResult>>
{
    public async Task<List<HorizontalBarChartResult>> Handle(GetCategoriesLeaderboardQuery request, CancellationToken cancellationToken)
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
                x.Quantity
            })
            .ToListAsync(cancellationToken);

        var grouped = items
            .GroupBy(x => new { x.CategoryId, x.CategoryName })
            .Select(g => new HorizontalBarChartResult(
                g.Key.CategoryName,
                g.Sum(x => x.Quantity)
            )).ToList();
        
        var result = request.IsDescending ? grouped.OrderByDescending(x=> x.Value) : grouped
            .OrderBy(x => x.Value);

        return result.Take(request.NumberOfItems).ToList();
    }
}