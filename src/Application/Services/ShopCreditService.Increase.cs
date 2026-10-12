using Common;
using Common.Exceptions;
using Domain.Entities.Shops;
using Domain.Enums;

namespace Application.Services;

public partial class ShopCreditService
{
    public async Task Increase(int transactionId, CancellationToken cancellationToken)
    {
        var transaction = await GetTransaction(transactionId);

        if (transaction.Status != PGTransactionStatus.Success)
            throw new BadRequestException(ErrorMessages.InternalTransaction_NotSuccessful);

        var shop = await GetShop(transaction.ShopId);

        shop.Credit += transaction.Amount;

        var creditHistory = new CreditHistory
        {
            Amount = transaction.Amount,
            Type = CreditHistoryType.IncreaseGatewayTransaction,
            Balance = shop.Credit
        };
        shop.CreditHistories.Add(creditHistory);
        transaction.CreditHistory = creditHistory;

        await context.SaveChangesAsync(cancellationToken);

    }
}