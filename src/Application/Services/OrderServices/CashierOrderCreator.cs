using Application.Interface;
using Domain.Entities.Orders;

namespace Application.Services.OrderServices;

public class CashierOrderCreator(IAuthService authService) : OrderCreatorBase
{
    protected override Task ValidatePaymentInfo(PaymentInfoDto? paymentInfo, CancellationToken cancellationToken)
    {
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
        var cashierId = authService.GetUserId();

        var newOrder = new CashierOrder(cashierId, customerId, shopId, serveInfo.MapToModel(), paymentInfo?.MapToModel(), orderItems,orderNumber);
    
        return Task.FromResult<Order>(newOrder);
    }
}