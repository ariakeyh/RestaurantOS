using Domain.Enums;

namespace Domain.Entities.Orders;

public class WebsiteOrder : Order
{
    private WebsiteOrder() { }
    public WebsiteOrder(Guid customerId,
        int shopId,
        OrderServeInfo serveInfo,
        PGPaymentInfo paymentInfo,
        List<OrderItem> items,
        string orderNumber) : base(customerId, shopId, OrderSource.Website, serveInfo, paymentInfo, items, OrderStatus.Preparing,orderNumber)
    {
    }
}