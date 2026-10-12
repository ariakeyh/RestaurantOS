using Application.Events;
using Application.Services;
using Domain.Enums;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Infra.EventSubscribers;

public class CashBackCreationSubscriber(IServiceScopeFactory scopeFactory) : INotificationHandler<OrderStatusChangeEvent>
{
    public async Task Handle(OrderStatusChangeEvent notification, CancellationToken cancellationToken)
    {
        if (notification.Status != OrderStatus.Completed)
        {
            return;
        }

        using var scope = scopeFactory.CreateScope();
        var creditService = scope.ServiceProvider.GetRequiredService<CustomerCreditService>();
        await creditService.IncreaseCreditBasedOnOrderCashback(notification.OrderId, CancellationToken.None);
    }
}