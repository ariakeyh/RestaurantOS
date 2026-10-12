using Domain.Entities.Agg;
using Domain.Entities.Shops;

namespace Domain.Entities.Auth;

public class User : AggregateRootWithKey<int>
{
    public string Name { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string? AdminPasswordHashed { get; set; }

    public ICollection<ShopUser> Shops { get; set; }
}