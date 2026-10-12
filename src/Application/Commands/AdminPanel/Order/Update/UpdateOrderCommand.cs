using Application.Services.OrderServices;
using MediatR;

namespace Application.Commands.AdminPanel.Order.Update;

public class UpdateOrderCommand : IRequest<UpdateOrderResult>
{
    internal int OrderId { get; set; }
    public List<Item> Items { get; set; }

    public void SetId(int id) => OrderId = id;
};

public sealed record UpdateOrderResult(int OrderId);

    
