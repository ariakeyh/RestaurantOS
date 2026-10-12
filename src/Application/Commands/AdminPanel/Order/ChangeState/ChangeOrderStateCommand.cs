using Domain.Enums;
using MediatR;

namespace Application.Commands.AdminPanel.Order.ChangeState;

public class ChangeOrderStateResult(int OrderId);
public class ChangeOrderStateCommand : IRequest<ChangeOrderStateResult>
{
    internal int Id { get; set; }
    public void SetId(int id) =>  Id = id;
    public OrderStatus NewStatus {get; set;}
}