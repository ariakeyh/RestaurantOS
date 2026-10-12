using Application.Interface;
using Application.Queries.AdminPanel.Reports.ReportsDto;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries.AdminPanel.Reports.OrderRatingLeaderboard;

public class GetPollOrderRatingLeaderboardHandler(IAppDbContext context, IAuthService authService) : IRequestHandler<GetPollOrderRatingLeaderboardQuery, List<HorizontalBarChartResult>>
{
    public async Task<List<HorizontalBarChartResult>> Handle(GetPollOrderRatingLeaderboardQuery request, CancellationToken cancellationToken)
    {
        var currentShopId = authService.GetShopId();

        var data = await context.PollResponses
            .Where(x => x.Order.ShopId == currentShopId)
            .GroupBy(x => x.StarRate)
            .Select(x => new
            {
                StarRate = x.Key,
                Count = x.Count()
            })
            .ToListAsync(cancellationToken);

        return Enumerable.Range(1, 5)
            .Select(star =>
            {
                var item = data.FirstOrDefault(x => x.StarRate == star);
                return new HorizontalBarChartResult(
                    star.ToString(),
                    item?.Count ?? 0
                );
            })
            .ToList();
    }
}