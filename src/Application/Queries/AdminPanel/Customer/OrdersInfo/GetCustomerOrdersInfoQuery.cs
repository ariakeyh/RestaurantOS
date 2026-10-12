using MediatR;

namespace Application.Queries.AdminPanel.Customer.OrdersInfo;

public record GetCustomerOrdersInfoQuery(Guid CustomerId) : IRequest<OrdersInfo>;

public sealed record OrdersInfo(
    int TotalOrdersCount,
    Dictionary<string,int> TopMenuItems,
    Dictionary<string,int> BottomMenuItems,
    int AverageDayBetweenOrders,
    Dictionary<string, int> OrderCountPerWeekday);