using Domain.Entities.Agg;

namespace Domain.Entities.Polls;

public class PollQuestionAnswer : AggregateRootWithKey<int>
{
    public int QuestionId { get; set; }
    public int ResponseId { get; set; }
    
    public string Answer { get; init; }

    public PollQuestion Question { get; set; }
    public PollResponse Response { get; set; }
}