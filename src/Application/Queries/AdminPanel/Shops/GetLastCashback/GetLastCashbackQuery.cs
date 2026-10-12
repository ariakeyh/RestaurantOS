using MediatR;

namespace Application.Queries.AdminPanel.Shops.GetLastCashback;

public sealed record GetLastCashbackQuery : IRequest<GetLastCashbackResult?>;

public sealed record GetLastCashbackResult(byte CashbackPercentage,int MaxCashbackAmount);