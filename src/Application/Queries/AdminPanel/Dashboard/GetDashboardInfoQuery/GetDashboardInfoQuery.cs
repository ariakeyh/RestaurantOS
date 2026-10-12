using MediatR;

namespace Application.Queries.AdminPanel.Dashboard.GetDashboardInfoQuery;

public sealed record GetDashboardInfoQuery : IRequest<GetDashboardInfoResult>;

public sealed record GetDashboardInfoResult(
    int TotalOrders,
    int TotalNewCustomers,
    int TotalAmount,
    string MostOrderedItemName,
    MostExpensiveOrderDto MostExpensiveOrder);

public sealed record MostExpensiveOrderDto(Guid CustomerId, string CustomerName, string CustomerPhoneNumber, int TotalPrice, int TotalPriceWithDiscount);

    
