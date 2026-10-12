using Application.Interface;
using Common;
using Common.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries.AdminPanel.MenuItems.Get;

public class GetMenuItemsQueryHandler(IAppDbContext context, IObjectStorage objectStorage, IAuthService authService)
    : IRequestHandler<GetMenuItemsQuery, GetMenuItemsQueryResult>
{
    public async Task<GetMenuItemsQueryResult> Handle(GetMenuItemsQuery request,
        CancellationToken cancellationToken)
    {
        var shopId = authService.GetShopId();
        var rawItem = await context.MenuItems
            .AsNoTracking()
            .Where(x => x.Id == request.Id && x.Category.ShopId == shopId)
            .Select(x => new
            {
                x.Id,
                x.Name,
                x.Description,
                x.Ingredients,
                x.ImageName,
                x.Price,
                x.Discount,
                FinalPrice = x.Price - x.Price * x.Discount / 100,
                x.IsUnAvailable,
                x.CategoryId,
                CategoryName = x.Category.Name
            })
            .FirstOrDefaultAsync(cancellationToken);

        if (rawItem == null)
            throw new NotFoundException(ErrorMessages.MenuItem_NotFound);

        return new GetMenuItemsQueryResult(
            rawItem.Id,
            rawItem.Name,
            rawItem.Description,
            rawItem.Ingredients,
            await objectStorage.GetFileUrl(rawItem.ImageName),
            rawItem.Price,
            rawItem.Discount,
            rawItem.FinalPrice,
            rawItem.IsUnAvailable,
            rawItem.CategoryId,
            rawItem.CategoryName
        );
    }
}