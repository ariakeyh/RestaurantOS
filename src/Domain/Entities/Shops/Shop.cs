using Domain.Entities.Agg;
using Domain.Entities.Customers;
using Domain.Entities.Menu;
using Domain.Entities.Orders;
using Domain.Entities.Sms;

namespace Domain.Entities.Shops;

public class Shop : AggregateRootWithKey<int>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Address { get; set; }
    public string ContactInfo { get; set; }
    public string ImageName { get; set; }
    public byte NumberOfTables { get; set; }
    public int Credit { get; set; }

    public ICollection<ShopCategoryAssignment> ShopCategories { get; set; }
    public ICollection<Category> Categories { get; set; }
    public ICollection<Order> Orders { get; set; }
    public ICollection<ShopUser> Users { get; set; }
    public ICollection<ShopCashbackSetting> CashbackSettings { get; set; }
    public ICollection<SmsRequest> SmsRequests { get; set; }
    public ICollection<CreditHistory> CreditHistories { get; set; }
    public ICollection<InternalPGTransaction> InternalPGTransactions { get; set; }
}