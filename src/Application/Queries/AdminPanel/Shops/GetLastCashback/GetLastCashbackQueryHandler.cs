using MediatR;
using Application.Interface;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries.AdminPanel.Shops.GetLastCashback;

public class GetLastCashbackQueryHandler(IAppDbContext context, IAuthService authService) : IRequestHandler<GetLastCashbackQuery, GetLastCashbackResult?>
{
    public async Task<GetLastCashbackResult?> Handle(GetLastCashbackQuery request, CancellationToken cancellationToken)
    {
        var shopId = authService.GetShopId();

        var latestCashback = await context.ShopCashbackSettings
            .AsNoTracking()
            .Where(x => x.ShopId == shopId)
            .OrderByDescending(x => x.Id)
            .Select(x => new GetLastCashbackResult(
                x.CashbackPercentage,
                x.MaxCashbackAmount
            ))
            .FirstOrDefaultAsync(cancellationToken);

        return latestCashback;
    }
}