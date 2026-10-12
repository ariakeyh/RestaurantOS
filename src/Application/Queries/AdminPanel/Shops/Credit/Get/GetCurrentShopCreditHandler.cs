using Application.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries.AdminPanel.Shops.Credit.Get;

public class GetCurrentShopCreditHandler(IAppDbContext context, IAuthService authService) : IRequestHandler<GetCurrentShopCreditQuery, GetCurrentShopCreditResult>
{
    public async Task<GetCurrentShopCreditResult> Handle(GetCurrentShopCreditQuery request, CancellationToken cancellationToken)
    {
        var currentShopId = authService.GetShopId();

        var credit = await context.Shops.Where(x => x.Id == currentShopId)
            .Select(x => x.Credit).FirstOrDefaultAsync(cancellationToken);
        
        return new GetCurrentShopCreditResult(credit);
    }
}