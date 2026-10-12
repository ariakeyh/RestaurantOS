using Domain.Entities.Agg;

namespace Domain.Entities.Menu;

public class MenuItemPriceHistory : AggregateRootWithKey<int>
{
    public MenuItemPriceHistory()
    {
        
    }

    public MenuItemPriceHistory(int menuItemId, int price, byte discount)
    {
        MenuItemId = menuItemId;
        Price = price;
        Discount = discount;
    }
    public int MenuItemId { get; set; }
    public MenuItem MenuItem { get; set; }
    public int Price { get; set; }
    public byte Discount { get; set; }
    
    public int PriceWithDiscount() => Price - Price * Discount / 100;
}