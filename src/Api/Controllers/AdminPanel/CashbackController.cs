using Application.Commands.AdminPanel.ShopInfos.CashbackSetting;
using Application.Queries.AdminPanel.Shops.GetLastCashback;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.AdminPanel;

public class CashbackController(IMediator prophet) : AdminApiBase
{
    [HttpPost]
    public async Task<UpsertCashbackSettingResult> UpsertCashbackSetting([FromBody] UpsertCashbackSettingCommand request) => 
        await prophet.Send(request);
    
    [HttpGet]
    public async Task<GetLastCashbackResult?> GetLastCashback([FromQuery] GetLastCashbackQuery request) => 
        await prophet.Send(request);
}