using Application.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries.AdminPanel.MenuItems.GetAll;

public class GetAllMenuItemsQueryHandler(IAppDbContext context, IObjectStorage objectStorage, IAuthService authService)
    : IRequestHandler<GetAllMenuItemsQuery, List<GetAllMenuItemsQueryResult>>
{
    public async Task<List<GetAllMenuItemsQueryResult>> Handle(GetAllMenuItemsQuery request,
        CancellationToken cancellationToken)
    {
        var shopId = authService.GetShopId();
        var query = context.MenuItems.Where(x => x.Category.ShopId == shopId)
            .OrderByDescending(x => x.Id).AsQueryable();

        if (request.CategoryId is not null)
            query = query.Where(x => x.CategoryId == request.CategoryId);

        if (!string.IsNullOrWhiteSpace(request.Name))
            query = query.Where(x => x.Name.Contains(request.Name));

        var rawResults = await query
            .Select(x => new
            {
                x.Id,
                x.Name,
                x.Description,
                x.Ingredients,
                x.ImageName,
                x.Price,
                x.Discount,
                x.IsUnAvailable,
                FinalPrice = x.Price - x.Price * x.Discount / 100,
                x.CategoryId,
                CategoryName = x.Category.Name
            })
            .ToListAsync(cancellationToken);

        var finalItems = new List<GetAllMenuItemsQueryResult>();

        foreach (var x in rawResults)
        {
            var imageUrl = await objectStorage.GetFileUrl(x.ImageName);

            var menuItem = new GetAllMenuItemsQueryResult(
                x.Id,
                x.Name,
                x.Description,
                x.Ingredients,
                imageUrl,
                x.Price,
                x.Discount,
                x.FinalPrice,
                x.IsUnAvailable,
                x.CategoryId,
                x.CategoryName
            );

            finalItems.Add(menuItem);
        }

        return finalItems;
    }
}