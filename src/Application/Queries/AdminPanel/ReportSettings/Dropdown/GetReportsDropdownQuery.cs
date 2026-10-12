using Domain.Enums;
using MediatR;

namespace Application.Queries.AdminPanel.ReportSettings.Dropdown;

public sealed record GetReportsDropdownQuery : IRequest<List<ReportDropdownDto>>;

public sealed record ReportDropdownDto(
    int Id,
    int CategoryId,
    string CategoryName,
    ReportType ReportType,
    string Description
);