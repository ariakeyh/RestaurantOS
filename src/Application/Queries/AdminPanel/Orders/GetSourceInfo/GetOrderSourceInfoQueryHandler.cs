using Application.Interface;
using Application.Services.OrderServices;
using Common;
using Common.Exceptions;
using Domain.Entities.Orders;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries.AdminPanel.Orders.GetSourceInfo;

public class GetOrderSourceInfoQueryHandler(IAppDbContext context, IAuthService authService)
    : IRequestHandler<GetOrderSourceInfoQuery, SourceInfoDto>
{
    public async Task<SourceInfoDto> Handle(GetOrderSourceInfoQuery request,
        CancellationToken cancellationToken)
    {
        var shopId = authService.GetShopId();
        
        var orderMeta = await context.Orders
            .Where(o => o.Id == request.OrderId && o.ShopId == shopId)
            .Select(o => new { o.Id, o.Source })
            .FirstOrDefaultAsync(cancellationToken);

        if (orderMeta is null || orderMeta.Id == 0)
            throw new NotFoundException(ErrorMessages.Order_NotFound);

        return orderMeta.Source switch
        {
            OrderSource.Cashier => await context.Orders
                .OfType<CashierOrder>()
                .Where(o => o.Id == request.OrderId)
                .Select(o => new CashierSourceInfoDto
                {
                    CashierId = o.CashierId,
                    CashierName = o.Cashier.Name
                })
                .FirstAsync(cancellationToken),

            OrderSource.POS => await context.Orders
                .OfType<PosOrder>()
                .Where(o => o.Id == request.OrderId)
                .Select(o => new PosSourceInfoDto
                {
                    PosId = o.POSDeviceId,
                    PosName = o.POSDevice.User.Name
                })
                .FirstAsync(cancellationToken),

            OrderSource.Website => new WebsiteSourceInfoDto(),

            _ => throw new NotFoundException(ErrorMessages.Order_NotFound)
        };

    }
}