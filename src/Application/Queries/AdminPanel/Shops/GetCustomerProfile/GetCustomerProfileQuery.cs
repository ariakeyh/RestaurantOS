using Domain.Enums;
using MediatR;

namespace Application.Queries.AdminPanel.Shops.GetCustomerProfile;

public sealed record FavoriteMenuItem(int MenuItemId);
public sealed record GetCustomerProfileResult(
    string Name,
    string PhoneNumber,
    CustomerBadge Badge,
    DateTime RegistrationDate,
    int TotalOrders,
    decimal TotalSpend,
    DateTime LastOrderDate,
    TimeSpan AverageOrderInterval,
    List<FavoriteMenuItem> Top3Items
    );

public class GetCustomerProfileQuery : IRequest<GetCustomerProfileResult>
{
    public Guid Id { get; set; }
}