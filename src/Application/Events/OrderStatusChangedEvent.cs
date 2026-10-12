using Domain.Enums;
using MediatR;

namespace Application.Events;

public class OrderStatusChangeEvent(int orderId, OrderStatus status) : INotification
{
    public int OrderId { get; } = orderId;
    public OrderStatus Status { get; } = status;
}