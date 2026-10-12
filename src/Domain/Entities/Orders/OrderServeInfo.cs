using Domain.Entities.Agg;
using Domain.Enums;

namespace Domain.Entities.Orders;

public abstract class OrderServeInfo : AggregateRootWithKey<int>
{
    public ServeInfoType Type { get; set; }
    public Order Order { get; set; }
}