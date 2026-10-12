using Common;
using Common.Exceptions;
using Domain.Entities.Orders;

namespace Application.Services.OrderServices;

public class WebsiteOrderCreator : OrderCreatorBase
{
    protected override Task ValidatePaymentInfo(PaymentInfoDto? paymentInfo, CancellationToken cancellationToken)
    {
        if (paymentInfo == null)
            throw new BadRequestException(ErrorMessages.WebsiteOrder_Payment_Required);
        
        if (paymentInfo is not PGPaymentInfoDto)
            throw new BadRequestException(ErrorMessages.WebsiteOrder_PGPayment_Required);

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
        var pgPaymentInfo = paymentInfo?.MapToModel() as PGPaymentInfo ?? throw new InvalidOperationException();
        
        var newOrder = new WebsiteOrder(customerId, shopId, serveInfo.MapToModel(), pgPaymentInfo, orderItems,orderNumber);

        return Task.FromResult<Order>(newOrder);
    }
}