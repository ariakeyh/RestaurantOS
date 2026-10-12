using Application.Services.OrderServices;
using MediatR;

namespace Application.Commands.AdminPanel.Order.Create;

public class CreateOrderCommandHandler(OrderService orderService) : IRequestHandler<Pos.Orders.Create.CreateOrderCommand>
{
    public async Task Handle(Pos.Orders.Create.CreateOrderCommand request, CancellationToken cancellationToken)
    {
        await orderService.CreateOrder(
            request.CustomerName,
            request.CustomerPhoneNumber,
            request.Source,
            request.Items,
            request.ServeInfo,
            request.PaymentInfo,
            cancellationToken);
    }
}