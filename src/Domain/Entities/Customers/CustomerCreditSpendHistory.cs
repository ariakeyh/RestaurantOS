using Domain.Entities.Agg;
using Domain.Entities.Orders;

namespace Domain.Entities.Customers;

public class CustomerCreditSpendHistory : AggregateRootWithKey<int>
{
    public int CustomerCreditId { get; set; }
    public int SpendAmount { get; set; }
    public int BalanceAmount { get; set; }
    public int OrderId { get; set; }
    
    public CustomerCredit Credit { get; set; }
    public Order Order { get; set; }
}