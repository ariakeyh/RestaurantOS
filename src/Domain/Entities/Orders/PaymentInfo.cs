using Domain.Entities.Agg;
using Domain.Enums;

namespace Domain.Entities.Orders;

public abstract class PaymentInfo : AggregateRootWithKey<int>
{
    public int OrderId { get; set; }
    public decimal Amount { get; set; }
    public DateTime PaidAt { get; set; }
    public PaymentInfoType Type { get; set; }

    public Order Order { get; set; }
}