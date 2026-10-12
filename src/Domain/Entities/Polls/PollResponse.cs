using Domain.Entities.Agg;
using Domain.Entities.Orders;

namespace Domain.Entities.Polls;

public class PollResponse : AggregateRootWithKey<int>
{
    public int OrderId { get; set; }
    public byte StarRate { get; set; }
    public string Note { get; set; }
    
    public Order Order { get; set; }
    public ICollection<PollQuestionAnswer> QuestionAnswers { get; set; }
    public ICollection<PollFoodRating> FoodRatings { get; set; }
}