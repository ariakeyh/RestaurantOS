using Application.Commands.AdminPanel.Order.ChangeState;
using Application.Commands.AdminPanel.Order.Create;
using Application.Commands.AdminPanel.Order.Edit;
using Application.Commands.AdminPanel.Order.Update;
using Application.Queries.AdminPanel.Orders.GetAll;
using Application.Queries.AdminPanel.Orders.GetHistories;
using Application.Queries.AdminPanel.Orders.GetMenuItems;
using Application.Queries.AdminPanel.Orders.GetPaymentInfo;
using Application.Queries.AdminPanel.Orders.GetServeInfo;
using Application.Queries.AdminPanel.Orders.GetSourceInfo;
using Application.Services.OrderServices;
using Common.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.AdminPanel;

public class OrderController(IMediator prophet) : AdminApiBase
{
    [HttpPut("{id:int}")]
    public async Task<ChangeOrderStateResult> Put([FromRoute] int id, [FromBody] ChangeOrderStateCommand request)
    {
        request.SetId(id);
        return await prophet.Send(request);
    }

    [HttpGet]
    public async Task<Pagination<GetAllOrdersResponse>> GetOnGoingOrders([FromQuery] GetAllOrdersQuery request,
        CancellationToken cancellation)
    {
        return await prophet.Send(request, cancellation);
    }
    
    [HttpGet("{id:int}/items")]
    public async Task<List<OrderMenuItemDto>> GetMenuItems(int id)
    {
        return await prophet.Send(new GetOrderMenuItems(id));
    }

    [HttpGet("{id:int}/serve-info")]
    public async Task<ServeInfoDto> GetServeInfo(int id, CancellationToken cancellation)
    {
        return await prophet.Send(new GetOrderServeInfoQuery { OrderId = id }, cancellation);
    }

    [HttpGet("{id:int}/source-info")]
    public async Task<SourceInfoDto> GetSourceInfo(int id, CancellationToken cancellation)
    {
        return await prophet.Send(new GetOrderSourceInfoQuery { OrderId = id }, cancellation);
    }

    [HttpGet("{id:int}/status-history")]
    public async Task<List<GetHistoriesOrderResponse>> GetStatusHistory(int id, CancellationToken cancellation)
    {
        return await prophet.Send(new GetHistoriesOrderQuery { OrderId = id }, cancellation);
    }

    [HttpGet("{id:int}/payment-history")]
    public async Task<List<PaymentInfoDto>> GetPaymentHistory(int id, CancellationToken cancellation)
    {
        return await prophet.Send(new GetOrderPaymentInfoQuery() { OrderId = id }, cancellation);
    }

    [HttpPost]
    public async Task CreateOrder([FromBody] CreateOrderCommand request)
    {
        await prophet.Send(request);
    }

    [HttpPut("update-items/{orderId:int}")]
    public async Task<UpdateOrderResult> UpdateOrderItems([FromRoute] int orderId, [FromBody] UpdateOrderCommand request)
    {
        request.SetId(orderId);
        return await prophet.Send(request);
    }
    [HttpPut("edit-order/{orderId:int}")]
    public async Task EditOrder([FromRoute]int orderId, [FromBody] EditOrderCommand request) => await prophet.Send(request);
}