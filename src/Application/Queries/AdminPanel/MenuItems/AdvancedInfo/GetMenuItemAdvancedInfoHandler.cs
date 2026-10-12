using Application.Interface;
using Common;
using Common.Exceptions;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries.AdminPanel.MenuItems.AdvancedInfo;

public class GetMenuItemAdvancedInfoHandler(IAppDbContext context, IAuthService authService, IObjectStorage objectStorage) : IRequestHandler<GetMenuItemAdvancedInfoQuery, GetMenuItemAdvancedInfoResult>
{
    public async Task<GetMenuItemAdvancedInfoResult> Handle(GetMenuItemAdvancedInfoQuery request, CancellationToken cancellationToken)
     {
        var shopId = authService.GetShopId();
        
        var menuItem = await context.MenuItems
            .AsNoTracking()
            .Include(m => m.Category)
            .FirstOrDefaultAsync(m =>
                m.Id == request.MenuItemId &&
                m.Category.ShopId == shopId,
                cancellationToken);

        if (menuItem is null)
            throw new NotFoundException(ErrorMessages.MenuItem_NotFound);
        
        var generalInfo = new MenuItemGeneralInfo(
            Title: menuItem.Name,
            PictureUrl: await objectStorage.GetFileUrl(menuItem.ImageName),
            Category: menuItem.Category.Name,
            BasePrice: menuItem.Price,
            PriceWithDiscount: menuItem.PriceWithDiscount()
        );
        
        var orderMetrics = await GetOrderMetrics(request.MenuItemId, cancellationToken);
        
        var ratingMetrics = await GetRatingMetrics(request.MenuItemId, cancellationToken);

        return new GetMenuItemAdvancedInfoResult(
            generalInfo,
            orderMetrics,
            ratingMetrics);
    }

    private async Task<MenuItemRatingMetrics> GetRatingMetrics(
        int menuItemId,
        CancellationToken cancellationToken)
    {
        var result = await context.PollFoodRatings
            .AsNoTracking()
            .Where(r => r.MenuItemId == menuItemId)
            .GroupBy(r => 1)
            .Select(g => new
            {
                TotalCount = g.Count(),
                Average = g.Average(x => (double)x.Rating)
            })
            .FirstOrDefaultAsync(cancellationToken);

        if (result is null || result.TotalCount == 0)
            return new MenuItemRatingMetrics(null, 0);
        
        var totalRatingCount = result.TotalCount;
        var averageRating = result.Average;

        return new MenuItemRatingMetrics(
            averageRating,
            totalRatingCount
        );
    }

    private async Task<MenuItemOrderMetrics> GetOrderMetrics(
        int menuItemId,
        CancellationToken cancellationToken)
    {
        var list = await context.OrderItems
            .Where(oi =>
                oi.MenuItemId == menuItemId &&
                oi.Order.Status == OrderStatus.Completed)
            .GroupBy(oi => oi.OrderId)
            .Select(g => new
            {
                OrderId = g.Key,
                TotalMenuItemPrice = g.Sum(x => x.Quantity * x.PriceWithDiscount),
                Order = g.Select(x => x.Order).FirstOrDefault()
            })
            .ToListAsync(cancellationToken);

        var numberOfOrders = list.Count;

        var totalMenuItemsPrice = list.Sum(x => x.TotalMenuItemPrice);

        var totalOrdersPrice = list
            .Where(x => x.Order != null)
            .Sum(x => x.Order.TotalPriceWithDiscount);

        var ordersPerWeekday = list
            .Where(x => x.Order != null)
            .GroupBy(x => x.Order.CreatedAt.DayOfWeek)
            .ToDictionary(
                g => g.Key.ToString(),
                g => g.Count()
            );

        return new MenuItemOrderMetrics(
            NumberOfOrders: numberOfOrders,
            TotalOrdersPrice: totalOrdersPrice,
            TotalMenuItemsPrice: totalMenuItemsPrice,
            OrdersPerWeekday: ordersPerWeekday
        );
    }
}