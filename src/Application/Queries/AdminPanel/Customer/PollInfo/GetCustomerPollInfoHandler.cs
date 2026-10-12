using Application.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries.AdminPanel.Customer.PollInfo;

public class GetCustomerPollInfoHandler(IAppDbContext context, IAuthService authService)
    : IRequestHandler<GetCustomerPollInfoQuery, PollInfo>
{
    public async Task<PollInfo> Handle(GetCustomerPollInfoQuery request, CancellationToken cancellationToken)
    {
        var shopId = authService.GetShopId();

        var pollResponses = await context.PollResponses
            .AsNoTracking()
            .Where(pr =>
                pr.Order.CustomerId == request.CustomerId &&
                pr.Order.ShopId == shopId)
            .Select(pr => new
            {
                pr.StarRate,
                pr.Note,
                pr.Order.CreatedAt,
                FoodRatings = pr.FoodRatings.Select(fr => new
                {
                    MenuItemName = fr.MenuItem.Name,
                    MenuItemId = fr.MenuItem.Id,
                    fr.Rating,
                    fr.CreatedAt
                }).ToList()
            })
            .ToListAsync(cancellationToken);

        var pollCount = pollResponses.Count;

        var averageRating = pollCount > 0
            ? pollResponses.Average(pr => pr.StarRate)
            : 0;
        
        var topOrderRatings = pollResponses
            .OrderByDescending(pr => pr.StarRate)
            .Take(5)
            .ToDictionary(pr => pr.CreatedAt, pr => pr.StarRate);
        
        var bottomOrderRatings = pollResponses
            .OrderBy(pr => pr.StarRate)
            .Take(5)
            .ToDictionary(pr => pr.CreatedAt, pr => pr.StarRate);
        
        var foodRatings = pollResponses
            .SelectMany(pr => pr.FoodRatings)
            .GroupBy(fr => fr.MenuItemName)
            .Select(g => new
            {
                Name = g.Key,
                Average =g.Average(fr => fr.Rating),
                Date = g.Select(x=>x.CreatedAt)
            })
            .ToList();

        var topMenuItemRatings = foodRatings
            .OrderByDescending(fr => fr.Average)
            .ThenByDescending(fr => fr.Date)
            .Take(5)
            .ToDictionary(fr => fr.Name, fr => fr.Average);

        var bottomMenuItemRatings = foodRatings
            .OrderBy(fr => fr.Average)
            .ThenByDescending(fr => fr.Date)
            .Take(5)
            .ToDictionary(fr => fr.Name, fr => fr.Average);

        var allPollNotes = pollResponses
            .Where(pr => !string.IsNullOrWhiteSpace(pr.Note))
            .Select(pr => pr.Note)
            .ToList();

        return new PollInfo(
            PollCount: pollCount,
            AverageRating: averageRating,
            TopOrderRatings: topOrderRatings,
            BottomOrderRatings: bottomOrderRatings,
            TopMenuItemAverageRatings: topMenuItemRatings,
            BottomMenuItemAverageRatings: bottomMenuItemRatings,
            AllPollNotes: allPollNotes
        );
    }
}