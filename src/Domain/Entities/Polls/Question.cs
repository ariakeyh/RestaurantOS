using Domain.Entities.Agg;
using Domain.Enums;

namespace Domain.Entities.Polls;

public class Question : AggregateRootWithKey<int>
{
    public string Title { get; set; }
    public QuestionType Type { get; set; }
    
    public ICollection<PollQuestion> Polls { get; set; }
}