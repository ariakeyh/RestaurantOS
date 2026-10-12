using Application.Commands.AdminPanel.ShopInfos.Update;
using Application.Queries.AdminPanel.Shops.Credit.Get;
using Application.Queries.AdminPanel.Shops.Get;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.AdminPanel;

public class ShopController(IMediator prophet) : AdminApiBase
{
    [HttpPut]
    [Consumes("multipart/form-data")]
    public async Task Update([FromForm] UpdateShopCommand request)
    {
        await prophet.Send(request);
    }
    
    [HttpGet]
    public async Task<GetShopQueryResult> Get()
    {
        return await prophet.Send(new GetShopQuery());
    }
    
    [HttpGet("credit")]
    public async Task<GetCurrentShopCreditResult>  GetCredit([FromQuery] GetCurrentShopCreditQuery request) =>
        await prophet.Send(request);
}