using Application.Interface;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Quartz;

namespace Api.BackgroundServices;

public class NotifierBackgroundJob(
    ILogger<NotifierBackgroundJob> logger,
    INotifierService notifierService,
    IAppDbContext dbContext)
    : IJob
{
    public async Task Execute(IJobExecutionContext context)
    {
        var now = DateTime.UtcNow;

        var staleOrders = await dbContext.Orders
            .AsNoTracking()
            .Where(x => x.Status == OrderStatus.PendingAcceptance)
            .Select(x => new
            {
                x.ShopId,
                x.CreatedAt
            })
            .ToListAsync(context.CancellationToken);

        logger.LogInformation("Fetched {StaleOrdersCount} stale orders", staleOrders.Count);

        var groupedNotifications = staleOrders
            .GroupBy(x => x.ShopId)
            .Select(group => new StaleOrderNotification
            {
                ShopId = group.Key,
                NumberOfStaleOrders = group.Count(),
                StaleInMinutes = (int)(now - group.Min(x => x.CreatedAt)).TotalMinutes
            })
            .ToList();

        logger.LogInformation("Sending {GroupCount} group notifications...", groupedNotifications.Count);

        await notifierService.NotifyStaleOrders(groupedNotifications, context.CancellationToken);
        
        logger.LogInformation("Done Sending {GroupCount} group notifications...", groupedNotifications.Count);
        
    }
}