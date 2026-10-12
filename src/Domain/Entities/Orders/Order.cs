using Domain.Entities.Agg;
using Domain.Entities.Customers;
using Domain.Entities.Shops;
using Domain.Enums;

namespace Domain.Entities.Orders;

public abstract class Order : AggregateRootWithKey<int>
{
    private readonly List<OrderItem> _items = [];
    private readonly List<OrderItemChangeHistory> _changeHistories = [];
    private readonly List<OrderStatusHistory> _statusHistories = [];
    private readonly List<PaymentInfo> _paymentInfos = [];

    public Guid CustomerId { get; private set; }
    public int ShopId { get; private set; }
    public OrderSource Source { get; private set; }
    public OrderStatus Status { get; private set; }

    public DateTime? ExpectedPreparation { get; private set; }
    public DateTime? ActualPreparation { get; private set; }

    public int OrderVersion { get; private set; }
    public string OrderNumber { get; private set; } 
    public int TotalPrice { get; private set; }
    public int TotalPriceWithDiscount { get; private set; }

    public Customer Customer { get; private set; }
    public Shop Shop { get; private set; }
    public OrderServeInfo ServeInfo { get; private set; }

    public IReadOnlyCollection<OrderItem> Items => _items;
    public IReadOnlyCollection<OrderItemChangeHistory> ChangeHistories => _changeHistories;
    public IReadOnlyCollection<OrderStatusHistory> StatusHistories => _statusHistories;
    public IReadOnlyCollection<PaymentInfo> PaymentInfos => _paymentInfos;

    #region Behaviors

    protected Order()
    {
    }

    protected Order(
        Guid customerId,
        int shopId,
        OrderSource source,
        OrderServeInfo serveInfo,
        PaymentInfo? paymentInfo,
        List<OrderItem> items,
        OrderStatus initialStatus,
        string orderNumber)
    {
        CustomerId = customerId;
        ShopId = shopId;
        Source = source;
        ServeInfo = serveInfo ?? throw new ArgumentNullException(nameof(serveInfo));
        OrderNumber = orderNumber;

        UpdateOrderItems(items);
        if (paymentInfo != null) AddPayment(paymentInfo);
        ChangeStatus(initialStatus);
    }

    public void UpdateOrderItems(List<OrderItem> items)
    {
        if (items == null || items.Count == 0)
            throw new InvalidOperationException("Order must have at least one item.");

        var incomingByMenuItem = items.ToDictionary(i => i.MenuItemId);
        var existingByMenuItem = _items.ToDictionary(i => i.MenuItemId);

        HandleRemovedItems(existingByMenuItem, incomingByMenuItem);

        HandleAddedOrUpdatedItems(incomingByMenuItem, existingByMenuItem);

        TotalPrice = _items.Sum(x => x.TotalPrice);
        TotalPriceWithDiscount = _items.Sum(x => x.TotalPriceWithDiscount);
        OrderVersion++;
    }

    private void HandleAddedOrUpdatedItems(Dictionary<int, OrderItem> incomingByMenuItem, Dictionary<int, OrderItem> existingByMenuItem)
    {
        foreach (var (menuItemId, incomingItem) in incomingByMenuItem)
        {
            if (existingByMenuItem.TryGetValue(menuItemId, out var existingItem))
            {
                if (existingItem.Quantity == incomingItem.Quantity)
                    continue;

                _changeHistories.Add(OrderItemChangeHistory.CreateUpdated(
                    orderId: Id,
                    menuItemId: menuItemId,
                    quantity: incomingItem.Quantity,
                    price: incomingItem.Price,
                    priceWithDiscount: incomingItem.PriceWithDiscount,
                    orderVersion: OrderVersion + 1
                ));

                existingItem.SetQuantity(incomingItem.Quantity);
            }
            else
            {
                _items.Add(incomingItem);

                _changeHistories.Add(OrderItemChangeHistory.CreateAdded(
                    orderId: Id,
                    menuItemId: menuItemId,
                    quantity: incomingItem.Quantity,
                    price: incomingItem.Price,
                    priceWithDiscount: incomingItem.PriceWithDiscount,
                    orderVersion: OrderVersion + 1
                ));
            }
        }
    }

    private void HandleRemovedItems(Dictionary<int, OrderItem> existingByMenuItem, Dictionary<int, OrderItem> incomingByMenuItem)
    {
        foreach (var (menuItemId, existingItem) in existingByMenuItem)
        {
            if (incomingByMenuItem.ContainsKey(menuItemId)) continue;
            
            _changeHistories.Add(OrderItemChangeHistory.CreateRemoved(
                orderId: Id,
                menuItemId: menuItemId,
                quantity: existingItem.Quantity,
                price: existingItem.Price,
                priceWithDiscount: existingItem.PriceWithDiscount,
                orderVersion: OrderVersion + 1
            ));
            _items.Remove(existingItem);
        }
    }


    private void AddStatusHistory(OrderStatus status)
    {
        _statusHistories.Add(new OrderStatusHistory
        {
            Status = status
        });
    }

    public void ChangeStatus(OrderStatus newStatus)
    {
        Status = newStatus;
        AddStatusHistory(newStatus);
    }

    private void AddPayment(PaymentInfo payment)
    {
        ArgumentNullException.ThrowIfNull(payment);
        _paymentInfos.Add(payment);
    }

    public void SetExpectedPreparation(DateTime expected)
    {
        ExpectedPreparation = expected;
    }
    
    public void ChangeServeInfo(OrderServeInfo serveInfo)
    {
        ArgumentNullException.ThrowIfNull(serveInfo);
        ServeInfo = serveInfo;
    }
    
    public void ChangeCustomer(Customer customer)
    {
        ArgumentNullException.ThrowIfNull(customer);
        CustomerId = customer.Id;
        Customer = customer;
    }

    #endregion
}