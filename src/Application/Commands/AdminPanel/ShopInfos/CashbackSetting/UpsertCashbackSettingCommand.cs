using MediatR;

namespace Application.Commands.AdminPanel.ShopInfos.CashbackSetting;

public sealed record UpsertCashbackSettingCommand(byte CashbackPercentage, int MaxCashbackAmount,int ExpirationInDays) : IRequest<UpsertCashbackSettingResult>;

public sealed record UpsertCashbackSettingResult;