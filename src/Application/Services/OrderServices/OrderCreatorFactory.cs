using Domain.Enums;

namespace Application.Services.OrderServices;

public class OrderCreatorFactory (POSOrderCreator posOrderCreator, WebsiteOrderCreator websiteOrderCreator, CashierOrderCreator cashierOrderCreator)
{
    public OrderCreatorBase GetCreator(OrderSource source) => source switch
    {
        OrderSource.POS => posOrderCreator,
        OrderSource.Website => websiteOrderCreator,
        OrderSource.Cashier => cashierOrderCreator,
        _ => throw new ArgumentOutOfRangeException(nameof(source))
    };
}