using Domain.Entities.Agg;
using Domain.Entities.Customers;

namespace Domain.Entities.Shops;

public class ShopCashbackSetting : AggregateRootWithKey<int>
{
    public int ShopId { get; set; }
    public byte CashbackPercentage { get; set; }
    public int MaxCashbackAmount { get; set; }
    public int? ExpirationInDays { get; set; }
    
    public Shop Shop { get; set; }
    public ICollection<OrderCustomerCredit> GeneratedCredits { get; set; }
}