using Domain.Entities.Agg;

namespace Domain.Entities.Reports;

public class ReportCategory : AggregateRootWithKey<int>
{
    public string Name { get; set; }
    public ICollection<Report> Reports { get; set; } = new HashSet<Report>();
}