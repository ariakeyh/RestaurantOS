using Domain.Entities.Agg;
using Domain.Enums;

namespace Domain.Entities.Sms;

public class SmsRequestReceptor : AggregateRootWithKey<int>
{
    public int SmsRequestId { get; private set; }
    public SmsRequest SmsRequest { get; private set; }

    public SmsReceptorStatus Status { get; private set; }
    public string StatusComment { get; private set; } = string.Empty;
    public DateTime SendDate { get; private set; }
    public int? ProviderMessageId { get; private set; }
    public string ReceptorNumber { get; private set; }
    public string Message { get; private set; }
    public int BasePrice { get; private set; }
    public int SellPrice { get; private set; }
    public int FinalProviderPrice { get; private set; }

    public ICollection<SmsRequestReceptorStatusHistory> StatusHistories { get; set; } = [];

    public SmsRequestReceptor(string receptorNumber, string message, int basePrice, int sellPrice, DateTime sendDate)
    {
        ReceptorNumber = receptorNumber;
        Message = message;
        BasePrice = basePrice;
        SellPrice = sellPrice;
        SendDate = sendDate;
    }

    public void SetStatus(SmsReceptorStatus status, string? comment = null)
    {
        if (Status != default)
        {
            StatusHistories.Add(
                new SmsRequestReceptorStatusHistory(Status, StatusComment));
        }


        Status = status;
        StatusComment = comment ?? string.Empty;
    }

    public void SetProviderMessageId(int id)
    {
        ProviderMessageId = id;
    }
}