using Application.Events;
using Application.Services;
using Domain.Enums;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Infra.EventSubscribers;

public class PollCreationSubscriber(IServiceScopeFactory scopeFactory) 
    : INotificationHandler<OrderStatusChangeEvent>
{
    public async Task Handle(OrderStatusChangeEvent notification, CancellationToken cancellationToken)
    {
        if (notification.Status != OrderStatus.Completed)
            return;

        using var scope = scopeFactory.CreateScope();
        var pollService = scope.ServiceProvider.GetRequiredService<PollService>();

        await pollService.CreatePoll(notification.OrderId);
    }
}
