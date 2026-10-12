using Application.Queries.AdminPanel.Customer.FinanceInfo;
using Application.Queries.AdminPanel.Customer.OrdersInfo;
using Application.Queries.AdminPanel.Customer.PollInfo;
using Application.Queries.AdminPanel.Customer.SmsInfo;
using Application.Queries.AdminPanel.Customer.GetName;
using Application.Queries.AdminPanel.Shops.GetCustomerProfile;
using Application.Queries.AdminPanel.Shops.GetCustomers;
using Common.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.AdminPanel;

public class CustomerController(IMediator prophet) : AdminApiBase
{
    [HttpGet]
    public async Task<Pagination<GetShopCustomersResponse>> GetAll([FromQuery] GetShopCustomersQuery request)
    {
        return await prophet.Send(request);
    }

    [HttpGet("{id:guid}")]
    public async Task<GetCustomerProfileResult> GetProfile([FromRoute]  Guid id)
    {
        return await prophet.Send(new GetCustomerProfileQuery{Id = id});
    }

    [HttpGet("advanced-info/{id:guid}/order-info")]
    public async Task<OrdersInfo> GetOrdersInfo([FromRoute] Guid id) => await prophet.Send(new GetCustomerOrdersInfoQuery (id));
    
    [HttpGet("advanced-info/{id:guid}/finance-info")]
    public async Task<FinanceInfo> GetFinanceInfo([FromRoute] Guid id) => await prophet.Send(new GetCustomerFinanceInfoQuery (id));

    [HttpGet("advanced-info/{id:guid}/poll-info")]
    public async Task<PollInfo> GetPollInfo([FromRoute] Guid id) => await prophet.Send(new GetCustomerPollInfoQuery (id));

    [HttpGet("advanced-info/{id:guid}/sms-info")]
    public async Task<SmsInfo> GetSmsInfo([FromRoute] Guid id) => await prophet.Send(new GetCustomerSmsInfoQuery (id));

    
    [HttpGet("/search")]
    public async Task<string?> GetCustomerName ([FromQuery] GetCustomerNameQuery request) => await prophet.Send(request);
}