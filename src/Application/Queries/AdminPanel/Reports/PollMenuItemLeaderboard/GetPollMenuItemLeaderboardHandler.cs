using Application.Interface;
using Application.Queries.AdminPanel.Reports.ReportsDto;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries.AdminPanel.Reports.PollMenuItemLeaderboard;

public class GetPollMenuItemLeaderboardHandler(IAppDbContext context, IAuthService authService) : IRequestHandler<GetPollMenuItemLeaderboardQuery, List<HorizontalBarChartResult>>
{
    public async Task<List<HorizontalBarChartResult>> Handle(GetPollMenuItemLeaderboardQuery request, CancellationToken cancellationToken)
    {
        var currentShopId = authService.GetShopId();

        var data = await context.PollFoodRatings
            .Where(x => x.MenuItem.Category.ShopId == currentShopId)
            .GroupBy(x => new
            {
                x.MenuItemId,
                x.MenuItem.Name,
                x.MenuItem.ImageName
            })
            .Select(r => new
            {
                MenuItemName = r.Key.Name,
                ImageName = r.Key.ImageName,
                AvgRating = r.Average(x => x.Rating)
            })
            .ToListAsync(cancellationToken);
        
        var ratings = data.Select(item => new HorizontalBarChartResult(item.MenuItemName, item.AvgRating)).ToList();

        return ratings;
    }
}