using Application.Queries.AdminPanel.Shops.CustomerCredits.CustomerCreditsInfo;
using Application.Queries.AdminPanel.Shops.CustomerCredits.CustomerCreditsList;
using Common.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.AdminPanel;

public class CustomerCreditController(IMediator prophet) : AdminApiBase
{
    [HttpGet]
    public async Task<Pagination<GetCustomerCreditsResult>> GetCustomersCredits([FromQuery] GetCustomerCreditsListQuery request) =>
        await prophet.Send(request);
    
    [HttpGet("{customerCreditId:int}")]
    public async Task<List<CustomerCreditDto>> GetCustomerCreditsInfo([FromRoute] int customerCreditId)
    {
        var request = new GetCustomerCreditsInfoQuery(customerCreditId);
        return await prophet.Send(request);
    }
}