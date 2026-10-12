using Domain.Enums;
using MediatR;

namespace Application.Queries.AdminPanel.ReportSettings.Get;

public sealed record GetShopUserReportSettingsQuery : IRequest<List<ReportSettingsDto>>;

public sealed record ReportSettingsDto(
    int Id,
    string Title,
    string Url,
    ReportType Type,
    string Description
);