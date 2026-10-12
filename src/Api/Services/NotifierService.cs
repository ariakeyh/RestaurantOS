using Api.Hubs;
using Application.Interface;
using Microsoft.AspNetCore.SignalR;

namespace API.Services;

public class NotifierService(IHubContext<NotifierHub> notifier) : INotifierService
{
    public async Task NotifyEvent(object payload, CancellationToken cancellationToken = default)
    {
        await notifier.Clients.All.SendAsync(nameof(NotifyEvent), payload, null, cancellationToken);
    }

    public async Task NotifyStaleOrders(List<StaleOrderNotification> notifications,
        CancellationToken cancellationToken = default)
    {
        foreach (var notification in notifications)
        {
            var groupName = notification.ShopId.ToString();
            await notifier.Clients.Group(groupName)
                .SendAsync(nameof(NotifyStaleOrders), notification, null, cancellationToken);
        }
    }
}