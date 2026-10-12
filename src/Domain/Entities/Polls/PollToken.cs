using Domain.Entities.Agg;
using Domain.Entities.Orders;

namespace Domain.Entities.Polls;

public class PollToken : AggregateRootWithKey<int>
{
    public string Token { get; private set; }
    public int OrderId { get; private set; }
    public Order Order { get; set; }

    public PollToken(int orderId )
    {
        var token = Guid.NewGuid().ToString()[..5];
        OrderId = orderId;
        Token = token;
    }
}