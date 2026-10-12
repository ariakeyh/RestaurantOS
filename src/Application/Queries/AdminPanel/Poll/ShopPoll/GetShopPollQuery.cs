using MediatR;

namespace Application.Queries.AdminPanel.Poll.ShopPoll;

public sealed record GetShopPollQueryResult(bool ShouldRateFood, int NotifyAfterDays, List<int> QuestionIds);

public sealed record GetShopPollQuery : IRequest<GetShopPollQueryResult?>;