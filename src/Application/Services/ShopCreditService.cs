using System.Collections.Concurrent;
using Application.Interface;
using Common;
using Common.Exceptions;
using Domain.Entities.Shops;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Application.Services;

public partial class ShopCreditService(IAppDbContext context)
{
    private static readonly ConcurrentDictionary<int, SemaphoreSlim> ShopLocks = new();
    private async Task<Shop> GetShop(int shopId)
    {
        var shopLock = ShopLocks.GetOrAdd(shopId, _ => new SemaphoreSlim(1, 1));

        await shopLock.WaitAsync();
        try
        {
            var shop = await context.Shops.FirstOrDefaultAsync(x => x.Id == shopId);
            if (shop == null)
                throw new BadRequestException(ErrorMessages.Shop_NotFound);
            
            return shop;
        }
        finally
        {
            shopLock.Release();
        }
    }

    private async Task<InternalPGTransaction> GetTransaction(int transactionId)
    {
        var transaction =
            await context.InternalPgTransactions.FirstOrDefaultAsync(x =>
                x.Type == PGTransactionType.Credit && x.Id == transactionId);

        if (transaction == null)
            throw new BadRequestException(ErrorMessages.InternalTransaction_NotFound);
        
        return transaction;
    }
}