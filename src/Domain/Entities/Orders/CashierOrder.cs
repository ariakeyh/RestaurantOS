using Domain.Entities.Auth;
using Domain.Enums;

namespace Domain.Entities.Orders;

public class CashierOrder : Order
{
    
    private CashierOrder() { }
    public CashierOrder(int cashierId,
        Guid customerId,
        int shopId,
        OrderServeInfo serveInfo,
        PaymentInfo? paymentInfo,
        List<OrderItem> items,
        string orderNumber) : base(customerId, shopId, OrderSource.Cashier, serveInfo, paymentInfo, items, OrderStatus.Preparing,orderNumber)
    {
        CashierId = cashierId;
    }

    public int CashierId { get; private set; }
    public User Cashier { get; private set; }
}