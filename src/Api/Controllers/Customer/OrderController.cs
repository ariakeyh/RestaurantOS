using Application.Queries.Customer.Orders.GetOrders;
using Common.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Customer;

[Authorize]
public class OrderController(IMediator mediator) : CustomerApiBase
{
    [HttpGet]
    public async Task<Pagination<GetOrdersQueryResult>> GetOrders([FromQuery] GetOrdersQuery req)
    {
        return await mediator.Send(req);
    }
}