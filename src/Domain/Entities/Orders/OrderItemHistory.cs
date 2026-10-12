using Domain.Entities.Agg;
using Domain.Entities.Menu;
using Domain.Enums;

namespace Domain.Entities.Orders;

public class OrderItemChangeHistory : AggregateRootWithKey<int>
{
    public int OrderId { get; private set; }
    public int MenuItemId { get; private set; }
    public int Quantity { get; private set; }
    public int Price { get; private set; }
    public int PriceWithDiscount { get; private set; }
    public int OrderVersion { get; private set; }
    public OrderItemHistoryType Type { get; private set; }

    public int TotalPrice => Quantity * Price;
    public int TotalPriceWithDiscount => Quantity * PriceWithDiscount;

    public Order Order { get; private set; }
    public MenuItem MenuItem { get; private set; }

    private OrderItemChangeHistory() { }

    public static OrderItemChangeHistory CreateAdded(int orderId, int menuItemId, int quantity, int price, int priceWithDiscount, int orderVersion)
    {
        return new OrderItemChangeHistory
        {
            OrderId = orderId,
            MenuItemId = menuItemId,
            Quantity = quantity,
            Price = price,
            PriceWithDiscount = priceWithDiscount,
            OrderVersion = orderVersion,
            Type = OrderItemHistoryType.Added
        };
    }

    public static OrderItemChangeHistory CreateUpdated(int orderId, int menuItemId, int quantity, int price, int priceWithDiscount, int orderVersion)
    {
        return new OrderItemChangeHistory
        {
            OrderId = orderId,
            MenuItemId = menuItemId,
            Quantity = quantity,
            Price = price,
            PriceWithDiscount = priceWithDiscount,
            OrderVersion = orderVersion,
            Type = OrderItemHistoryType.Updated
        };
    }

    public static OrderItemChangeHistory CreateRemoved(int orderId, int menuItemId, int quantity, int price, int priceWithDiscount, int orderVersion)
    {
        return new OrderItemChangeHistory
        {
            OrderId = orderId,
            MenuItemId = menuItemId,
            Quantity = quantity,
            Price = price,
            PriceWithDiscount = priceWithDiscount,
            OrderVersion = orderVersion,
            Type = OrderItemHistoryType.Removed
        };
    }
}
