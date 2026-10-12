using System.Text;
using Application.Interface;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Quartz;

namespace Api.BackgroundServices;

public class NotifyOwnerBackgroundJob(
    ILogger<NotifyOwnerBackgroundJob> logger,
    ISmsService smsService,
    IAppDbContext dbContext) : IJob
{
    public async Task Execute(IJobExecutionContext context)
    {
        logger.LogInformation("Fetching pending orders.");

        var cutoff = DateTime.Now.AddMinutes(-5);

        var shopsWithPendingOrders = await dbContext.Shops
            .Select(s => new
            {
                ShopId = s.Id,
                ShopName = s.Name,
                PendingOrdersCount = s.Orders
                    .Count(o => o.Status == OrderStatus.PendingAcceptance && o.CreatedAt <= cutoff),
                OwnerPhoneNumbers = s.Users
                    .Where(u => u.Type == ShopUserType.Manager)
                    .Select(u => u.User.PhoneNumber)
                    .Where(p => !string.IsNullOrEmpty(p))
                    .Distinct()
                    .ToArray()
            })
            .Where(x => x.PendingOrdersCount > 0)
            .ToListAsync(context.CancellationToken);

        logger.LogInformation("Fetched {PendingOrderShopCount} shops with pending orders.", shopsWithPendingOrders.Count);

        foreach (var shopWithPendingOrder in shopsWithPendingOrders)
        {
            var message = GetNotifyMessage(shopWithPendingOrder.ShopName, shopWithPendingOrder.PendingOrdersCount);
           // await smsService.Send(30000, message, shopWithPendingOrder.OwnerPhoneNumbers);
        }

        logger.LogInformation("Owner notification job ended.");
    }

    private static string GetNotifyMessage(string shopName, int pendingOrdersCount)
    {
        return new StringBuilder().Append("مدیر محترم رستوران ")
            .Append(shopName)
            .Append(", ")
            .Append(pendingOrdersCount)
            .Append(" سفارش هنوز در انتظار تایید میباشند")
            .ToString();
    }
}