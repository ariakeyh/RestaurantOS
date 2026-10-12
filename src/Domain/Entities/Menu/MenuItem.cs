using Domain.Entities.Agg;
using Domain.Entities.Orders;
using Domain.Entities.Polls;

namespace Domain.Entities.Menu;

public class MenuItem : AggregateRootWithKey<int>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Ingredients { get; set; }
    public string ImageName { get; set; } = string.Empty;
    public int Price { get; set; }
    public byte Discount { get; set; }
    public int CategoryId { get; set; }
    public bool IsUnAvailable { get; set; }
    public Category Category { get; set; }
    public ICollection<OrderItem> Orders { get; set; }
    public ICollection<PollFoodRating> Ratings { get; set; }
    public ICollection<OrderItemChangeHistory> ChangeHistories { get; set; }
    public ICollection<MenuItemPriceHistory> PriceHistories { get; set; } = new HashSet<MenuItemPriceHistory>();


    public int PriceWithDiscount() => Price - Price * Discount / 100;
    
    public void UpdatePrice(int newPrice, byte newDiscount)
    {
        if (Price == newPrice && Discount == newDiscount) return;
        
        Price = newPrice;
        Discount = newDiscount;
        PriceHistories.Add(new MenuItemPriceHistory(Id, newPrice, newDiscount));
    }
}