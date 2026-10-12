using Domain.Entities.Agg;
using Domain.Entities.Menu;

namespace Domain.Entities.Polls;

public class PollFoodRating : AggregateRootWithKey<int>
{
    public int MenuItemId { get; set; }
    public int ResponseId { get; set; }
    
    public byte Rating { get; set; }
    public string Note { get; set; }

    public MenuItem MenuItem { get; set; }
    public PollResponse Response { get; set; }
}