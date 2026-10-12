using Domain.Enums;
using MediatR;

namespace Application.Queries.PollPanel.GetInfo;

public sealed record GetPollInfoQuery(string PollToken) : IRequest<GetPollInfoResult>;

public sealed record GetPollInfoResult(OrderInfo OrderInfo,List<MenuItemsInfo> MenuItems, bool ShouldRateFoods,List<PollQuestions> Questions);

public sealed record OrderInfo(
    int TotalPrice,
    int TotalPriceWithDiscount,
    DateTime Date);

public sealed record MenuItemsInfo(string Name, string ImageName);

public sealed record PollQuestions(int QuestionId, string QuestionTitle, QuestionType QuestionType);