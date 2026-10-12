using Domain.Entities.Agg;
using Domain.Entities.Shops;

namespace Domain.Entities.Reports;

public class ShopUserReport : AggregateRoot
{
    public Guid ShopUserId { get; set; }
    public ShopUser ShopUser { get; set; }
    public int ReportId { get; set; }
    public Report Report { get; set; }
}