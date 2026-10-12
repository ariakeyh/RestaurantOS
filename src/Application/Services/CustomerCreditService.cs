using System.Text;
using Application.Interface;
using Application.Services.OrderServices;
using Domain.Entities.Customers;
using Domain.Entities.Shops;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Application.Services;

public class CustomerCreditService(IAppDbContext context, ICacheService cacheService, OrderService orderService, SmsSchedulerService smsSchedulerService)
{
    private const string CashbackKeyPrefix = "CashbackConfig";
    private static string GetCacheKey(int shopId) => $"{CashbackKeyPrefix}:{shopId}";

    public async Task IncreaseCreditBasedOnOrderCashback(int orderId, CancellationToken cancellationToken)
    {
        var order = await orderService.GetOrderMarketingInfo(orderId);

        var cashbackSetting = await GetCashback(order.ShopId, cancellationToken);

        var cashbackAmount = CalculateCashbackAmount(order, cashbackSetting);

        await AddCredit(order.Id, order.CustomerId, cashbackAmount, cashbackSetting, cancellationToken);

        var now = DateTime.Now;
        var credits = await GetCustomerValidCredits(cancellationToken, order, now);
        
        var closestExpiration = credits.Min(x => x.Expiration);
        var sumAllCredits = credits.Sum(x => x.Amount - x.SpentAmount);

        var message = BuildCreditMessage(order.CustomerName, order.ShopName, sumAllCredits, closestExpiration);
        
        await smsSchedulerService.Schedule(SmsRequestType.Credit, order.ShopId, order.CustomerPhone, message, now);
    }

    private async Task<List<CustomerCredit>> GetCustomerValidCredits(CancellationToken cancellationToken, OrderMarkettingInfoDto order,
        DateTime now)
    {
        var credits = await context.CustomerCredits
            .Where(x=>x.CustomerId == order.CustomerId)
            .Where(x=>x.Expiration == null || x.Expiration >= now)
            .Where(x=>x.Amount - x.SpentAmount > 0)
            .ToListAsync(cancellationToken);
        return credits;
    }

    public static string BuildCreditMessage(
        string customerName,
        string shopName,
        decimal sumAllCredits,
        DateTime? closestExpiration)
    {
        var sb = new StringBuilder();

        if (!string.IsNullOrWhiteSpace(customerName))
        {
            sb.Append("✨ ");
            sb.Append(customerName);
            sb.Append(" عزیز، ");
        }

        sb.Append("🎉 شما ");
        sb.Append(sumAllCredits);
        sb.Append(" تومان اعتبار از طرف ");
        sb.Append(shopName);
        sb.Append(" دریافت کرده‌اید.");

        if (closestExpiration.HasValue)
        {
            var daysToExpire = (closestExpiration.Value - DateTime.Now).Days;
            if (daysToExpire > 0)
            {
                sb.Append(" ⏰ این اعتبار تا ");
                sb.Append(daysToExpire);
                sb.Append(" روز دیگر معتبر است.");
            }
            else
            {
                sb.Append(" ⏰ اعتبار شما امروز منقضی می‌شود!");
            }
        }
        else
        {
            sb.Append(" ✅ این اعتبار بدون تاریخ انقضا است.");
        }

        sb.Append(" از خریدتان لذت ببرید! 🍽️☕️");

        return sb.ToString();
    }

    private async Task AddCredit(int orderId,
        Guid customerId,
        int cashbackAmount,
        ShopCashbackSetting cashbackSetting,
        CancellationToken cancellationToken)
    {
        var credit = new OrderCustomerCredit
        {
            CustomerId = customerId,
            Amount = cashbackAmount,
            SpentAmount = 0,
            Expiration = cashbackSetting.ExpirationInDays.HasValue
                ? DateTime.UtcNow.AddDays(cashbackSetting.ExpirationInDays.Value)
                : null,
            Type = CustomerCreditType.Cashback,
            OrderId = orderId,
            CashbackSettingId = cashbackSetting.Id,
        };

        context.OrderCustomerCredits.Add(credit);

        await context.SaveChangesAsync(cancellationToken);
    }

    private static int CalculateCashbackAmount(OrderMarkettingInfoDto order, ShopCashbackSetting cashbackSetting)
    {
        var cashbackAmount = Math.Min(
            order.TotalPriceWithDiscount * cashbackSetting.CashbackPercentage / 100,
            cashbackSetting.MaxCashbackAmount
        );
        return cashbackAmount;
    }

    private async Task<ShopCashbackSetting> GetCashback(int currentShopId, CancellationToken cancellationToken)
    {
        var cashbackSetting = await cacheService.GetValue<ShopCashbackSetting>(GetCacheKey(currentShopId));
        if (cashbackSetting is not null) return cashbackSetting;

        cashbackSetting = await context.ShopCashbackSettings
            .Where(x => x.ShopId == currentShopId)
            .OrderBy(x => x.Id)
            .LastAsync(cancellationToken);

        await cacheService.SetValue(GetCacheKey(currentShopId), cashbackSetting, TimeSpan.FromMinutes(10));
        return cashbackSetting;
    }
}