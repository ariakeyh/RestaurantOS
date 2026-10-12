using Application.Interface;
using Domain.Entities.Sms;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Application.Services;

public class SmsPriceProviderService(IAppDbContext context, ICacheService cacheService)
{
    private const string CacheKey = "SmsBasePrice";
    private const int MessageLengthForPage = 60;

    public async Task<(int BasePrice, int SellPrice)> CalculatePricing(int messageLength, SmsPriceType type)
    {
        var cacheKey = CacheKey + type.GetHashCode();

        var priceSetting = await GetSmsPriceSettings(type, cacheKey);

        var pages = GetPagesCountBasedOnMessageLength(messageLength);

        return (pages * priceSetting.BasePrice, pages *  priceSetting.SellPrice);
    }

    private static int GetPagesCountBasedOnMessageLength(int messageLength)
    {
        messageLength += 6; //لغو11
        var pages = messageLength / MessageLengthForPage + 1;
        return pages;
    }


    private async Task<SmsPriceSetting> GetSmsPriceSettings(SmsPriceType type, string cacheKey)
    {
        var priceSetting = await cacheService.GetValue<SmsPriceSetting>(cacheKey);

        if (priceSetting is not null)
            return priceSetting;
        
        priceSetting = await context.SmsPriceSettings
            .Where(x => x.Type == type)
            .OrderBy(x => x.Id)
            .LastAsync();

        await cacheService.SetValue(cacheKey, priceSetting, TimeSpan.FromDays(10));
        
        return priceSetting;
    }
}