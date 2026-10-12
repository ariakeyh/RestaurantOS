using Application.Interface;
using Domain.Entities.Customers;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries.AdminPanel.Shops.CustomerCredits.CustomerCreditsCards;

public class GetCustomerCreditsCardsQueryHandler(IAppDbContext context, IAuthService authService)
    : IRequestHandler<GetCustomerCreditsCardsQuery, GetCustomerCreditsCardsResult>
{
    public async Task<GetCustomerCreditsCardsResult> Handle(GetCustomerCreditsCardsQuery request,
        CancellationToken cancellationToken)
    {
        var shopId = authService.GetShopId();

        var now = DateTime.UtcNow;

        var query = ApplyFilters(request, shopId);

        var result = await query
            .GroupBy(c => 1)
            .Select(g => new GetCustomerCreditsCardsResult(
                g.Sum(c => c.Amount),
                g.Sum(c => c.SpentAmount),
                g
                    .Where(c => c.Expiration.HasValue && c.Expiration.Value < now)
                    .Sum(c => c.Amount)
            ))
            .FirstOrDefaultAsync(cancellationToken);


        return result;
    }

    private IQueryable<CustomerCredit> ApplyFilters(GetCustomerCreditsCardsQuery request, int shopId)
    {
        var query = context.CustomerCredits.Where(x => x.Customer.ShopId == shopId).AsQueryable();

        if (request.From.HasValue)
            query = query.Where(x => x.CreatedAt >= request.From.Value);

        if (request.To.HasValue)
            query = query.Where(x => x.CreatedAt <= request.To.Value);
        return query;
    }
}