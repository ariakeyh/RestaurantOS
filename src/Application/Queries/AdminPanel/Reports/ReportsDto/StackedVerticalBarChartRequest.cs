using MediatR;

namespace Application.Queries.AdminPanel.Reports.ReportsDto;

public record StackedVerticalBarChartRequest : ReportingRequest, IRequest<Dictionary<string,List<StackedVerticalBarChartResult>>>;

public sealed record StackedVerticalBarChartResult(string Label, Dictionary<string, double> Values);