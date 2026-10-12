using Domain.Entities.Agg;
using Domain.Enums;

namespace Domain.Entities.Shops;

public class InternalPGTransaction : AggregateRootWithKey<int>
{
    public PGProvider PGProvider { get; set; }
    public PGTransactionStatus Status { get; set; }
    public PGTransactionType Type { get; set; }
    public string StatusComment { get; set; } = string.Empty;
    public string Payload { get; set; } = string.Empty;
    public int Amount { get; set; }
    public string Authority { get; set; } = string.Empty;

    public int ShopId { get; set; }
    public Shop Shop { get; set; }


    public int? CreditHistoryId { get; set; }
    public CreditHistory CreditHistory { get; set; }
}