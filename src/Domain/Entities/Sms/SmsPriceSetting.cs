using Domain.Entities.Agg;
using Domain.Enums;

namespace Domain.Entities.Sms;

public class SmsPriceSetting : AggregateRootWithKey<int>
{
    public SmsPriceType Type { get; set; }
    public int BasePrice { get; set; }
    public int SellPrice { get; set; }
}