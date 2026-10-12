using Application.Interface;
using Application.Queries.AdminPanel.Reports.ReportsDto;
using Application.Utils;
using Application.Utils.ChartData;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries.AdminPanel.Dashboard.GetDashboardOrderCharts;

public class GetDashboardOrderChartsQueryHandler(IAppDbContext context, IAuthService authService)
    : IRequestHandler<GetDashboardOrderChartsQuery, Dictionary<string, List<StackedVerticalBarChartResult>>>
{
    public async Task<Dictionary<string, List<StackedVerticalBarChartResult>>> Handle(GetDashboardOrderChartsQuery request,
        CancellationToken cancellationToken)
    {
        var currentShopId = authService.GetShopId();

        var (from, to) = request.GetNormalizedRange();

        var query = context.Orders.AsNoTracking().Where(x => x.ShopId == currentShopId).AsQueryable();

        query = query.Where(o => o.CreatedAt >= from && o.CreatedAt <= to);

        var orders = await query.Select(o => new
            {
                o.CreatedAt,
                o.Source
            })
            .ToListAsync(cancellationToken);

        var chartResult = ChartDataBuilder.BuildDailyHourlyMultiValueChart(
            source: orders,
            dateSelector: o => o.CreatedAt,
            categorySelector: o =>o.Source.ToString(),
            valueSelector: o => 1
        );
        return chartResult;
    }
}