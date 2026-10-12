using Application.Interface;
using Common;
using Common.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries.AdminPanel.Categories.Get;

public class GetCategoriesQueryHandler(IAppDbContext context, IAuthService authService) : IRequestHandler<GetCategoriesQuery, GetCategoriesQueryResult>
{
    public async Task<GetCategoriesQueryResult> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
    {
        var shopId = authService.GetShopId();
        var category = await context.Categories.AsNoTracking()
            .Where(x => x.Id == request.Id && x.ShopId == shopId)
            .Select(x => new GetCategoriesQueryResult(x.Id, x.Name, x.ImageName, x.Items.Count))
            .FirstOrDefaultAsync(cancellationToken);

        return category ?? throw new NotFoundException(ErrorMessages.Category_NotFound);
    }
}