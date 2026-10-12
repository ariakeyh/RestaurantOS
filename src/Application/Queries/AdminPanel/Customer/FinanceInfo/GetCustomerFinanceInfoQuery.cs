using MediatR;

namespace Application.Queries.AdminPanel.Customer.FinanceInfo;

public record GetCustomerFinanceInfoQuery(Guid CustomerId) : IRequest<FinanceInfo>;
public sealed record FinanceInfo(
    int TotalCredits,
    int SpentCredits,
    int ExpiredCredits,
    int TotalOrderPrice,
    int TotalOrderPriceWithDiscount,
    int TotalGivenDiscount,
    int AverageOrderPrice,
    Dictionary<DateTime, int> TopPriceOrders,
    Dictionary<DateTime, int> BottomPriceOrders,
    int ActiveCredits);