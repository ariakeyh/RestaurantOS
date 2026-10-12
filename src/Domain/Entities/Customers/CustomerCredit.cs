using Domain.Entities.Agg;
using Domain.Enums;

namespace Domain.Entities.Customers;

public abstract class CustomerCredit : AggregateRootWithKey<int>
{
    public Guid CustomerId { get; set; }
    public int Amount { get; set; }
    public int SpentAmount { get; set; }
    public DateTime? Expiration { get; set; }
    public CustomerCreditType Type { get; set; }

    public Customer Customer { get; set; }
    public ICollection<CustomerCreditSpendHistory> SpendHistories { get; set; }
    
}