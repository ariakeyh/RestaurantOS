using Application.Services.OrderServices;
using MediatR;

namespace Application.Commands.Pos.Orders.Create;

public class CreateOrderCommandHandler(OrderService orderService) : IRequestHandler<CreateOrderCommand>
{
    public async Task Handle(CreateOrderCommand request, CancellationToken cancellationToken)
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