using Application.Interface;
using Common.Exceptions;
using Domain.Entities.Shops;
using Domain.Entities.Sms;
using Domain.Enums;

namespace Application.Services;

public class SmsSchedulerService(SmsPriceProviderService smsPriceProviderService, ShopCreditService shopCreditService, IAppDbContext context)
{
    public async Task Schedule(
        SmsRequestType type,
        int shopId,
        string phoneNumber,
        string message,
        DateTime sendDate)
    {
        await Schedule(type, shopId, [(phoneNumber, message)], sendDate);
    }

    public async Task Schedule(
        SmsRequestType type,
        int shopId,
        List<(string PhoneNumber, string Message)> letters,
        DateTime sendDate)
    {
        Validate(type);

        var shop = await GetShop(shopId);

        var receptors = new List<SmsRequestReceptor>();

        foreach (var (phoneNumber, message) in letters)
        {
            var smsPrice = await smsPriceProviderService.CalculatePricing(message.Length, SmsPriceType.Message);
            var receptor = new SmsRequestReceptor(
                phoneNumber,
                message,
                smsPrice.BasePrice,
                smsPrice.SellPrice,
                sendDate
            );
            receptors.Add(receptor);
        }

        var sumSellPrice = receptors.Sum(x => x.SellPrice);

        var creditHistoryId = await shopCreditService.Decrease(shopId,
            sumSellPrice, 
            CreditHistoryType.DecreaseSmsRequest,
            string.Empty,
            CancellationToken.None);
        
        var smsRequestStatus = creditHistoryId.HasValue
            ? SmsRequestStatus.Approved
            : SmsRequestStatus.RejectedForInSufficientBalance;

        var smsReceptorsStatus =  creditHistoryId.HasValue
            ? SmsReceptorStatus.Scheduled
            : SmsReceptorStatus.Pending;

        foreach (var receptor in receptors)
        {
            receptor.SetStatus(smsReceptorsStatus);
        }

        var smsRequest = new SmsRequest(
            shopId,
            creditHistoryId.Value,
            type,
            smsRequestStatus,
            receptors
        );

        context.SmsRequests.Add(smsRequest);
        await context.SaveChangesAsync(CancellationToken.None);
    }

    public async Task<int> Schedule(string message, SmsRequestType type, int shopId, int receptorsCount, string payload)
    {
        if (type is not (SmsRequestType.Area or SmsRequestType.SmartData))
            throw new BadRequestException($"This method doesn't handle the given type {type}");

        var priceType = type == SmsRequestType.Area ? SmsPriceType.Area : SmsPriceType.Message;
        var smsPrice = await smsPriceProviderService.CalculatePricing(message.Length, priceType);
        var totalBasePrice = receptorsCount * smsPrice.BasePrice;
        var totalSellPrice = receptorsCount * smsPrice.SellPrice;
        
        var creditHistoryId = await shopCreditService.Decrease(shopId,
            totalSellPrice, 
            CreditHistoryType.DecreaseSmsRequest,
            string.Empty,
            CancellationToken.None);
        
        var smsRequest = new SmsRequest(
            shopId,
            creditHistoryId.Value,
            type,
            SmsRequestStatus.PendingApproval,
            payload,
            totalBasePrice,
            totalSellPrice,
            receptorsCount
        );

        context.SmsRequests.Add(smsRequest);
        await context.SaveChangesAsync(CancellationToken.None);
        return smsRequest.Id;
    }

    private static void Validate(SmsRequestType type)
    {
        if (type is SmsRequestType.SmartData or SmsRequestType.Area)
            throw new BadRequestException($"This method doesn't handle the given type {type}");
    }

    private async Task<Shop?> GetShop(int shopId)
    {
        var shop = await context.Shops.FindAsync(shopId);
        if (shop is null)
            throw new BadRequestException("Shop not found");
        return shop;
    }
}