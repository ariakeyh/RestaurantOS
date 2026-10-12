using Application.Commands.Pos.Orders.Create;
using Application.Queries.AdminPanel.Orders.GetAll;
using Application.Queries.AdminPanel.Orders.GetMenuItems;
using Common.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.POS;

public class OrderController(IMediator prophet) : PosApiBase
{
    [HttpPost]
    public async Task CreateOrder([FromBody] CreateOrderCommand order)
    {
        await prophet.Send(order);
    }
    
    [HttpGet("{id:int}/items")]
    public async Task GetMenuItems(int id)
    {
        await prophet.Send(new GetOrderMenuItems(id));
    }
    
    [HttpGet]
    public async Task<Pagination<GetAllOrdersResponse>> GetOnGoingOrders([FromQuery] GetAllOrdersQuery request,
        CancellationToken cancellation)
    {
        return await prophet.Send(request, cancellation);
    }
}