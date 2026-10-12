using Domain.Entities.Orders;

namespace Application.Services.OrderServices;

public  abstract class OrderCreatorBase
{
    public async Task<Order> CreateOrder(
        int shopId,
        Guid customerId,
        ServeInfoDto serveInfo,
        PaymentInfoDto? paymentInfo,
        List<OrderItem> orderItems,
        CancellationToken cancellationToken = default)
    {
        await ValidatePaymentInfo(paymentInfo, cancellationToken);
        
        var orderNumber = Guid.NewGuid().ToString("N")[..6];
        return await HandleOrder(shopId, customerId, serveInfo, paymentInfo, orderItems, orderNumber, cancellationToken);
    }
    
    protected abstract Task ValidatePaymentInfo(PaymentInfoDto? paymentInfo, CancellationToken cancellationToken);
    protected abstract Task<Order> HandleOrder(int shopId, Guid customerId, ServeInfoDto serveInfo, PaymentInfoDto? paymentInfo, List<OrderItem> orderItems,string orderNumber, CancellationToken cancellationToken);
}