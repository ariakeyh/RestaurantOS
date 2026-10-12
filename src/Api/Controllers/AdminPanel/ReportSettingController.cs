using Application.Commands.AdminPanel.ReportSettings.UpdateSettings;
using Application.Queries.AdminPanel.ReportSettings.Dropdown;
using Application.Queries.AdminPanel.ReportSettings.Get;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.AdminPanel;

public class ReportSettingController (IMediator prophet) : AdminApiBase
{
    [HttpPatch]
    public async Task UpdateReportSettings([FromBody] UpdateReportSettingsCommand request) => 
        await prophet.Send(request);
        
    [HttpGet("dropdown")]
    public async Task<List<ReportDropdownDto>> GetReportsDropdown([FromQuery] GetReportsDropdownQuery request) => 
        await prophet.Send(request);
    
    [HttpGet]
    public async Task<List<ReportSettingsDto>> GetReportSettings([FromQuery] GetShopUserReportSettingsQuery request) => 
        await prophet.Send(request);
}