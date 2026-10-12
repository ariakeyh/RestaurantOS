using Application.Queries.AdminPanel.Dashboard.GetDashboardInfoQuery;
using Application.Queries.AdminPanel.Dashboard.GetDashboardOrderCharts;
using Application.Queries.AdminPanel.Reports.MenuItemLeaderboard;
using Application.Queries.AdminPanel.Reports.ReportsDto;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.AdminPanel;

public class DashboardController(IMediator prophet) : AdminApiBase
{
    [HttpGet("info")]
    public async Task<GetDashboardInfoResult> GetDashboardCharts([FromQuery] GetDashboardInfoQuery request) =>
        await prophet.Send(request);

    [HttpGet("orders-chart")]
    public async Task<Dictionary<string, List<StackedVerticalBarChartResult>>> GetDashboardOrderCharts() 
    {
        var query = new GetDashboardOrderChartsQuery
        {
            From = null,
            To = null
        };
        
        return await prophet.Send(query);
    }
    
    [HttpGet("leaderboard")]
    public async Task<List<HorizontalBarChartResult>> GetMenuItemLeaderboard()
    {
        var request = new GetMenuItemLeaderboardQuery
        {
            From = null,
            To = null,
            IsDescending = true
        };
        return await prophet.Send(request);
    }
}