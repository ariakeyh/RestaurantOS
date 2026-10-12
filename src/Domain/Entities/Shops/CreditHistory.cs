using Domain.Entities.Agg;
using Domain.Entities.Sms;
using Domain.Enums;

namespace Domain.Entities.Shops;

public class CreditHistory : AggregateRootWithKey<int>
{
    public int ShopId { get; set; }
    public Shop Shop { get; set; } = null!;

    public int Amount { get; set; }
    public int Balance { get; set; }
    public CreditHistoryType Type { get; set; }

    public string Note { get; set; } = string.Empty;


    public InternalPGTransaction? InternalPgTransaction { get; set; }

    public SmsRequest? SmsRequest { get; set; }
}