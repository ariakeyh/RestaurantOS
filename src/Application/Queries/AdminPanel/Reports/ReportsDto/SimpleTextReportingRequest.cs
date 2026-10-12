using MediatR;

namespace Application.Queries.AdminPanel.Reports.ReportsDto;

public record SimpleTextReportingRequest : ReportingRequest, IRequest<List<SimpleTextReportingResult>>;

public sealed record SimpleTextReportingResult(string Label, double Value, string? IconName);