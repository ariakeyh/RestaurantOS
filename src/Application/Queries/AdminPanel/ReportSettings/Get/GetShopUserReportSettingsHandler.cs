using Application.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries.AdminPanel.ReportSettings.Get;

public class GetShopUserReportSettingsHandler(IAppDbContext context, IAuthService authService) :  IRequestHandler<GetShopUserReportSettingsQuery, List<ReportSettingsDto>>
{
    public async Task<List<ReportSettingsDto>> Handle(GetShopUserReportSettingsQuery request, CancellationToken cancellationToken)
    {
        var shopUserId = authService.GetShopUserId();
        
        var reportSettings = await context.ShopUserReports
            .AsNoTracking()
            .Where(sur => sur.ShopUserId == shopUserId)
            .Include(sur => sur.Report)
            .Select(sur => new ReportSettingsDto(
                sur.Report.Id,
                sur.Report.Name,
                sur.Report.ApiUrl,
                sur.Report.ReportType,
                sur.Report.Description
            ))
            .ToListAsync(cancellationToken);

        return reportSettings;
    }
}