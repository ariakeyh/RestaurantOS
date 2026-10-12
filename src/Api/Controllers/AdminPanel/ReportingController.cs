using Application.Queries.AdminPanel.Dashboard.GetDashboardOrderCharts;
using Application.Queries.AdminPanel.Reports.CategoriesLeaderBoard;
using Application.Queries.AdminPanel.Reports.CategoriesPriceVolumeLeaderboard;
using Application.Queries.AdminPanel.Reports.CreditReportCards;
using Application.Queries.AdminPanel.Reports.MenuItemLeaderboard;
using Application.Queries.AdminPanel.Reports.MenuItemPriceVolumeLeaderBoard;
using Application.Queries.AdminPanel.Reports.OrderRatingLeaderboard;
using Application.Queries.AdminPanel.Reports.OrdersPrepTimeLeaderboard;
using Application.Queries.AdminPanel.Reports.PollMenuItemLeaderboard;
using Application.Queries.AdminPanel.Reports.PollReportCard;
using Application.Queries.AdminPanel.Reports.ReportsDto;
using Application.Queries.AdminPanel.Shops.CustomerCredits.CustomerCreditsCards;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace Api.Controllers.AdminPanel;
[EnableRateLimiting("StrictPolicy")]
public class ReportingController(IMediator prophet) : AdminApiBase
{
    [HttpGet("leaderboard")]
    public async Task<List<HorizontalBarChartResult>> GetMenuItemLeaderboard([FromQuery] GetMenuItemLeaderboardQuery request)
    {
        return await prophet.Send(request);
    }

    [HttpGet("orders-chart")]
    public async Task<Dictionary<string, List<StackedVerticalBarChartResult>>> GetDashboardOrderCharts(
        [FromQuery] GetDashboardOrderChartsQuery request)
    {
        return await prophet.Send(request);
    }
    
    [HttpGet("credits-cards")]
    public async Task<GetCustomerCreditsCardsResult> GetCustomerCreditsCards([FromQuery] GetCustomerCreditsCardsQuery request)
    {
        return await prophet.Send(request);
    }
    
    [HttpGet("leaderboard-price")]
    public async Task<List<HorizontalBarChartResult>> GetMenuItemPriceVolumeLeaderboard([FromQuery] GetMenuItemPriceVolumeLeaderboardQuery request) => 
        await prophet.Send(request);
    
    [HttpGet("leaderboard-categories")]
    public async Task<List<HorizontalBarChartResult>> GetCategoriesLeaderboard([FromQuery] GetCategoriesLeaderboardQuery request) =>
        await prophet.Send(request);
    
    [HttpGet("leaderboard-categories-price")]
    public async Task<List<HorizontalBarChartResult>> GetCategoriesPriceVolumeLeaderboard([FromQuery] GetCategoriesPriceVolumeLeaderboardQuery request) => 
        await prophet.Send(request);
    
    [HttpGet("leaderboard-prep-time")]
    public async Task<List<HorizontalBarChartResult>> GetOrdersPrepTimeLeaderboard([FromQuery] GetOrdersPrepTimeLeaderboardQuery request) => 
        await prophet.Send(request);
    
    [HttpGet("orders-prep-time")]
    public async Task<List<SimpleTextReportingResult>> GetOrdersPrepTimeLeaderboard([FromQuery] GetCreditReportQuery request) => 
        await prophet.Send(request);
    
    [HttpGet("leaderboard-poll-items")]
    public async Task<List<HorizontalBarChartResult>> GetPollMenuItemLeaderboard([FromQuery] GetPollMenuItemLeaderboardQuery request) => 
        await prophet.Send(request);
    
    [HttpGet("poll-card")]
    public async Task<List<SimpleTextReportingResult>> GetPollReportCard([FromQuery] GetPollReportCardQuery request) => 
        await prophet.Send(request);

    [HttpGet("orders-rating-card")]
    public async Task<List<HorizontalBarChartResult>> GetOrderRatingReportCard(
        [FromQuery] GetPollOrderRatingLeaderboardQuery request) => await prophet.Send(request);
}