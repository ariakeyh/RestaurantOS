using Application.Commands.AdminPanel.SmsRequest.CreateForArea;
using Application.Commands.AdminPanel.SmsRequest.CreateForSmartData;
using Application.Queries.AdminPanel.Sms.GetPrice;
using Application.Queries.AdminPanel.Sms.RequestInfo;
using Application.Queries.AdminPanel.SmsRequests.GetAll;
using Common.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.AdminPanel;

public class SmsController(IMediator prophet) : AdminApiBase
{
    [HttpGet]
    public async Task<Pagination<SmsRequestDto>> GetAllSmsRequestsPaginated([FromQuery] GetAllSmsRequestsQuery request) => 
        await prophet.Send(request);
    
    [HttpPost("area")]
    public async Task<CreateSmsRequestForAreaResult> CreateSmsRequestForArea([FromBody] CreateSmsRequestForAreaCommand request) => await prophet.Send(request);
    
    [HttpPost("smart-data")]
    public async Task<CreateSmsRequestForSmartDataResult> CreateSmsRequestForSmartData([FromBody] CreateSmsRequestForSmartDataCommand request) => 
        await prophet.Send(request);
    
    [HttpGet("calculate-price")]
    public async Task<GetSmsPriceQueryResult>  GetSmsPriceCalculator([FromBody] GetSmsPriceQuery request) => 
        await prophet.Send(request);
    
    [HttpGet("{id:int}")]
    public async Task<GetSmsRequestInfoResult> GetSmsRequestInfo([FromRoute] int id) => await prophet.Send(new  GetSmsRequestInfoQuery(id));
}