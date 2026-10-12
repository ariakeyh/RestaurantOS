using MediatR;
using Application.Services.OrderServices;

namespace Application.Commands.AdminPanel.Order.ChangeState;
    
public class ChangeOrderStateCommandHandler(OrderService orderService) : IRequestHandler<ChangeOrderStateCommand, ChangeOrderStateResult>
{
    public async Task<ChangeOrderStateResult> Handle(ChangeOrderStateCommand request, CancellationToken cancellationToken)
    {
        await orderService.ChangeStatus(request.Id, request.NewStatus, cancellationToken);
        return new ChangeOrderStateResult(request.Id);
    }
}