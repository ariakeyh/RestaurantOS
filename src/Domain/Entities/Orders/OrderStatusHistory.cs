using Domain.Entities.Agg;
using Domain.Enums;

namespace Domain.Entities.Orders;

public class OrderStatusHistory : AggregateRootWithKey<int>
{
    public int OrderId { get; set; }

    public OrderStatus Status { get; set; }

    public Order Order { get; set; }
}