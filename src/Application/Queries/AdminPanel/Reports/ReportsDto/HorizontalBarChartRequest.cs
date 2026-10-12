using MediatR;

namespace Application.Queries.AdminPanel.Reports.ReportsDto;

public record HorizontalBarChartRequest : ReportingRequest, IRequest<List<HorizontalBarChartResult>>
{
    public bool IsDescending { get; set; }
    public int NumberOfItems = 5;
}

public sealed record HorizontalBarChartResult(string Label, double Value);