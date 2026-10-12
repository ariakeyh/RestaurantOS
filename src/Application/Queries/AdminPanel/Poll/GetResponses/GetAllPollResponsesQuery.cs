using Common.Interfaces;

namespace Application.Queries.AdminPanel.Poll.GetResponses;

public class GetAllPollResponsesResult
{
    public int OrderId { get; set; }
    public string Note { get; set; }
    public byte StarRate { get; set; }
    public int NumberOfFoodRatings { get; set; }
    public int NumberOfQuestionsAnswered { get; set; }
    public string CreateDate { get; set; }
}
public sealed record GetAllPollResponsesQuery(DateTime? FromDate, DateTime? ToDate, List<int>? Ratings) : PaginationRequest<GetAllPollResponsesResult>;