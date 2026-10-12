using Application.Interface;
using Common;
using Common.Exceptions;
using Domain.Entities.Shops;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Commands.AdminPanel.ShopInfos.CashbackSetting;

public class UpsertCashbackSettingCommandHandler(IAppDbContext context, IAuthService authService) : IRequestHandler<UpsertCashbackSettingCommand, UpsertCashbackSettingResult>
{
    public async Task<UpsertCashbackSettingResult> Handle(UpsertCashbackSettingCommand request, CancellationToken cancellationToken)
    {
        var currentShopId = authService.GetShopId();
        
        var latestCashback = await context.ShopCashbackSettings
            .Where(x => x.ShopId == currentShopId)
            .OrderByDescending(x => x.CreatedAt)
            .FirstOrDefaultAsync(cancellationToken);

        if (latestCashback is not null && latestCashback.CashbackPercentage == request.CashbackPercentage &&
            latestCashback.MaxCashbackAmount == request.MaxCashbackAmount)
            throw new DuplicateException(ErrorMessages.CashbackSetting_Duplicate);

        var newCashback = new ShopCashbackSetting
        {
            ShopId = currentShopId,
            CashbackPercentage = request.CashbackPercentage,
            MaxCashbackAmount = request.MaxCashbackAmount,
            ExpirationInDays = request.ExpirationInDays,
        };
        
        context.ShopCashbackSettings.Add(newCashback);
        await context.SaveChangesAsync(cancellationToken);
        
        return new UpsertCashbackSettingResult();
    }
}