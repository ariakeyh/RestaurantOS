using Domain.Entities.Shops;
using Domain.Enums;

namespace Application.Services;

public partial class ShopCreditService
{
    public async Task<int?> Decrease(
        int shopId,
        int amount,
        CreditHistoryType creditHistoryType,
        string note,
        CancellationToken cancellationToken)
    {
        var shop = await GetShop(shopId);

        if (shop.Credit < amount)
            return null;

        shop.Credit -= amount;

        var creditHistory = new CreditHistory
        {
            Amount = amount,
            Type = creditHistoryType,
            Balance = shop.Credit,
            Note = note,
            ShopId = shopId
        };

        shop.CreditHistories.Add(creditHistory);

        await context.SaveChangesAsync(cancellationToken);

        return creditHistory.Id;
    }

}