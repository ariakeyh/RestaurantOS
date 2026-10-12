using Application.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries.AdminPanel.Categories.GetAll;

public class GetAllCategoriesQueryHandler(IAppDbContext context, IAuthService authService)
    : IRequestHandler<GetAllCategoriesQuery, List<GetAllCategoriesQueryResult>>
{
    public async Task<List<GetAllCategoriesQueryResult>> Handle(GetAllCategoriesQuery request,
        CancellationToken cancellationToken)
    {
        var shopId = authService.GetShopId();
        return await context.Categories.Where(x => x.ShopId == shopId)
            .OrderByDescending(x => x.Id)
            .Select(x => new GetAllCategoriesQueryResult(x.Id, x.Name, x.ImageName, x.Items.Count))
            .ToListAsync(cancellationToken);
    }
}