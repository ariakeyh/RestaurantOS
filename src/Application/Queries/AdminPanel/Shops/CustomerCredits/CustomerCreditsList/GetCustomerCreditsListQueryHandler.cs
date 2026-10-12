using Application.Common.Dtos;
using Application.Interface;
using Application.Utils;
using Common.Interfaces;
using Domain.Entities.Customers;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries.AdminPanel.Shops.CustomerCredits.CustomerCreditsList;

public class GetCustomerCreditsListQueryHandler(IAppDbContext context, IAuthService authService) : IRequestHandler<GetCustomerCreditsListQuery, Pagination<GetCustomerCreditsResult>>
{
    public async Task<Pagination<GetCustomerCreditsResult>> Handle(GetCustomerCreditsListQuery request, CancellationToken cancellationToken)
    {
        var shopId = authService.GetShopId();

        var query = context.CustomerCredits
            .AsNoTracking()
            .Include(x => x.Customer)
            .ThenInclude(x=>x.User)
            .Where(x => x.Customer.ShopId == shopId);

        query = ApplyFilters(query, request);

        var result = await query
            .Select(x => new GetCustomerCreditsResult
            {
                Id = x.Id,
                Amount = x.Amount,
                Type = x.Type,
                SpentAmount = x.SpentAmount,
                Expiration = x.Expiration,
                CustomerInfo = new CustomerInfo(x.Customer)
            })
            .OrderByDescending(x => x.Expiration)
            .ToPaginationAsync(request.PageNumber, request.PageSize, cancellationToken);

        return result;
    }

    private static IQueryable<CustomerCredit> ApplyFilters(IQueryable<CustomerCredit> query, GetCustomerCreditsListQuery request)
    {
        if (request.From is not null)
            query = query.Where(x => x.CreatedAt >= request.From.Value);

        if (request.To is not null)
            query = query.Where(x => x.CreatedAt <= request.To.Value);

        if (request.ExpireFrom is not null)
            query = query.Where(x => x.Expiration >= request.ExpireFrom.Value);

        if (request.ExpireTo is not null)
            query = query.Where(x => x.Expiration <= request.ExpireTo.Value);
        
        if(request.CustomerPhoneNumber is not null)
            query = query.Where(x => x.Customer.User.PhoneNumber == request.CustomerPhoneNumber);

        return query;
    }
}