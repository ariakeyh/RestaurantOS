using Application.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries.AdminPanel.Categories.GetDropdown;

public class GetDropdownCategoriesQueryHandler(IAppDbContext context, IAuthService authService)
    : IRequestHandler<GetDropdownCategoriesQuery, List<GetDropdownCategoriesQueryResult>>
{
    public async Task<List<GetDropdownCategoriesQueryResult>> Handle(GetDropdownCategoriesQuery request,
        CancellationToken cancellationToken)
    {
        var shopId = authService.GetShopId();
        return await context.Categories
            .AsNoTracking()
            .Where(x => x.ShopId == shopId)
            .OrderByDescending(x => x.Name)
            .Select(x => new GetDropdownCategoriesQueryResult(x.Id, x.Name, x.ImageName, x.Items.Count))
            .ToListAsync(cancellationToken);
    }
}