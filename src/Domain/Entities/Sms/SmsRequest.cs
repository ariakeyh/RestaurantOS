using Domain.Entities.Agg;
using Domain.Entities.Shops;
using Domain.Enums;

namespace Domain.Entities.Sms;

public class SmsRequest : AggregateRootWithKey<int>
{
    public int ShopId { get; private set; }
    public Shop Shop { get; private set; } = null!;
    public int? CreditHistoryId { get; set; }
    public CreditHistory CreditHistory { get; set; }
    public string? ProviderPackId { get; private set; }
    public SmsRequestType Type { get; private set; }
    public SmsRequestStatus Status { get; private set; }
    public int ReceptorsCount { get; private set; }
    public int TotalDelivered { get; private set; }
    public int TotalFailed { get; private set; }
    public int TotalSent { get; private set; }
    
    public int BasePrice { get; private set; }
    public int SellPrice { get; private set; }
    public int FinalProviderPrice { get; private set; }
    
    public string Payload { get; private set; }

    public ICollection<SmsRequestReceptor> Receptors { get; set; }

    public SmsRequest(int shopId, int creditHistoryId, SmsRequestType type, SmsRequestStatus status, ICollection<SmsRequestReceptor> receptors)
    {
        ShopId = shopId;
        CreditHistoryId = creditHistoryId;
        Type = type;
        Status = status;
        Payload = string.Empty;
        Receptors = receptors;
        BasePrice = receptors.Sum(x => x.BasePrice);
        SellPrice = receptors.Sum(x => x.SellPrice);
    }
    
    public SmsRequest(int shopId, int? creditHistoryId, SmsRequestType type, SmsRequestStatus status, string payload, int basePrice, int sellPrice, int receptorsCount )
    {
        ShopId = shopId;
        CreditHistoryId = creditHistoryId;
        Type = type;
        Status = status;
        Payload = payload;
        BasePrice = basePrice;
        SellPrice = sellPrice;
        ReceptorsCount = receptorsCount;
        Receptors = [];
    }

    public void UpdateStatus(int totalSent, int totalDelivered, int totalFailed)
    {
        TotalSent = totalSent;
        TotalDelivered = totalDelivered;
        TotalFailed = totalFailed;
    }

    public void IncreaseSend()
    {
        TotalSent++;
    }
    
    public void SetProviderPackId(string packId)
    {
        ProviderPackId = packId;
    }
}