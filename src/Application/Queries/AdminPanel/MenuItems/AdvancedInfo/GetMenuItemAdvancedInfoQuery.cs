using MediatR;

namespace Application.Queries.AdminPanel.MenuItems.AdvancedInfo;

public sealed record GetMenuItemAdvancedInfoQuery(int MenuItemId) : IRequest<GetMenuItemAdvancedInfoResult>;


public sealed record GetMenuItemAdvancedInfoResult(
    MenuItemGeneralInfo GeneralInfo,
    MenuItemOrderMetrics OrderMetrics,
    MenuItemRatingMetrics RatingMetrics);
    
public sealed record MenuItemGeneralInfo(
    string Title,
    string PictureUrl,
    string Category,
    int BasePrice,
    int PriceWithDiscount);
    
public sealed record MenuItemOrderMetrics(
    int NumberOfOrders,
    int TotalOrdersPrice,
    int TotalMenuItemsPrice,
    Dictionary<string, int> OrdersPerWeekday);
    
public sealed record MenuItemRatingMetrics(
    double? AverageRating,
    int TotalRatingCount);