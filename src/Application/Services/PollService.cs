using System.Text;
using Application.Interface;
using Application.Services.OrderServices;
using Domain.Entities.Polls;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Application.Services;

public class PollService(IAppDbContext context, ICacheService cacheService,OrderService orderService, SmsSchedulerService smsScheduler,IShlinkService shlink)
{
    public async Task CreatePoll(int orderId)
    {
        var order = await orderService.GetOrderMarketingInfo(orderId);
        var token = await CreateToken(orderId);
        var pollLink = await shlink.ShortenUrl("https://poll.encel.menu/" + token);
        var message = BuildOrderPollMessage(order.ShopName, pollLink); 
        var pollSettings = await GetPollSettings(order.ShopId);
        await ScheduleSms(order.CustomerName, order.ShopId, pollSettings.NotifyAfterDays, message);
    }

    private async Task<string> CreateToken(int orderId)
    {
        var pollToken = new PollToken(orderId);
        context.PollTokens.Add(pollToken);
        await context.SaveChangesAsync(CancellationToken.None);
        return pollToken.Token;
    }

    private async Task ScheduleSms(string customerPhoneNumber, int shopId,
        int notifyAfterDays, string message)
    {
        await smsScheduler.Schedule(SmsRequestType.Poll, shopId, customerPhoneNumber, message,
            DateTime.Now.AddDays(notifyAfterDays));
    }

    private async Task<Poll> GetPollSettings(int shopId)
    {
        var cacheKey = $"Poll:{shopId}";
        var pollSettings = await cacheService.GetValue<Poll>(cacheKey);

        if (pollSettings is not null) return pollSettings;

        pollSettings = await context.Polls.Where(x => x.ShopId == shopId).OrderBy(x => x.Id).LastAsync();
        await cacheService.SetValue(cacheKey, pollSettings, TimeSpan.FromHours(6));

        return pollSettings;
    }

    private static string BuildOrderPollMessage(string shopName, string pollLink)
    {
        var sb = new StringBuilder();

        sb.AppendLine("مشتری گرامی،");
        sb.AppendLine();
        sb.AppendLine($"از اینکه سفارش خود را از {shopName} ثبت کردید سپاسگزاریم.");
        sb.AppendLine("خوشحال می‌شویم نظر شما را در یک نظرسنجی کوتاه بدانیم.");
        sb.AppendLine();
        sb.AppendLine($"لینک نظرسنجی: {pollLink}");

        return sb.ToString();
    }
}