using MediatR;
using Application.Services.OrderServices;

namespace Application.Commands.AdminPanel.Order.Edit;

public class EditOrderHandler(OrderService orderService) : IRequestHandler<EditOrderCommand>
{
    public async Task Handle(EditOrderCommand request, CancellationToken cancellationToken)
    {
        await orderService.EditOrder(request.OrderId,
            request.CustomerName,
            request.CustomerPhoneNumber,
            request.Items,
            request.ServeInfo,
            cancellationToken);
    }
}