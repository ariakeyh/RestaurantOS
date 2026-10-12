using Domain.Enums;
using Domain.Entities.Shops;

namespace Domain.Entities.Orders;

public class PosOrder : Order
{
    private PosOrder() { }
    
    public PosOrder(Guid posDeviceId,
        Guid customerId,
        int shopId,
        OrderServeInfo serveInfo,
        PosPaymentInfo paymentInfo,
        List<OrderItem> items,
        string orderNumber) : base(customerId, shopId, OrderSource.POS, serveInfo, paymentInfo, items, OrderStatus.Preparing,orderNumber)
    {
        POSDeviceId = posDeviceId;
    }

    public Guid POSDeviceId { get; private set; }

    public POSDevice POSDevice { get; private set; }
}