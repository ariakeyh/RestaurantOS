using Application.Commands.AdminPanel.Credits.GenerateLink;
using Application.Queries.AdminPanel.Shops.Credit.CreditHistory.GetAll;
using Common.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.AdminPanel;

public class CreditController(IMediator prophet) : AdminApiBase
{
    [HttpPost("increase")]
    public async Task<IncreaseShopCreditGenerateLinkResult> GeneratePaymentLink(IncreaseShopCreditGenerateLinkCommand request) 
        => await prophet.Send(request);

    [HttpGet]
    public async Task<Pagination<CreditHistoryDto>> GetHistory([FromQuery] GetAllCreditHistoryQuery request)
    {
        return await prophet.Send(request);
    }
}