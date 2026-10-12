using MediatR;

namespace Application.Queries.AdminPanel.Reports.ReportsDto;

public record LineChartRequest : ReportingRequest, IRequest<List<LineChartResult>>;

public sealed record LineChartResult(string Label, double Value);