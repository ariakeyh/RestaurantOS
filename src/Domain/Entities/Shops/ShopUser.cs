using Domain.Entities.Agg;
using Domain.Entities.Auth;
using Domain.Entities.Reports;
using Domain.Enums;

namespace Domain.Entities.Shops;

public class ShopUser : AggregateRootWithKey<Guid>
{
    public int UserId { get; set; }
    public User User { get; set; }

    public int ShopId { get; set; }
    public Shop Shop { get; set; }

    public ShopUserType Type { get; set; }

    public ICollection<ShopUserReport> ShopUserReports { get; set; } = new HashSet<ShopUserReport>();
}