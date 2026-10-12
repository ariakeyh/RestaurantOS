using MediatR;

namespace Application.Commands.AdminPanel.Credits.GenerateLink;

public sealed record IncreaseShopCreditGenerateLinkCommand(int AmountToIncrease) : IRequest<IncreaseShopCreditGenerateLinkResult>;
public sealed record IncreaseShopCreditGenerateLinkResult(string PaymentLink);