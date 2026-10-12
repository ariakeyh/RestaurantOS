using Application.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries.AdminPanel.ReportSettings.Dropdown;

public class GetReportsDropdownHandler(IAppDbContext context) : IRequestHandler<GetReportsDropdownQuery, List<ReportDropdownDto>>
{
    public async Task<List<ReportDropdownDto>> Handle(GetReportsDropdownQuery request, CancellationToken cancellationToken)
    {
        return await context.Reports
            .OrderBy(r => r.ReportCategory.Name) 
            .Select(r => new ReportDropdownDto(
                r.Id,
                r.ReportCategoryId,
                r.ReportCategory.Name,
                r.ReportType,
                r.Description
            ))
            .ToListAsync(cancellationToken);
    }
}