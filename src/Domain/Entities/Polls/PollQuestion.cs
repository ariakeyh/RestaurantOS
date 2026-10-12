using Domain.Entities.Agg;

namespace Domain.Entities.Polls;

public class PollQuestion : AggregateRootWithKey<int>
{
    public int PollId { get; set; }
    public int QuestionId { get; set; }

    public Poll Poll { get; set; }
    public Question Question { get; set; }
    public ICollection<PollQuestionAnswer> Answers { get; set; }
}