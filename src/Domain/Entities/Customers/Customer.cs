using Domain.Entities.Orders;
using Domain.Entities.Shops;
using Domain.Enums;

namespace Domain.Entities.Customers;

public class Customer : ShopUser
{
    public string Name { get; set; } = string.Empty;

    public CustomerBadge Badge { get; set; }

    public ICollection<Order> Orders { get; set; } = new HashSet<Order>();
    public ICollection<CustomerCredit> Credits { get; set; } = new HashSet<CustomerCredit>();
}