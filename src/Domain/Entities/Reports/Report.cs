using Domain.Entities.Agg;
using Domain.Enums;

namespace Domain.Entities.Reports;

public class Report : AggregateRootWithKey<int>
{
    public int ReportCategoryId { get; set; }
    public ReportCategory ReportCategory { get; set; }
    
    public string Name { get; set; }
    public ReportType ReportType { get; set; }
    public string ApiUrl { get; set; }
    public string Description { get; set; }

    public ICollection<ShopUserReport> ShopUserReports { get; set; } = new HashSet<ShopUserReport>();
}