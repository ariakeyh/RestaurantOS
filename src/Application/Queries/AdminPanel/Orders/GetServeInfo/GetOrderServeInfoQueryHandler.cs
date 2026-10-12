using Application.Interface;
using Application.Services.OrderServices;
using Common;
using Common.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries.AdminPanel.Orders.GetServeInfo;

public class GetOrderServeInfoQueryHandler(IAppDbContext context, IAuthService authService)
    : IRequestHandler<GetOrderServeInfoQuery, ServeInfoDto>
{
    public async Task<ServeInfoDto> Handle(GetOrderServeInfoQuery request,
        CancellationToken cancellationToken)
    {
        var shopId = authService.GetShopId();
        var orderServeInfo = await context.Orders
            .Select(o => o.ServeInfo)
            .FirstOrDefaultAsync(o => o.Id == request.OrderId && o.Order.ShopId == shopId, cancellationToken);

        return orderServeInfo == null
            ? throw new NotFoundException(ErrorMessages.Order_NotFound)
            : ServeInfoDto.FromModel(orderServeInfo);
    }
}