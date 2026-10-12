using Application.Interface;
using Application.Services;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Commands.AdminPanel.Credits.GenerateLink;

public class IncreaseShopCreditGenerateLinkHandler(
    IAppDbContext context,
    IAuthService authService,
    InternalPaymentService internalPaymentService)
    : IRequestHandler<IncreaseShopCreditGenerateLinkCommand, IncreaseShopCreditGenerateLinkResult>
{
    public async Task<IncreaseShopCreditGenerateLinkResult> Handle(IncreaseShopCreditGenerateLinkCommand request,
        CancellationToken cancellationToken)
    {
        var currentShopId = authService.GetShopId();
        var currentUserId = authService.GetUserId();
        var ownerOfShop = await context.Shops
            .Where(x => x.Id == currentShopId)
            .SelectMany(x => x.Users)
            .Include(x=>x.User)
            .Where(s => s.UserId == currentUserId)
            .FirstOrDefaultAsync(cancellationToken);

        if (ownerOfShop is null)
            throw new UnauthorizedAccessException();

        var paymentLink = await internalPaymentService.GeneratePaymentLink(PGTransactionType.Credit,
            currentShopId,
            ownerOfShop.User.PhoneNumber,
            "افزایش اعتبار",
            request.AmountToIncrease,
            cancellationToken);

        return new IncreaseShopCreditGenerateLinkResult(paymentLink);
    }
}