using Domain.Entities.Agg;
using Domain.Entities.Menu;

namespace Domain.Entities.Orders;

public class OrderItem : AggregateRootWithKey<int>
{
    public int OrderId { get; private set; }
    public int MenuItemId { get; private set; }
    public int Quantity { get; private set; }
    public int Price { get; private set; }
    public int PriceWithDiscount { get; private set; }

    public Order Order { get; private set; }
    public MenuItem MenuItem { get; private set; }

    public int TotalPrice => Quantity * Price;
    public int TotalPriceWithDiscount => Quantity * PriceWithDiscount;

    private OrderItem() { }

    public static OrderItem Create(int menuItemId, int quantity, int price, int priceWithDiscount)
    {
        if (quantity <= 0) throw new ArgumentOutOfRangeException(nameof(quantity), "Quantity must be greater than zero.");
        if (price < 0) throw new ArgumentOutOfRangeException(nameof(price), "Price cannot be negative.");
        if (priceWithDiscount < 0) throw new ArgumentOutOfRangeException(nameof(priceWithDiscount), "Discounted price cannot be negative.");

        return new OrderItem
        {
            MenuItemId = menuItemId,
            Quantity = quantity,
            Price = price,
            PriceWithDiscount = priceWithDiscount
        };
    }

    public void SetQuantity(int newQuantity)
    {
        if (newQuantity <= 0) throw new ArgumentOutOfRangeException(nameof(newQuantity), "Quantity must be greater than zero.");
        Quantity = newQuantity;
    }

    public void SetPricing(int newPrice, int newPriceWithDiscount)
    {
        if (newPrice < 0) throw new ArgumentOutOfRangeException(nameof(newPrice), "Price cannot be negative.");
        if (newPriceWithDiscount < 0) throw new ArgumentOutOfRangeException(nameof(newPriceWithDiscount), "Discounted price cannot be negative.");

        Price = newPrice;
        PriceWithDiscount = newPriceWithDiscount;
    }
}
