using Application.Common.Dtos;
using Application.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries.AdminPanel.Orders.GetHistories;

public class GetHistoriesOrderQueryHandler(IAppDbContext context, IAuthService authService)
    : IRequestHandler<GetHistoriesOrderQuery, List<GetHistoriesOrderResponse>>
{
    public async Task<List<GetHistoriesOrderResponse>> Handle(GetHistoriesOrderQuery request,
        CancellationToken cancellationToken)
    {
        var shopId = authService.GetShopId();
        var ordersQuery = context.OrderStatusHistories
            .Include(x=>x.Order.Customer.User)
            .Where(x => x.OrderId == request.OrderId && x.Order.ShopId == shopId)
            .OrderByDescending(x => x.CreatedAt)
            .AsQueryable();

        return await ordersQuery.Select(x => new GetHistoriesOrderResponse
        {
            Creator = new CustomerInfo(x.Order.Customer),
            Status = x.Status,
            Date = x.CreatedAt
        }).ToListAsync(cancellationToken);
    }
}