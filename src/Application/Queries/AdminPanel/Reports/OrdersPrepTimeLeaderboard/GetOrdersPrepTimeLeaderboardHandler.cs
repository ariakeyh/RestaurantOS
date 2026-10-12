using Application.Interface;
using Application.Queries.AdminPanel.Reports.ReportsDto;
using Common;
using Common.Exceptions;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries.AdminPanel.Reports.OrdersPrepTimeLeaderboard;

public class GetOrdersPrepTimeLeaderboardHandler(IAppDbContext context, IAuthService authService):  IRequestHandler<GetOrdersPrepTimeLeaderboardQuery, List<HorizontalBarChartResult>>
{
    public async Task<List<HorizontalBarChartResult>> Handle(GetOrdersPrepTimeLeaderboardQuery request, CancellationToken cancellationToken)
    {
        var shopId = authService.GetShopId();

        var(from,to) = request.GetNormalizedRange();

        var items = await context.Orders
            .AsNoTracking()
            .Where(x => x.ShopId == shopId && x.CreatedAt >= from && x.CreatedAt <= to && x.Status == OrderStatus.Completed)
            .Select(x => new
            {
                Minutes = (int)(x.ActualPreparation.GetValueOrDefault() - x.CreatedAt).TotalMinutes
            })
            .ToListAsync(cancellationToken);

        var result = items
            .GroupBy(x => GetPrepLabel(x.Minutes))
            .Select(g => new HorizontalBarChartResult(
                g.Key,
                g.Count()
            ));

        return result.Take(request.NumberOfItems).ToList();
    }
    
    private static string GetPrepLabel(int minutes)
    {
        return minutes switch
        {
            <= 10 => "10",
            <= 20 => "20",
            <= 30 => "30",
            _     => "30+"
        };
    }
}