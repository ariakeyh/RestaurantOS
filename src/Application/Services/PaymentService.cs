using Application.Interface;
using Common;
using Common.Exceptions;
using Domain.Entities.Shops;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Application.Services;

public class InternalPaymentService(
    IAppDbContext context,
    ShopCreditService shopCreditService,
    IPaymentGatewayService gateway)
{
    public async Task<string> GeneratePaymentLink(
        PGTransactionType type,
        int shopId,
        string userPhoneNumber,
        string description,
        int amount,
        CancellationToken cancellationToken)
    {
        var result = await gateway.CreatePaymentLink(amount, description, userPhoneNumber, cancellationToken);

        var transaction = new InternalPGTransaction
        {
            PGProvider = PGProvider.Sepehr,
            Status = PGTransactionStatus.Created,
            Type = type,
            Amount = amount,
            Authority = result.Authority,
            ShopId = shopId
        };

        context.InternalPgTransactions.Add(transaction);
        await context.SaveChangesAsync(cancellationToken);

        return result.URL;
    }

    public async Task VerifyPaymentLink(string authority, CancellationToken cancellationToken)
    {
        var transaction = await GetTransaction(authority, cancellationToken);

        var result = await gateway.VerifyPayment(authority, transaction.Amount, cancellationToken);

        transaction.StatusComment = result.Message;
        transaction.Status = result.IsSuccess ? PGTransactionStatus.Success : PGTransactionStatus.Failed;
        transaction.Payload = result.Payload;

        await context.SaveChangesAsync(cancellationToken);
        await ApplyTransaction(transaction);
    }

    private async Task ApplyTransaction(InternalPGTransaction transaction)
    {
        if (transaction.Status == PGTransactionStatus.Success &&
            transaction.Type == PGTransactionType.Credit)
        {
            await shopCreditService.Increase(transaction.Id, CancellationToken.None);
        }
    }

    private async Task<InternalPGTransaction> GetTransaction(string authority, CancellationToken cancellationToken)
    {
        var transaction = await context.InternalPgTransactions
            .FirstOrDefaultAsync(x => x.Status == PGTransactionStatus.Created &&
                                      x.PGProvider == PGProvider.Sepehr &&
                                      x.Authority == authority,
                cancellationToken);

        if (transaction is null)
            throw new BadRequestException(ErrorMessages.InternalTransaction_NotFound);

        return transaction;
    }
}