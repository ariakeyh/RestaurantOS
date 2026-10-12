using Domain.Entities.Orders;
using Domain.Entities.Shops;

namespace Domain.Entities.Customers;

public class OrderCustomerCredit : CustomerCredit
{
    public int? OrderId { get; set; }
    public int? CashbackSettingId { get; set; }
    
    public Order Order { get; set; }
    public ShopCashbackSetting CashbackSetting { get; set; }
}