using Application.Interface;
using Common;
using Common.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries.AdminPanel.Shops.Get;

public class GetShopQueryHandler(IAppDbContext context, IObjectStorage objectStorage, IAuthService authService) : IRequestHandler<GetShopQuery, GetShopQueryResult>
{
    public async Task<GetShopQueryResult> Handle(GetShopQuery request, CancellationToken cancellationToken)
    {
        var shopId = authService.GetShopId();
        var shop = await context.Shops.AsNoTracking()
            .Where(x => x.Id == shopId)
            .Select(x => new
            {
                x.Name,
                x.Description,
                x.Address,
                x.ContactInfo,
                x.ImageName,
                x.NumberOfTables
            })
            .FirstOrDefaultAsync(cancellationToken);

        if (shop is null)
            throw new NotFoundException(ErrorMessages.NotFound);

        return new GetShopQueryResult(
            shop.Name,
            shop.Description,
            shop.Address,
            shop.ContactInfo,
            await objectStorage.GetFileUrl(shop.ImageName),
            shop.NumberOfTables
        );
    }   
}