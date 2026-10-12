using MediatR;

namespace Application.Commands.PollPanel.Poll.Submit;

public sealed record SubmitPollCommand(
    string PollToken,
    byte Rating,
    string Note,
    List<FoodRatingDto>? FoodRatings,
    List<QuestionAnswerDto>? Questions
) : IRequest;

public sealed record FoodRatingDto(int MenuItemId, byte Rating, string Note);
public sealed record QuestionAnswerDto(int QuestionId, string Answer);