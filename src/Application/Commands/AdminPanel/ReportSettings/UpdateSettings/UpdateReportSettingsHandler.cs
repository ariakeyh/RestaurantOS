using Application.Interface;
using Domain.Entities.Reports;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Commands.AdminPanel.ReportSettings.UpdateSettings;

public class UpdateReportSettingsHandler(IAppDbContext context, IAuthService authService)
    : IRequestHandler<UpdateReportSettingsCommand>
{
    public async Task Handle(UpdateReportSettingsCommand request, CancellationToken cancellationToken)
    {
        var shopUserId = authService.GetShopUserId();

        var existingMappings = await GetCurrentShopUserActiveReports(shopUserId, cancellationToken);
        var existingReportIds = existingMappings.Select(x => x.ReportId).ToHashSet();
        var requestedReportIds = request.ReportIds.ToHashSet();

        var reportsToDelete = GetReportsToDelete(existingMappings, requestedReportIds);
        var reportsToAdd = GetReportsToAdd(shopUserId, requestedReportIds, existingReportIds);

        await ApplyChanges(reportsToDelete, reportsToAdd, cancellationToken);
    }

    private async Task<List<ShopUserReport>> GetCurrentShopUserActiveReports(Guid shopUserId, CancellationToken cancellationToken)
    {
        return await context.ShopUserReports
            .Where(x => x.ShopUserId == shopUserId)
            .ToListAsync(cancellationToken);
    }

    private static List<ShopUserReport> GetReportsToDelete(
        List<ShopUserReport> existingMappings,
        HashSet<int> requestedReportIds)
    {
        return existingMappings
            .Where(x => !requestedReportIds.Contains(x.ReportId))
            .ToList();
    }

    private static IEnumerable<ShopUserReport> GetReportsToAdd(
        Guid shopUserId,
        HashSet<int> requestedReportIds,
        HashSet<int> existingReportIds)
    {
        return requestedReportIds
            .Except(existingReportIds)
            .Select(reportId => new ShopUserReport
            {
                ShopUserId = shopUserId,
                ReportId = reportId
            });
    }

    private async Task ApplyChanges(
        List<ShopUserReport> reportsToDelete,
        IEnumerable<ShopUserReport> reportsToAdd,
        CancellationToken cancellationToken)
    {
        if (reportsToDelete.Count != 0)
            context.ShopUserReports.RemoveRange(reportsToDelete);

        var shopUserReports = reportsToAdd as ShopUserReport[] ?? reportsToAdd.ToArray();
        if (shopUserReports.Length != 0)
            await context.ShopUserReports.AddRangeAsync(shopUserReports, cancellationToken);

        await context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }
}