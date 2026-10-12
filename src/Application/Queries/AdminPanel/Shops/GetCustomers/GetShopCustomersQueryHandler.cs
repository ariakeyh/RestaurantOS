using Application.Common.Dtos;
using Application.Interface;
using Application.Utils;
using Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries.AdminPanel.Shops.GetCustomers;

public class GetShopCustomersQueryHandler (IAppDbContext context, IAuthService authService) : IRequestHandler<GetShopCustomersQuery, Pagination<GetShopCustomersResponse>>
{
    public async Task<Pagination<GetShopCustomersResponse>> Handle(GetShopCustomersQuery request, CancellationToken cancellationToken)
    {
        var shopId = authService.GetShopId();
        
        var customerQuery = context.Customers
            .Where(x => x.ShopId == shopId)
            .Include(x=>x.User)
            .OrderByDescending(x => x.CreatedAt)
            .AsQueryable();
        
        customerQuery = ApplyFilters(request, customerQuery);
        
        var query = await customerQuery
            .Select(x => new 
            {
                RegisterDate = x.CreatedAt,

                LastOrderDate = x.Orders
                    .OrderByDescending(o => o.CreatedAt)
                    .Select(o => o.CreatedAt)
                    .FirstOrDefault(),

                Customer = x
            }).ToListAsync(cancellationToken);
        
        var result = query.Select(x=> new  GetShopCustomersResponse
        {
            RegisterDate = x.RegisterDate,
            LastOrderDate = x.LastOrderDate,
            Customer = new CustomerInfo(x.Customer)
        }).ToList();

        return await result.ToPagedList(
            request.PageNumber,
            request.PageSize,
            cancellationToken);
    }

    private static IQueryable<Domain.Entities.Customers.Customer> ApplyFilters(GetShopCustomersQuery request, IQueryable<Domain.Entities.Customers.Customer> customerQuery)
    {
        if(!string.IsNullOrEmpty(request.PhoneNumber))
            customerQuery = customerQuery.Where(x=>x.User.PhoneNumber.Contains(request.PhoneNumber));
        
        if(!string.IsNullOrWhiteSpace(request.Name))
            customerQuery = customerQuery.Where(x=>x.Name.Contains(request.Name));
        
        if(request.From.HasValue)
            customerQuery = customerQuery.Where(x => x.CreatedAt >= request.From.Value);
        
        if(request.To.HasValue)
            customerQuery = customerQuery.Where(x => x.CreatedAt <= request.To.Value);
        return customerQuery;
    }
}