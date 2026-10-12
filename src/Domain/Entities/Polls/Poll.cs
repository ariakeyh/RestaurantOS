using Domain.Entities.Agg;
using Domain.Entities.Shops;

namespace Domain.Entities.Polls;

public class Poll : AggregateRootWithKey<int>
{
    public int ShopId { get; set; }
    public bool ShouldRateFoods { get; set; }
    public int NotifyAfterDays { get; set; }
    
    public Shop Shop { get; set; }
    public ICollection<PollQuestion> Questions { get; set; }
}