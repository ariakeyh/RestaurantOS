using Application.Interface;
using Application.Services.OrderServices;
using Common;
using Common.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries.AdminPanel.Orders.GetPaymentInfo;

public class GetOrderPaymentInfoQueryHandler(IAppDbContext context, IAuthService authService)
    : IRequestHandler<GetOrderPaymentInfoQuery, List<PaymentInfoDto>>
{
    public async Task<List<PaymentInfoDto>> Handle(GetOrderPaymentInfoQuery request,
        CancellationToken cancellationToken)
    {
        var shopId = authService.GetShopId();

        var orderPaymentInfos = await context.Orders
            .SelectMany(o => o.PaymentInfos.Where(x => x.OrderId == request.OrderId && x.Order.ShopId == shopId)
                .OrderByDescending(x => x.Id))
            .ToListAsync(cancellationToken);


        return orderPaymentInfos is null
            ? throw new NotFoundException(ErrorMessages.Order_NotFound)
            : orderPaymentInfos.ConvertAll(PaymentInfoDto.FromModel);
    }
}