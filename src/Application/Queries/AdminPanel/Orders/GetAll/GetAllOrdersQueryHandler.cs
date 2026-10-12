using Application.Common.Dtos;
using Application.Interface;
using Application.Utils;
using Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries.AdminPanel.Orders.GetAll;

public class GetAllOrdersQueryHandler(IAppDbContext context, IAuthService authService)
    : IRequestHandler<GetAllOrdersQuery, Pagination<GetAllOrdersResponse>>
{
    public async Task<Pagination<GetAllOrdersResponse>> Handle(GetAllOrdersQuery request,
        CancellationToken cancellationToken)
    {
        var shopId = authService.GetShopId();
        var ordersQuery = context.Orders
            .Where(x => x.ShopId == shopId)
            .Include(x=>x.Customer.User)
            .OrderByDescending(x => x.CreatedAt)
            .AsQueryable();

        if (request.Status.HasValue)
            ordersQuery = ordersQuery.Where(x => x.Status == request.Status);
        
        if(request.CustomerId.HasValue)
            ordersQuery = ordersQuery.Where(x => x.CustomerId == request.CustomerId);

        return await ordersQuery.Select(x => new GetAllOrdersResponse
        {
            Id = x.Id,
            CreationDate = x.CreatedAt,
            ExpectedPreparation = x.ExpectedPreparation,
            ActualPreparation = x.ActualPreparation,
            Source = x.Source,
            TotalPrice = x.TotalPrice,
            TotalPriceWithDiscount = x.TotalPriceWithDiscount,
            Type = x.ServeInfo.Type,
            Customer = new CustomerInfo(x.Customer),
            Status = x.Status,
            OrderNumber = x.OrderNumber
        }).ToPaginationAsync(request.PageNumber, request.PageSize, cancellationToken);
    }
}