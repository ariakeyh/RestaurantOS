using Application.Interface;
using Common;
using Common.Exceptions;
using Domain.Entities.Orders;

namespace Application.Services.OrderServices;

public class POSOrderCreator(IAuthService authService) : OrderCreatorBase
{
    protected override Task ValidatePaymentInfo(PaymentInfoDto? paymentInfo, CancellationToken cancellationToken)
    {
        if (paymentInfo == null)
            return Task.CompletedTask;

        if (paymentInfo is not PosPaymentInfoDto)
            throw new BadRequestException(ErrorMessages.POSOrder_POSPayment_Required);

        return Task.CompletedTask;
    }

    protected override Task<Order> HandleOrder(
        int shopId,
        Guid customerId,
        ServeInfoDto serveInfo,
        PaymentInfoDto? paymentInfo,
        List<OrderItem> orderItems,
        string orderNumber,
        CancellationToken cancellationToken)
    {
        var posDeviceId = authService.GetShopUserId();
        var posPaymentInfo = paymentInfo?.MapToModel() as PosPaymentInfo ?? throw new InvalidOperationException();
        var newOrder = new PosOrder(posDeviceId, customerId, shopId, serveInfo.MapToModel(), posPaymentInfo,
            orderItems, orderNumber);
        return Task.FromResult<Order>(newOrder);
    }
}