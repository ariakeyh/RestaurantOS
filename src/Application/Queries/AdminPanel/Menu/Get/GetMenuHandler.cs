using Application.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries.AdminPanel.Menu.Get;

public class GetMenuHandler(IAppDbContext context, IAuthService authService, IObjectStorage objectStorage)
    : IRequestHandler<GetMenuQuery, GetMenuResult>
{
    public async Task<GetMenuResult> Handle(GetMenuQuery request, CancellationToken cancellationToken)
    {
        var shopId = authService.GetShopId();

        var categories = await context.Categories.Where(x => x.ShopId == shopId)
            .Select(x => new CategoryDto(x.Name, x.ImageName, x.Items.Count, x.Items.Select(m =>
                    new MenuitemDto(m.Id, m.Name, m.Description, m.Ingredients,  m.Price, m.Discount,
                        m.Price - m.Price * m.Discount / 100, m.IsUnAvailable){ImageName = m.ImageName})
                .ToList()))
            .ToListAsync(cancellationToken);


        var tasks = categories
            .SelectMany(c => c.Items)
            .Select(async item =>
            {
                item.ImageName = await objectStorage.GetFileUrl(item.ImageName);
            });

        await Task.WhenAll(tasks);
        return new GetMenuResult(categories);
    }
}